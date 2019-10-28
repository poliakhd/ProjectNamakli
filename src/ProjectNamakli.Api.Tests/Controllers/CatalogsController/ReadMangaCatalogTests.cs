// <copyright file="ReadMangaCatalogTests.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>21/06/2019 10:37 PM</date>
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
    public class ReadMangaCatalogTests : BaseApiTests
    {
        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        
        public ReadMangaCatalogTests(TestContext context)
            : base(context)
        {
        }

        [Fact(DisplayName = "Integration -> ReadMangaCatalog -> GetCatalogs")]
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

        [Fact(DisplayName = "Integration -> ReadMangaCatalog -> GetCatalogContent")]
        public async void GetCatalogContentAsync_Popular()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/catalogs/readmanga");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should().Be(70);
        }

        [Fact(DisplayName = "Integration -> ReadMangaCatalog -> GetCatalogContent -> Update")]
        public async void GetCatalogContentAsync_Update()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/catalogs/readmanga?sortType=update");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should().Be(70);
        }
        
        [Fact(DisplayName = "Integration -> ReadMangaCatalog -> GetCatalogContent -> Rating")]
        public async void GetCatalogContentAsync_Rating()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/catalogs/readmanga?sortType=rating");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should().Be(70);
        }
        
        [Fact(DisplayName = "Integration -> ReadMangaCatalog -> Page -> GetCatalogContent")]
        public async void GetCatalogContentAsync_Page_Popular()
        {
            var result = await Context.Client
                .GetAsync($"/api/v1/catalogs/readmanga?page={_random.Next(1, 10)}");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should()
                .Be(70);
        }

        [Fact(DisplayName = "Integration -> ReadMangaCatalog -> GetCatalogContent -> Page -> Update")]
        public async void GetCatalogContentAsync_Page_Update()
        {   
            var result = await Context.Client
                .GetAsync($"/api/v1/catalogs/readmanga?sortType=update&page={_random.Next(1, 10)}");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should()
                .Be(70);
        }
        
        [Fact(DisplayName = "Integration -> ReadMangaCatalog -> GetCatalogContent -> Page -> Rating")]
        public async void GetCatalogContentAsync_Page_Rating()
        {
            var result = await Context.Client
                .GetAsync($"/api/v1/catalogs/readmanga?sortType=rating&page={_random.Next(1, 10)}");

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