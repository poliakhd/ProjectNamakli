// <copyright file="ReadMangaParserTests.Genre.cs" company="10Apps">
// Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>Daniil Poliakh</author>
// <date>21/07/2018 9:20 PM</date>
// <summary>
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using ProjectNamakli.Domain.Types;
using Xunit;

namespace ProjectNamakli.Infrastructure.Tests.Parsers.ReadManga
{
    public partial class ReadMangaParserTests
    {
        [Fact(DisplayName = "Infrastructure -> ReadMangaParser -> GetGenres")]
        public async Task GetGenresAsync_ShouldNotBeNull()
        {
            try
            {
                var genres = await _parser.GetGenresAsync();
    
                genres.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            } 
        }
        
        [Fact(DisplayName = "Infrastructure -> ReadMangaParser -> GetGenreContent -> New")]
        public async Task GetGenreContenttAsync_New_ShouldNotBeNull()
        {
            try
            {
                var genres = await _parser.GetGenresAsync();

                var catalogContent = await _parser.GetGenreContentAsync(SortType.New,
                    genres.ElementAt(_random.Next(0, genres.Count() - 1)).Id, 0);
    
                catalogContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            } 
        }

        [Fact(DisplayName = "Infrastructure -> ReadMangaParser -> GetGenreContent -> Popular")]
        public async Task GetGenreContenttAsync_Popular_ShouldNotBeNull()
        {
            try
            {
                var genres = await _parser.GetGenresAsync();
    
                var catalogContent = await _parser.GetGenreContentAsync(SortType.Popular,
                    genres.ElementAt(_random.Next(0, genres.Count() - 1)).Id, 0);
    
                catalogContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            } 
        }

        [Fact(DisplayName = "Infrastructure -> ReadMangaParser -> GetGenreContent -> Rating")]
        public async Task GetGenreContenttAsync_Rating_ShouldNotBeNull()
        {
            try
            {
                var genres = await _parser.GetGenresAsync();
    
                var catalogContent = await _parser.GetGenreContentAsync(SortType.Rating,
                    genres.ElementAt(_random.Next(0, genres.Count() - 1)).Id, 0);
    
                catalogContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            } 
        }

        [Fact(DisplayName = "Infrastructure -> ReadMangaParser -> GetGenreContent -> Update")]
        public async Task GetGenreContenttAsync_Update_ShouldNotBeNull()
        {
            try
            {
                var genres = await _parser.GetGenresAsync();
    
                var catalogContent = await _parser.GetGenreContentAsync(SortType.Update,
                    genres.ElementAt(_random.Next(0, genres.Count() - 1)).Id, 0);
    
                catalogContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            } 
        }
    }
}