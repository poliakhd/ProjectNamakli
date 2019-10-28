// <copyright file="ReadMangaParserTests.Manga.cs" company="10Apps">
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
using ProjectNamakli.Application.Extensions;
using ProjectNamakli.Application.Models;
using ProjectNamakli.Domain.Types;
using Xunit;

namespace ProjectNamakli.Infrastructure.Tests.Parsers.ReadManga
{
    public partial class ReadMangaParserTests
    {
        [Fact(DisplayName = "Infrastructure -> ReadMangaParser -> GetMangaContent")]
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
        
        [Fact(DisplayName = "Infrastructure -> ReadMangaParser -> GetSortOptions")]
        public async Task GetMangaChapterContentAsync_ShouldNotBeNull()
        {
            try
            {
                var catalogContent = await _parser.GetCatalogContentAsync(SortType.Rating, 0);

                var mangaPreviewContent = catalogContent.FirstOrDefault().To<MangaPreview>();
                var mangaContent = (await _parser.GetMangaContentAsync(mangaPreviewContent.Id)).To<Manga>();
                
                var mangaChapter = mangaContent.Chapters.FirstOrDefault().To<Chapter>();
                var mangaChapterContent = await _parser.GetMangaChapterContentAsync(mangaContent.Id, mangaChapter.Volume, mangaChapter.Id);

                mangaChapterContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            }
        }
    }
}