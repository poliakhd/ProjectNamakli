// <copyright file="MangaFoxSourceParserTests.Manga.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>20/06/2019 12:05 AM</date>
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
using ProjectNamakli.Domain.Models.Interfaces;
using ProjectNamakli.Domain.Types;
using Xunit;

namespace ProjectNamakli.Infrastructure.Tests.Parsers.MangaFox
{
    public partial class MangaFoxSourceParserTests
    {
        [Fact(DisplayName = "Infrastructure -> MangaFoxSourceParser -> GetMangaContent")]
        public async Task GetMangaContentAsync_ShouldNotBeNull()
        {
            try
            {
                var catalogContent = await _parser.GetCatalogContentAsync(SortType.Rating, 0);
                var mangaContent = (await _parser.GetMangaContentAsync(catalogContent.FirstOrDefault().To<IMangaPreview>().Id)).To<IManga>();

                mangaContent.Should()
                    .NotBeNull();
            }
            catch (HttpRequestException)
            {
            }
        }
        
        [Fact(DisplayName = "Infrastructure -> MangaFoxSourceParser -> GetMangaChapterContent")]
        public async Task GetMangaChapterContentAsync_ShouldNotBeNull()
        {
            try
            {
                var catalogContent = await _parser.GetCatalogContentAsync(SortType.Rating, 0);

                var mangaPreviewContent = catalogContent.FirstOrDefault().To<IMangaPreview>();
                var mangaContent = (await _parser.GetMangaContentAsync(mangaPreviewContent.Id)).To<IManga>();
                
                var mangaChapter = mangaContent.Chapters.FirstOrDefault().To<IChapter>();
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