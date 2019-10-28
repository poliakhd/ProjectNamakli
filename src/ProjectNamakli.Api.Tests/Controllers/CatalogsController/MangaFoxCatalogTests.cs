// <copyright file="MangaFoxCatalogTests.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>21/06/2019 10:36 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json;
using ProjectNamakli.Application.Extensions;
using ProjectNamakli.Application.Models;
using ProjectNamakli.Domain.Types;
using Xunit;

namespace ProjectNamakli.Api.Tests.Controllers.CatalogsController
{
    [Collection("ApiTestsCollection")]
    public class MangaFoxCatalogTests : BaseApiTests
    {
        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        /// <inheritdoc />
        public MangaFoxCatalogTests(TestContext context)
            : base(context)
        {
        }

        [Fact(DisplayName = "Integration -> MangaFoxCatalog -> GetCatalogs")]
        public async void GetCatalogsAsync_Popular()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/catalogs");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<Catalog>>(await result.GetContent());

            collection.Should()
                .NotBeNull();

            collection.Count()
                .Should()
                .Be(Enum.GetValues(typeof(CatalogType)).Length);
        }

        [Fact(DisplayName = "Integration -> MangaFoxCatalog -> GetCatalogContent")]
        public async void GetCatalogContentAsync_Popular()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/catalogs/mangafox");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should()
                .Be(70);
        }

        [Fact(DisplayName = "Integration -> MangaFoxCatalog -> GetCatalogContent -> Update")]
        public async void GetCatalogContentAsync_Update()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/catalogs/mangafox?sortType=update");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should()
                .Be(70);
        }
        
        [Fact(DisplayName = "Integration -> MangaFoxCatalog -> GetCatalogContent -> Rating")]
        public async void GetCatalogContentAsync_Rating()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/catalogs/mangafox?sortType=rating");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should()
                .Be(70);
        }
        
        [Fact(DisplayName = "Integration -> MangaFoxCatalog -> Page -> GetCatalogContent")]
        public async void GetCatalogContentAsync_Page_Popular()
        {
            var result = await Context.Client
                .GetAsync($"/api/v1/catalogs/mangafox?page={_random.Next(1, 10)}");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should()
                .Be(70);
        }

        [Fact(DisplayName = "Integration -> MangaFoxCatalog -> GetCatalogContent -> Page -> Update")]
        public async void GetCatalogContentAsync_Page_Update()
        {   
            var result = await Context.Client
                .GetAsync($"/api/v1/catalogs/mangafox?sortType=update&page={_random.Next(1, 10)}");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should()
                .Be(70);
        }
        
        [Fact(DisplayName = "Integration -> MangaFoxCatalog -> GetCatalogContent -> Page -> Rating")]
        public async void GetCatalogContentAsync_Page_Rating()
        {
            var result = await Context.Client
                .GetAsync($"/api/v1/catalogs/mangafox?sortType=rating&page={_random.Next(1, 10)}");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should()
                .Be(70);
        }
    }
}