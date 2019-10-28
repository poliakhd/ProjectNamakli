// <copyright file="MintMangaSourceParserTests.Genre.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>20/06/2019 12:06 AM</date>
// <summary>
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

namespace ProjectNamakli.Infrastructure.Tests.Parsers.MintManga
{
    public partial class MintMangaSourceParserTests
    {
        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetGenres")]
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
        
        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetGenreContent -> New")]
        public async Task GetGenreContentAsync_New_ShouldNotBeNull()
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

        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetGenreContent -> Popular")]
        public async Task GetGenreContentAsync_Popular_ShouldNotBeNull()
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

        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetGenreContent -> Rating")]
        public async Task GetGenreContentAsync_Rating_ShouldNotBeNull()
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

        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetGenreContent -> Update")]
        public async Task GetGenreContentAsync_Update_ShouldNotBeNull()
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