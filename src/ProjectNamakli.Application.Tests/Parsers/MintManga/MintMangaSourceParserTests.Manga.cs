// <copyright file="MintMangaSourceParserTests.Manga.cs" company="10Apps">
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
using ProjectNamakli.Application.Extensions;
using ProjectNamakli.Application.Models;
using ProjectNamakli.Domain.Types;
using Xunit;

namespace ProjectNamakli.Infrastructure.Tests.Parsers.MintManga
{
    public partial class MintMangaSourceParserTests
    {
        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetMangaContent")]
        public async Task GetMangaContentAsync_ShouldNotBeNull()
        {
            try
            {
                var catalogContent = await _parser.GetCatalogContentAsync(SortType.Rating, 0);
                var mangaContent = (await _parser.GetMangaContentAsync(catalogContent.FirstOrDefault().To<MangaPreview>().Id)).To<Manga>();

                mangaContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            }
            
        }
        
        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetMangaChapterContent")]
        public async Task GetMangaChapterContentAsync_ShouldNotBeNull()
        {
            try
            {
                var catalogContent = await _parser.GetCatalogContentAsync(SortType.Rating, 0);

                var mangaPreviewContent = catalogContent.FirstOrDefault().To<MangaPreview>();
                var mangaContent = (await _parser.GetMangaContentAsync(mangaPreviewContent.Id)).To<Manga>();
                
                var mangaChapter = mangaContent.Chapters.FirstOrDefault().To<Chapter>();
                var mangaChapterContent = await _parser.GetMangaChapterContentAsync(mangaContent.Id, mangaChapter.Volume, $"{mangaChapter.Id}?mtr=1");

                mangaChapterContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            }
        }
    }
}