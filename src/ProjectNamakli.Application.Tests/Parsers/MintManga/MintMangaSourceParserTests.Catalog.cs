// <copyright file="MintMangaSourceParserTests.Catalog.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>20/06/2019 12:06 AM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using ProjectNamakli.Domain.Types;
using Xunit;

namespace ProjectNamakli.Infrastructure.Tests.Parsers.MintManga
{
    public partial class MintMangaSourceParserTests
    {
        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetCatalogContent -> New")]
        public async Task GetCatalogContentAsync_New_ShouldNotBeNull()
        {
            try
            {
                var catalogContent = await _parser.GetCatalogContentAsync(SortType.New, 0);
    
                catalogContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            }
        }

        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetCatalogContent -> Popular")]
        public async Task GetCatalogContentAsync_Popular_ShouldNotBeNull()
        {
            try
            {
                var catalogContent = await _parser.GetCatalogContentAsync(SortType.Popular, 0);
    
                catalogContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            }
        }

        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetCatalogContent -> Rating")]
        public async Task GetCatalogContentAsync_Rating_ShouldNotBeNull()
        {
            try
            {
                var catalogContent = await _parser.GetCatalogContentAsync(SortType.Rating, 0);
    
                catalogContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            }
        }

        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetCatalogContent -> Update")]
        public async Task GetCatalogContentAsync_Update_ShouldNotBeNull()
        {
            try
            {
                var catalogContent = await _parser.GetCatalogContentAsync(SortType.Update, 0);
    
                catalogContent.Should()
                    .NotBeNull();    
            }
            catch (HttpRequestException)
            {
            }
        }
    }
}