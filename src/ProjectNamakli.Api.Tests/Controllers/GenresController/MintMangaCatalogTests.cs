// <copyright file="MintMangaCatalogTests.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>21/06/2019 10:38 PM</date>
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
using Xunit;

namespace ProjectNamakli.Api.Tests.Controllers.GenresController
{
    [Collection("ApiTestsCollection")]
    public class MintMangaGenreTests : BaseApiTests
    {
        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        
        public MintMangaGenreTests(TestContext context)
            : base(context)
        {
        }

        [Fact(DisplayName = "Integration -> MintMangaGenre -> GetGenres")]
        public async void GetGenresAsync_Popular()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/genres/mintmanga");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<Genre>>(await result.GetContent());

            collection.Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "Integration -> MintMangaGenre -> GetGenreContent")]
        public async void GetGenreContentAsync_Popular()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/genres/mintmanga");
            
            var genres =
                JsonConvert.DeserializeObject<IList<Genre>>(await result.GetContent());

            result = await Context.Client
                .GetAsync($"/api/v1/genres/mintmanga/{genres[_random.Next(0, genres.Count)].Id}");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());
            
            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should();
        }

        [Fact(DisplayName = "Integration -> MintMangaGenre -> GetGenreContent -> Update")]
        public async void GetCatalogContentAsync_Update()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/genres/mintmanga");
            
            var genres =
                JsonConvert.DeserializeObject<IList<Genre>>(await result.GetContent());

            result = await Context.Client
                .GetAsync($"/api/v1/genres/mintmanga/{genres[_random.Next(0, genres.Count)].Id}?sortType=update");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());
            
            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should();
        }
        
        [Fact(DisplayName = "Integration -> MintMangaGenre -> GetGenreContent -> Rating")]
        public async void GetCatalogContentAsync_Rating()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/genres/mintmanga");
            
            var genres =
                JsonConvert.DeserializeObject<IList<Genre>>(await result.GetContent());

            result = await Context.Client
                .GetAsync($"/api/v1/genres/mintmanga/{genres[_random.Next(0, genres.Count)].Id}?sortType=rating");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());
            
            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should();
        }
        
        [Fact(DisplayName = "Integration -> MintMangaGenre -> GetGenreContent -> Page")]
        public async void GetCatalogContentAsync_Page_Popular()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/genres/mintmanga");
            
            var genres =
                JsonConvert.DeserializeObject<IList<Genre>>(await result.GetContent());

            result = await Context.Client
                .GetAsync($"/api/v1/genres/mintmanga/{genres[_random.Next(0, genres.Count)].Id}?page={_random.Next(0, 1)}");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());
            
            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should();
        }

        [Fact(DisplayName = "Integration -> MintMangaGenre -> GetGenreContent -> Page -> Update")]
        public async void GetCatalogContentAsync_Page_Update()
        {   
            var result = await Context.Client
                .GetAsync("/api/v1/genres/mintmanga");
            
            var genres =
                JsonConvert.DeserializeObject<IList<Genre>>(await result.GetContent());

            result = await Context.Client
                .GetAsync($"/api/v1/genres/mintmanga/{genres[_random.Next(0, genres.Count)].Id}?sortType=update&page={_random.Next(0, 1)}");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());
            
            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should();
        }
        
        [Fact(DisplayName = "Integration -> MintMangaGenre -> GetGenreContent -> Page -> Rating")]
        public async void GetCatalogContentAsync_Page_Rating()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/genres/mintmanga");
            
            var genres =
                JsonConvert.DeserializeObject<IList<Genre>>(await result.GetContent());

            result = await Context.Client
                .GetAsync($"/api/v1/genres/mintmanga/{genres[_random.Next(0, genres.Count)].Id}?sortType=rating&page={_random.Next(0, 1)}");

            var collection =
                JsonConvert.DeserializeObject<IEnumerable<MangaPreview>>(await result.GetContent());
            
            collection.Should()
                .NotBeNull();
            
            collection.Count()
                .Should();
        }
    }
}