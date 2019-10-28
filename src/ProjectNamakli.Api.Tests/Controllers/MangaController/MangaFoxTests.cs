﻿// <copyright file="MangaFoxTests.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>21/06/2019 10:39 PM</date>
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

namespace ProjectNamakli.Api.Tests.Controllers.MangaController
{
    [Collection("ApiTestsCollection")]
    public class MangaFoxTests : BaseApiTests
    {
        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        
        /// <inheritdoc />
        public MangaFoxTests(TestContext context) : base(context)
        {
        }

        [Fact(DisplayName = "Integration -> MangaFox -> GetManga")]
        public async void GetMangaAsync()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/catalogs/mangafox");

            var collection =
                JsonConvert.DeserializeObject<IList<MangaPreview>>(await result.GetContent());


            if(collection.Count == 0)
                return;

            result = await Context.Client
                .GetAsync($"/api/v1/manga/mangafox/{collection[_random.Next(0, collection.Count - 1)].Id}");

            var manga =
                JsonConvert.DeserializeObject<Manga>(await result.GetContent());
            
            manga.Should().NotBeNull();
        }
        
        [Fact(DisplayName = "Integration -> MangaFox -> GetMangaChapter")]
        public async void GetMangaChapterAsync()
        {
            var result = await Context.Client
                .GetAsync("/api/v1/catalogs/mangafox");

            var collection =
                JsonConvert.DeserializeObject<IList<MangaPreview>>(await result.GetContent());

            result = await Context.Client
                .GetAsync($"/api/v1/manga/mangafox/{collection[_random.Next(0, collection.Count - 1)].Id}");

            var manga =
                JsonConvert.DeserializeObject<Manga>(await result.GetContent());

            if (manga.Chapters == null)
                return;

            var chapters = manga.Chapters.ToList();

            var chapter = chapters[_random.Next(0, chapters.Count - 1)].As<Chapter>();
            
            result = await Context.Client
                .GetAsync($"/api/v1/manga/mangafox/{manga.Id}?volume={chapter.Volume}&chapter={chapter.Id}");

            var images = 
                JsonConvert.DeserializeObject<MangaPages>(await result.GetContent());
            
            images.Should().NotBeNull();
        }
    }
}