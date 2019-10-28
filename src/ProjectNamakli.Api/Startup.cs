// <copyright file="Startup.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>21/06/2019 2:21 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using ProjectNamakli.Application.Helpers.Interfaces;
using ProjectNamakli.Application.Parsers;
using ProjectNamakli.Application.Parsers.Interfaces;
using ProjectNamakli.Application.PipelineBehaviors;
using ProjectNamakli.Application.Queries.Catalogs;
using Swashbuckle.AspNetCore.Swagger;

namespace ProjectNamakli.Api
{
    /// <summary>
    /// Program startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/> instance</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets program configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Method for adding program services
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> instance</param>
        /// <returns><see cref="IServiceProvider"/> instance</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = false;
                o.AssumeDefaultVersionWhenUnspecified = false;
                o.DefaultApiVersion = ApiVersion.Parse("1");
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(x => x.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddSwaggerGen(cfg =>
            {
                cfg.DescribeAllEnumsAsStrings();
                cfg.SwaggerDoc("v1", new Info { Title = "10Manga", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
            });

            services.AddResponseCompression();

            return AutofacContainer(services);
        }

        /// <summary>
        /// Method for configuring program services
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> instance</param>
        /// <param name="env"><see cref="IHostingEnvironment"/> instance</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(
                new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

            app.UseResponseCompression();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseApiVersioning();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "10Manga");
            });
        }

        private IServiceProvider AutofacContainer(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(GetCatalogsCriterion).Assembly
            };

            var builder = new ContainerBuilder();

            RegisterParsers(builder);

            RegisterMultiton(builder, assemblies);

            RegisterMediatr(builder, assemblies);

            builder.Populate(services);

            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        private void RegisterParsers(ContainerBuilder builder)
        {
            builder.RegisterType<ReadMangaParser>()
                .Named<IParser>("ReadManga")
                .InstancePerLifetimeScope();

            builder.RegisterType<MintMangaParser>()
                .Named<IParser>("MintManga")
                .InstancePerLifetimeScope();

            builder.RegisterType<MangaFoxParser>()
                .Named<IParser>("MangaFox")
                .InstancePerLifetimeScope();

            builder.RegisterType<ParserFactory>()
                .As<IParserFactory>()
                .InstancePerLifetimeScope();
        }

        private void RegisterMultiton(ContainerBuilder builder, Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(IMultiton<,>))
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private void RegisterMediatr(ContainerBuilder builder, Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(PerformancePipelineBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(x =>
            {
                var c = x.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();
        }
    }
}
