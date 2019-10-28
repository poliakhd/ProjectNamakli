// <copyright file="ReadMangaParser.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>19/06/2019 11:22 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    /// ReadManga site parser
    /// </summary>
    public class ReadMangaParser
        : IParser
    {
        private readonly Regex _genreIdRegex = new Regex(@"genre/(\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly Regex _mangaChapterUrlsRegex = new Regex(@"\[(.*?)\]", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly Regex _mangaViewsRegex = new Regex(@"Просмотров: (\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly Regex _mangaVolumeRegex = new Regex(@"(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private readonly Regex _mangaGenreRegex = new Regex(@"genre/(\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly Regex _mangaAuthorTranslatorRegex = new Regex(@"person/(\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly Regex _mangaChaptersRegex = new Regex(@"/(\w+)/(\w+)/(\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly HttpClient _client;

        private readonly HtmlParser _parser = new HtmlParser();

        private readonly ILogger _logger;

        private readonly string _genresUrl;

        private readonly string _mangaUrl;

        private readonly string _mangaChapterUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMangaParser" /> class
        /// </summary>
        /// <param name="logger">Instance of <see cref="ILogger"/></param>
        /// <param name="httpClientFactory">Instance of <see cref="IMultiton{TKey,TValue}"/></param>
        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor", Justification = "Reviewed")]
        public ReadMangaParser(
            ILogger<ReadMangaParser> logger,
            IMultiton<string, HttpClient> httpClientFactory)
        {
            _logger = logger;
            _client = httpClientFactory.GetInstance(BaseUrl);

            _genresUrl = $"{BaseUrl}/list/genres/sort_name";
            _mangaUrl = $"{BaseUrl}/{{0}}";
            _mangaChapterUrl = $"{BaseUrl}/{{0}}/{{1}}/{{2}}?mtr=1";
        }

        /// <inheritdoc/>
        public virtual string BaseUrl { get; set; } = "http://readmanga.me";

        /// <inheritdoc/>
        public async Task<IEnumerable<ISort>> GetSortOptionsAsync()
        {
            var sorts = new[]
            {
                new Sort { Id = SortType.Update.ToString().ToLower(), Type = "Latest Chapters" },
                new Sort { Id = SortType.Rating.ToString().ToLower(), Type = "Rating" },
                new Sort { Id = SortType.Popular.ToString().ToLower(), Type = "Popularity" },
                new Sort { Id = SortType.New.ToString().ToLower(), Type = "New" }
            };

            return await Task.FromResult(sorts);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<IGenre>> GetGenresAsync()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, _genresUrl))
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var html = await response.GetContent();

                using (var htmlDocument = await _parser.ParseDocumentAsync(html))
                {
                    var htmlGenres = htmlDocument.QuerySelectorAll(".table.table-hover tbody tr td a");

                    return htmlGenres?.Select(
                        x =>
                        {
                            var genreUrl = x.GetAttribute("href");

                            _genreIdRegex.Match(genreUrl.EmptyStringIfNull())
                                .GetGroupValue(1, out var id);

                            return new Genre
                            {
                                Id = id,
                                Title = x.TextContent,
                                Url = $"{BaseUrl}{genreUrl}"
                            };
                        });
                }
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<IMangaPreview>> GetCatalogContentAsync(SortType sortType, int page)
        {
            return await ParseCatalogAsync(BuildCatalogContentUrl(sortType, page));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<IMangaPreview>> GetGenreContentAsync(SortType sortType, string url, int page)
        {
            return await ParseCatalogAsync(BuildGenreContentUrl(sortType, url, page));
        }

        /// <inheritdoc/>
        public async Task<IManga> GetMangaContentAsync(string id)
        {
            var url = string.Format(_mangaUrl, id);

            var parsedManga = new Manga();

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var htmlContent = await response.GetContent();
                using (var htmlDocument = await _parser.ParseDocumentAsync(htmlContent))
                {
                    var htmlManga = htmlDocument.QuerySelector(".leftContent");
                    if (htmlManga == null)
                    {
                        return null;
                    }

                    GetInformation(htmlManga, parsedManga);

                    var information = htmlManga.QuerySelectorAll(".subject-meta.col-sm-7 p");

                    if (information.Any())
                    {
                        GetVolume(information, parsedManga);
                        GetViews(information, parsedManga);

                        foreach (var informationLine in information.Skip(2))
                        {
                            if (GetGenres(informationLine, parsedManga))
                            {
                                continue;
                            }

                            if (GetAuthors(informationLine, parsedManga))
                            {
                                continue;
                            }

                            if (GetTranslators(informationLine, parsedManga))
                            {
                                continue;
                            }

                            GetPublishedYear(informationLine, parsedManga);
                        }
                    }

                    GetChapters(htmlManga, parsedManga);
                }
            }

            return parsedManga;
        }

        /// <inheritdoc/>
        public async Task<IMangaPages> GetMangaChapterContentAsync(string id, string volume, string chapter)
        {
            var url = string.Format(_mangaChapterUrl, id, volume, chapter);

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var html = await response.GetContent();

                using (var htmlDocument = await _parser.ParseDocumentAsync(html))
                {
                    var htmlChapterImages = htmlDocument
                        .QuerySelectorAll("script")
                        .FirstOrDefault(x => x.TextContent.Contains("rm_h.init"));

                    if (htmlChapterImages == null)
                    {
                        return null;
                    }

                    var htmlImages = htmlChapterImages.TextContent
                        .Substring(htmlChapterImages.TextContent.IndexOf("rm_h.init", StringComparison.InvariantCulture));

                    var imagesDto = new List<Uri>();

                    foreach (Match match in _mangaChapterUrlsRegex.Matches(htmlImages))
                    {
                        match.GetGroupValue(1, out var image);

                        if (string.IsNullOrEmpty(image))
                        {
                            continue;
                        }

                        var imageAttributes = image
                            .Replace("\'", string.Empty)
                            .Replace("\"", string.Empty)
                            .Split(',');

                        imagesDto.Add(new Uri($@"{imageAttributes[1]}{imageAttributes[2]}"));
                    }

                    return new MangaPages() { Images = imagesDto };
                }
            }
        }

        /// <summary>
        /// Method with common catalog site page parsing logic
        /// </summary>
        /// <param name="url">Url of catalog site page to parse</param>
        /// <returns>List of manga</returns>
        private async Task<IEnumerable<IMangaPreview>> ParseCatalogAsync(string url)
        {
            var mangaPreviewList = new List<MangaPreview>();

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var html = await response.GetContent();

                using (var htmlDocument = await _parser.ParseDocumentAsync(html))
                {
                    var htmlMangaList = htmlDocument.QuerySelectorAll(".tile.col-sm-6");

                    if (!htmlMangaList.Any())
                    {
                        _logger.LogInformation($"There were no content for catalog: {url}");
                        return null;
                    }

                    foreach (var htmlManga in htmlMangaList)
                    {
                        var mangaUrl = htmlManga.QuerySelector(".img a")?.GetAttribute("href");
                        var mangaCoverUrl = htmlManga.QuerySelector(".img a img")?.GetAttribute("data-original");

                        var mangaPreviewDto = new MangaPreview()
                        {
                            Id = mangaUrl?.Replace("/", string.Empty),
                            Title =
                                htmlManga.QuerySelector("h4")?.TextContent
                                    .Replace("\n", string.Empty)
                                    .TrimStart(' ')
                                    .TrimEnd(' '),
                            Url = $@"{BaseUrl}{mangaUrl}",
                            Covers = new[] { mangaCoverUrl }
                        };

                        var htmlRatings = htmlManga.QuerySelector(".desc .rating")?.GetAttribute("title").Split(' ');

                        if (htmlRatings != null)
                        {
                            mangaPreviewDto.Rating = float.Parse(htmlRatings[0]);
                            mangaPreviewDto.RatingMax = float.Parse(htmlRatings[2]);
                        }

                        mangaPreviewList.Add(mangaPreviewDto);
                    }
                }
            }

            return mangaPreviewList;
        }

        /// <summary>
        /// Method for building url awaits in <see cref="ParseCatalogAsync"/> method
        /// </summary>
        /// <param name="sortType">Type of sort</param>
        /// <param name="page">Page number</param>
        /// <returns>Built url</returns>
        private string BuildCatalogContentUrl(SortType sortType, int page)
        {
            var urlTemplate = $"{BaseUrl}/list{{0}}{(page > 0 ? $"&offset={70 * page}&max=70" : string.Empty)}";

            switch (sortType)
            {
                case SortType.New:
                    urlTemplate = string.Format(urlTemplate, "?sortType=created");
                    break;
                case SortType.Popular:
                    urlTemplate = string.Format(urlTemplate, "?sortType=rate");
                    break;
                case SortType.Rating:
                    urlTemplate = string.Format(urlTemplate, "?sortType=votes");
                    break;
                case SortType.Update:
                    urlTemplate = string.Format(urlTemplate, "?sortType=updated");
                    break;
            }

            return urlTemplate;
        }

        /// <summary>
        /// Method for building url awaits in <see cref="ParseCatalogAsync"/> method
        /// </summary>
        /// <param name="sortType">Type of sort</param>
        /// <param name="url">Url part</param>
        /// <param name="page">Page number</param>
        /// <returns>Built url</returns>
        private string BuildGenreContentUrl(SortType sortType, string url, int page)
        {
            var urlTemplate = $"{BaseUrl}/list/genre/{url}{{0}}{(page > 0 ? $"{url}&offset={70 * page}&max=70" : string.Empty)}";

            switch (sortType)
            {
                case SortType.New:
                    urlTemplate = string.Format(urlTemplate, "?sortType=created");
                    break;
                case SortType.Popular:
                    urlTemplate = string.Format(urlTemplate, "?sortType=rate");
                    break;
                case SortType.Rating:
                    urlTemplate = string.Format(urlTemplate, "?sortType=votes");
                    break;
                case SortType.Update:
                    urlTemplate = string.Format(urlTemplate, "?sortType=updated");
                    break;
            }

            return urlTemplate;
        }

        /// <summary>
        /// Method for gets main information for manga
        /// </summary>
        /// <param name="htmlManga">Html manga representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        private void GetInformation(IElement htmlManga, IManga parsedManga)
        {
            parsedManga.Id =
                htmlManga.QuerySelector("meta[itemprop=url]")?.GetAttribute("content").Replace(BaseUrl + @"/", string.Empty);

            parsedManga.Title = htmlManga.QuerySelector(".names .name")?.TextContent;

            parsedManga.AlternateTitles = new[]
            {
                htmlManga.QuerySelector(".names .eng-name")?.TextContent,
                htmlManga.QuerySelector(".names .original-name")?.TextContent
            }.Where(x => x != null);

            parsedManga.Description = htmlManga.QuerySelector("meta[itemprop=description]")?.GetAttribute("content");

            parsedManga.Covers =
                htmlManga.QuerySelectorAll(".picture-fotorama img")?.Select(x => x.GetAttribute("data-full"));

            parsedManga.Rating =
                float.Parse(htmlManga.QuerySelector(".user-rating meta[itemprop=ratingValue]")?.GetAttribute("content"));
        }

        /// <summary>
        /// Method for gets volume information for manga
        /// </summary>
        /// <param name="information">Html manga information representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        private void GetVolume(IHtmlCollection<IElement> information, IManga parsedManga)
        {
            _mangaVolumeRegex.Match(information[0].TextContent.EmptyStringIfNull())
                .GetGroupValue(1, out var textVolumes);

            parsedManga.Volumes = textVolumes;
        }

        /// <summary>
        /// Method for gets views information for manga
        /// </summary>
        /// <param name="information">Html manga information representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        private void GetViews(IHtmlCollection<IElement> information, IManga parsedManga)
        {
            if (information.Length < 6)
            {
                parsedManga.Views = 0;
                return;
            }

            _mangaViewsRegex.Match(information[2].TextContent.EmptyStringIfNull())
                .GetGroupValue(1, out var textViews);

            int.TryParse(textViews, out var views);

            parsedManga.Views = views;
        }

        /// <summary>
        /// Method for get genre information for manga
        /// </summary>
        /// <param name="informationLine">Html manga information line representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        /// <returns>Returns <value>true</value> if action was succeed; otherwise returns <value>false</value>.</returns>
        private bool GetGenres(IElement informationLine, IManga parsedManga)
        {
            if (informationLine.QuerySelector(".elem_genre") != null)
            {
                parsedManga.Genres = informationLine
                    .QuerySelectorAll(".elem_genre a")
                    .Select(x =>
                    {
                        var url = x.GetAttribute("href");

                        _mangaGenreRegex.Match(url.EmptyStringIfNull())
                            .GetGroupValue(1, out var id);

                        return new Genre
                        {
                            Id = id,
                            Title = x.TextContent,
                            Url = $@"{BaseUrl}{url}"
                        };
                    });

                return true;
            }

            return false;
        }

        /// <summary>
        /// Method for gets author information for manga
        /// </summary>
        /// <param name="informationLine">Html manga information line representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        /// <returns>Returns <value>true</value> if action was succeed; otherwise returns <value>false</value>.</returns>
        private bool GetAuthors(IElement informationLine, IManga parsedManga)
        {
            if (informationLine.QuerySelector(".elem_author") != null)
            {
                parsedManga.Authors = informationLine
                    .QuerySelectorAll(".elem_author a")
                    .Select(x =>
                    {
                        var url = x.GetAttribute("href");

                        _mangaAuthorTranslatorRegex.Match(url.EmptyStringIfNull())
                            .GetGroupValue(1, out var id);

                        return new Author
                        {
                            Id = id,
                            Name = x.TextContent,
                            Url = $@"{BaseUrl}{url}"
                        };
                    });

                return true;
            }

            return false;
        }

        /// <summary>
        /// Method for gets translator information for manga
        /// </summary>
        /// <param name="informationLine">Html manga information line representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        /// <returns>Returns <value>true</value> if action was succeed; otherwise returns <value>false</value>.</returns>
        private bool GetTranslators(IElement informationLine, IManga parsedManga)
        {
            if (informationLine.QuerySelector(".elem_translator") != null)
            {
                parsedManga.Translators =
                    informationLine.QuerySelectorAll(".elem_translator a")
                        .Where(x => !string.IsNullOrEmpty(x.TextContent))
                        .Select(x =>
                        {
                            var url = x.GetAttribute("href");

                            _mangaAuthorTranslatorRegex.Match(url.EmptyStringIfNull())
                                .GetGroupValue(1, out var id);

                            return new Translator
                            {
                                Id = id,
                                Name = x.TextContent,
                                Url = $@"{BaseUrl}{url}"
                            };
                        });

                return true;
            }

            return false;
        }

        /// <summary>
        /// Method for gets published year information for manga
        /// </summary>
        /// <param name="informationLine">Html manga information line representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        private void GetPublishedYear(IElement informationLine, IManga parsedManga)
        {
            if (informationLine.QuerySelector(".elem_year") != null)
            {
                parsedManga.Published = int.Parse(informationLine.QuerySelector(".elem_year a")?.TextContent);
            }
        }

        /// <summary>
        /// Method for gets chapter information for manga
        /// </summary>
        /// <param name="htmlManga">Html manga representation</param>
        /// <param name="parsedManga">Object manga representation</param>
        private void GetChapters(IElement htmlManga, IManga parsedManga)
        {
            var chapters = new List<IChapter>();
            var htmlChapters = htmlManga.QuerySelectorAll(".expandable.chapters-link tbody tr");

            foreach (var htmlChapter in htmlChapters)
            {
                var htmlChapterInfo = htmlChapter.QuerySelectorAll("td");

                if (htmlChapterInfo.Any())
                {
                    var chapterUrl = htmlChapterInfo[0].QuerySelector("a")?.GetAttribute("href");

                    _mangaChaptersRegex.Match(chapterUrl.EmptyStringIfNull())
                        .GetGroupValue(2, out var volume)
                        .GetGroupValue(3, out var chapter);

                    chapters.Add(
                        new Chapter
                        {
                            Id = chapter,
                            Volume = volume,
                            Name = Regex.Replace(htmlChapterInfo[0].QuerySelector("a")?.TextContent ?? string.Empty, @"\s{2,}", " ").Trim(),
                            Url = $@"{BaseUrl}{chapterUrl}",
                            Date = htmlChapterInfo[1].TextContent.Replace("\n", string.Empty).Replace(" ", string.Empty)
                        });
                }
            }

            parsedManga.Chapters = chapters;
        }
    }
}