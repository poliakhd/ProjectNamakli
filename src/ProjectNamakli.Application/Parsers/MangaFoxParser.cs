// <copyright file="MangaFoxParser.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>18/06/2019 7:16 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.Logging;
using ProjectNamakli.Application.Extensions;
using ProjectNamakli.Application.Helpers.Interfaces;
using ProjectNamakli.Application.Models;
using ProjectNamakli.Application.Parsers.Interfaces;
using ProjectNamakli.Domain.Models.Interfaces;
using ProjectNamakli.Domain.Types;

namespace ProjectNamakli.Application.Parsers
{
    /// <summary>
    /// MangaFox site parser
    /// </summary>
    public class MangaFoxParser
        : IParser
    {
        private readonly Regex _genreIdRegex = new Regex(@"directory/(\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly Regex _mangaCatalogIdRegex = new Regex(@"manga/(\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly Regex _chapterIdRegex = new Regex(@"c(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly Regex _chapterVolumeRegex = new Regex(@"v(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly Regex _mangaVolumeRegex = new Regex(@"Vol.(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly Regex _mangaAuthorIdRegex = new Regex(@"author/(.*)(?!$)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly HttpClient _client;

        private readonly HtmlParser _parser = new HtmlParser();

        private readonly ILogger<MangaFoxParser> _logger;

        private readonly string _genresUrl;

        private readonly string _mangaUrl;

        private readonly string _mangaChapterUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="MangaFoxParser" /> class.
        /// </summary>
        /// <param name="logger">Instance of <see cref="ILogger{TCategoryName}"/>.</param>
        /// <param name="httpClientFactory">Instance of <see cref="IMultiton{TKey,TValue}"/>.</param>
        public MangaFoxParser(
            ILogger<MangaFoxParser> logger,
            IMultiton<string, HttpClient> httpClientFactory)
        {
            _logger = logger;
            _client = httpClientFactory.GetInstance(BaseUrl);

            if (!_client.DefaultRequestHeaders.Contains("Cookie"))
            {
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", $"isAdult=1;");
            }

            _genresUrl = $"{BaseUrl}/directory/";
            _mangaUrl = $"{BaseUrl}/manga/{{0}}";
            _mangaChapterUrl = $"{BaseUrl}/manga/{{0}}{{1}}{{2}}{{3}}";
        }

        /// <inheritdoc/>
        public string BaseUrl { get; set; } = "http://fanfox.net";

        /// <inheritdoc />
        public async Task<IEnumerable<ISort>> GetSortOptionsAsync()
        {
            var sorts = new[]
            {
                new Sort { Id = SortType.Update.ToString().ToLower(), Type = "Latest Chapters" },
                new Sort { Id = SortType.Rating.ToString().ToLower(), Type = "Rating" },
                new Sort { Id = SortType.Popular.ToString().ToLower(), Type = "Popularity" }
            };

            return await Task.FromResult(sorts);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IGenre>> GetGenresAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_genresUrl, BaseUrl)))
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var html = await response.GetContent();

                using (var htmlDocument = await _parser.ParseDocumentAsync(html))
                {
                    var genres = htmlDocument.QuerySelectorAll(".browse-bar-filter-list > .container > ul > li > a");

                    if (genres == null)
                    {
                        _logger.LogInformation($"Genres were not found at {_genresUrl}");
                        return null;
                    }

                    return genres.Select(
                        x =>
                        {
                            var url = x.GetAttribute("href");
                            _genreIdRegex.Match(url).GetGroupValue(1, out var id);

                            return new Genre
                            {
                                Id = id,
                                Title = x.TextContent,
                                Url = $"{BaseUrl}{url}"
                            };
                        });
                }
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IMangaPreview>> GetCatalogContentAsync(SortType sortType, int page)
        {
            return await ParseCatalogAsync(BuildCatalogContentUrl(sortType, page));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IMangaPreview>> GetGenreContentAsync(SortType sortType, string url, int page)
        {
            return await ParseCatalogAsync(BuildGenreContentUrl(sortType, url, page));
        }

        /// <inheritdoc />
        public async Task<IManga> GetMangaContentAsync(string id)
        {
            var url = string.Format(_mangaUrl, id);

            var manga = new Manga();

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    var html = await response.GetContent();

                    using (var htmlDocument = await _parser.ParseDocumentAsync(html))
                    {
                        var mangaDetail = htmlDocument.QuerySelector(".detail-info");
                        var mangaChapters = htmlDocument.QuerySelectorAll(".detail-main-list > li > a");
                        var mangaLastChapter = mangaChapters.FirstOrDefault();

                        if (mangaDetail == null)
                        {
                            _logger.LogInformation($"There were no content for manga: {url}");
                            return null;
                        }

                        manga.Id = id.Substring(id.LastIndexOf('/') + 1);
                        manga.Url = $"/manga/{id}";

                        GetInformation(mangaDetail, manga);
                        GetVolume(mangaLastChapter, manga);
                        GetGenres(mangaDetail, manga);
                        GetAuthors(mangaDetail, manga);
                        GetChapters(mangaChapters, manga);
                    }
                }
            }

            return manga;
        }

        /// <inheritdoc />
        public async Task<IMangaPages> GetMangaChapterContentAsync(string id, string volume, string chapter)
        {
            var url = string.Format(_mangaChapterUrl, id, string.IsNullOrEmpty(volume) ? string.Empty : $"/{volume}", $"/{chapter}", "/{0}.html");

            var images = new Dictionary<int, Uri>();

            using (var request = new HttpRequestMessage(HttpMethod.Get, string.Format(url, "1")))
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var htmlContent = await response.GetContent();
                using (var htmlDocument = await _parser.ParseDocumentAsync(htmlContent))
                {
                    var maxPages = htmlDocument
                        .QuerySelectorAll(".pager-list-left > span > a")
                        .Where(x => x.GetAttribute("data-page") != null)
                        .Max(x => int.Parse(x.GetAttribute("data-page")));

                    if (maxPages == 0)
                    {
                        return null;
                    }

                    for (int i = 1; i <= maxPages; i++)
                    {
                        images.Add(i, null);
                    }

                    do
                    {
                        for (int i = 1; i <= maxPages; i++)
                        {
                            if (images[i] != null)
                            {
                                continue;
                            }

                            using (var imageRequest = new HttpRequestMessage(HttpMethod.Get, string.Format(url, i)))
                            using (var imageResponse = await _client.SendAsync(imageRequest))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    var imageHtmlContent = await imageResponse.GetContent();

                                    using (var imageSource = await _parser.ParseDocumentAsync(imageHtmlContent))
                                    {
                                        var imageSrc = imageSource
                                            .QuerySelector(".reader-main-img")?
                                            .GetAttribute("src");

                                        if (imageSrc == null)
                                        {
                                            _logger.LogError($"Cannot get image src from manga id = {id}; volume = {volume}; chapter = {chapter}");
                                            images[i] = new Uri(string.Empty);
                                        }
                                        else
                                        {
                                            images[i] = new Uri(imageSrc);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    while (images.Values.Any(x => x is null));
                }
            }

            return new MangaPages { Images = images.Values };
        }

        /// <summary>
        /// Method for building url awaits in <see cref="ParseCatalogAsync"/> method.
        /// </summary>
        /// <param name="sortType">Type of sort.</param>
        /// <param name="page">Page number.</param>
        /// <returns>Built url.</returns>
        private string BuildCatalogContentUrl(SortType sortType, int page)
        {
            page++;

            var urlTemplate = $"{BaseUrl}/directory/{{0}}{{1}}";

            switch (sortType)
            {
                case SortType.New:
                    throw new NotSupportedException(nameof(SortType.New));
                case SortType.Popular:
                    urlTemplate = string.Format(urlTemplate, page > 0 ? $"{page}.htm" : string.Empty, string.Empty);
                    break;
                case SortType.Rating:
                    urlTemplate = string.Format(urlTemplate, page > 0 ? $"{page}.htm" : string.Empty, "?rating");
                    break;
                case SortType.Update:
                    urlTemplate = string.Format(urlTemplate, page > 0 ? $"{page}.htm" : string.Empty, "?latest");
                    break;
            }

            return urlTemplate;
        }

        /// <summary>
        /// Method for building url awaits in <see cref="ParseCatalogAsync"/> method
        /// </summary>
        /// <param name="sortType">Type of sort</param>
        /// <param name="genre">Genre id</param>
        /// <param name="page">Page number</param>
        /// <returns>Built url</returns>
        private string BuildGenreContentUrl(SortType sortType, string genre, int page)
        {
            page++;

            var urlTemplate = $"{BaseUrl}/directory/{{0}}/{{1}}{{2}}";

            switch (sortType)
            {
                case SortType.New:
                    throw new NotSupportedException(nameof(SortType.New));
                case SortType.Popular:
                    urlTemplate = string.Format(urlTemplate, genre, page > 0 ? $"{page}.htm" : string.Empty, string.Empty);
                    break;
                case SortType.Rating:
                    urlTemplate = string.Format(urlTemplate, genre, page > 0 ? $"{page}.htm" : string.Empty, "?rating");
                    break;
                case SortType.Update:
                    urlTemplate = string.Format(urlTemplate, genre, page > 0 ? $"{page}.htm" : string.Empty, "?latest");
                    break;
            }

            return urlTemplate;
        }

        /// <summary>
        /// Method with common catalog site page parsing logic
        /// </summary>
        /// <param name="url">Url of catalog site page to parse</param>
        /// <returns>List of manga</returns>
        private async Task<IEnumerable<IMangaPreview>> ParseCatalogAsync(string url)
        {
            var mangaResultList = new List<MangaPreview>();

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var htmlContent = await response.GetContent();

                using (var htmlDocument = await _parser.ParseDocumentAsync(htmlContent))
                {
                    var htmlMangaList = htmlDocument.QuerySelectorAll(".manga-list-1-list.line li");

                    if (!htmlMangaList.Any())
                    {
                        _logger.LogInformation($"There were no content for catalog: {url}");
                        return null;
                    }

                    foreach (var htmlManga in htmlMangaList)
                    {
                        var mangaUrlControl = htmlManga.QuerySelector("a");
                        var mangaImgControl = htmlManga.QuerySelector("a img");
                        var mangaRatingControl = htmlManga.QuerySelector(".item-score")?.TextContent;

                        _mangaCatalogIdRegex
                            .Match(mangaUrlControl?.GetAttribute("href").TrimEnd('/').EmptyStringIfNull())
                            .GetGroupValue(1, out var id);

                        float.TryParse(mangaRatingControl, out var rating);

                        mangaResultList.Add(new MangaPreview()
                        {
                            Id = id,
                            Title = mangaUrlControl?.GetAttribute("title").Replace("\n", string.Empty).Trim(' '),
                            Url = mangaUrlControl?.GetAttribute("href").Replace(BaseUrl, string.Empty),
                            Covers = new[] { mangaImgControl?.GetAttribute("src") },
                            Rating = rating,
                            RatingMax = 5
                        });
                    }
                }
            }

            return mangaResultList;
        }

        /// <summary>
        /// Method for getting manga information
        /// </summary>
        /// <param name="htmlManga">Html manga representation</param>
        /// <param name="mangaDto">Object manga representation</param>
        private void GetInformation(IElement htmlManga, IManga mangaDto)
        {
            mangaDto.Title = htmlManga.QuerySelector(".detail-info-right-title-font")?.TextContent;
            mangaDto.AlternateTitles = null;
            mangaDto.Description = htmlManga.QuerySelector(".fullcontent")?.TextContent;
            mangaDto.Covers = htmlManga.QuerySelectorAll(".detail-info-cover-img")?.Select(x => x.GetAttribute("src"));
            mangaDto.Status = (StatusType)Enum.Parse(typeof(StatusType), htmlManga.QuerySelector(".detail-info-right-title-tip")?.TextContent);

            var htmlMangaRating = htmlManga.QuerySelector(".item-score")?.TextContent;

            if (float.TryParse(htmlMangaRating, out var rating))
            {
                mangaDto.Rating = rating;
            }

            mangaDto.RatingMax = 5;
        }

        /// <summary>
        ///     Method for getting manga volume information
        /// </summary>
        /// <param name="htmlManga">Html manga representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        private void GetVolume(IElement htmlManga, IManga parsedManga)
        {
            if (htmlManga == null)
            {
                parsedManga.Volumes = null;
                return;
            }

            var chapter = htmlManga.QuerySelector(".title3");
            var textLastVolume = chapter?.TextContent;

            if (string.IsNullOrEmpty(textLastVolume))
            {
                parsedManga.Volumes = "N/A";
                return;
            }

            if (textLastVolume.Contains("TBD"))
            {
                textLastVolume = "TBD";
            }

            _mangaVolumeRegex.Match(textLastVolume.EmptyStringIfNull())
                .GetGroupValue(1, out var lastVolume);

            parsedManga.Volumes = lastVolume;
        }

        /// <summary>
        /// Method for getting manga genres information
        /// </summary>
        /// <param name="htmlManga">Html manga representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        private void GetGenres(IElement htmlManga, IManga parsedManga)
        {
            var genres = htmlManga.QuerySelectorAll(".detail-info-right-tag-list a");

            parsedManga.Genres = genres
                    .Select(x =>
                    {
                        var genreUrl = x.GetAttribute("href");

                        _genreIdRegex.Match(genreUrl)
                            .GetGroupValue(1, out var id);

                        return new Genre()
                        {
                            Id = id,
                            Title = x.TextContent,
                            Url = genreUrl
                        };
                    });
        }

        /// <summary>
        /// Method for getting manga authors information
        /// </summary>
        /// <param name="htmlManga">Html manga representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        private void GetAuthors(IElement htmlManga, IManga parsedManga)
        {
            var authors = htmlManga.QuerySelectorAll(".detail-info-right-say a");

            parsedManga.Authors = authors
                    .Select(x =>
                    {
                        var authorUrl = x.GetAttribute("href");

                        _mangaAuthorIdRegex.Match(authorUrl)
                            .GetGroupValue(1, out var id);

                        return new Author()
                        {
                            Id = id,
                            Name = x.TextContent,
                            Url = authorUrl
                        };
                    });
        }

        /// <summary>
        /// Method for getting manga chapters information
        /// </summary>
        /// <param name="htmlManga">Html manga representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        private void GetChapters(IEnumerable<IElement> htmlManga, IManga parsedManga)
        {
            if (htmlManga == null)
            {
                return;
            }

            parsedManga.Chapters = htmlManga
                .Select(x =>
                {
                    var chapterUrl = x.GetAttribute("href");

                    _chapterIdRegex.Match(chapterUrl).GetValue(out var id);
                    _chapterVolumeRegex.Match(chapterUrl).GetValue(out var volume);

                    return new Chapter()
                    {
                        Id = id,
                        Volume = volume,
                        Name = x.GetAttribute("title"),
                        Url = chapterUrl,
                        Date = x.QuerySelector(".title2")?.TextContent
                    };
                });
        }
    }
}