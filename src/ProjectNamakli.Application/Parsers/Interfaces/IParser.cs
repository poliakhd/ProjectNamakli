// <copyright file="IParser.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>19/06/2019 10:44 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectNamakli.Domain.Models.Interfaces;
using ProjectNamakli.Domain.Types;

namespace ProjectNamakli.Application.Parsers.Interfaces
{
    /// <summary>
    /// Site parser interface
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Gets or sets base url
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Asynchronous method for getting available sort options
        /// </summary>
        /// <returns>List of sorts options</returns>
        Task<IEnumerable<ISort>> GetSortOptionsAsync();

        /// <summary>
        /// Asynchronous method for getting available genres
        /// </summary>
        /// <returns>List of genres</returns>
        Task<IEnumerable<IGenre>> GetGenresAsync();

        /// <summary>
        /// Asynchronous method for getting catalog content
        /// </summary>
        /// <param name="sortType">Type of sort</param>
        /// <param name="page">Page number</param>
        /// <returns>List of manga</returns>
        Task<IEnumerable<IMangaPreview>> GetCatalogContentAsync(SortType sortType, int page);

        /// <summary>
        /// Asynchronous method for getting genre content
        /// </summary>
        /// <param name="sortType">Type of sort</param>
        /// <param name="genre">Genre id</param>
        /// <param name="page">Page number</param>
        /// <returns>List of manga</returns>
        Task<IEnumerable<IMangaPreview>> GetGenreContentAsync(SortType sortType, string genre, int page);

        /// <summary>
        /// Asynchronous method for getting manga content
        /// </summary>
        /// <param name="id">Manga id</param>
        /// <returns>Manga content</returns>
        Task<IManga> GetMangaContentAsync(string id);

        /// <summary>
        /// Asynchronous method for getting manga chapter images
        /// </summary>
        /// <param name="id">Manga id</param>
        /// <param name="volume">Volume</param>
        /// <param name="chapter">Chapter</param>
        /// <returns>List of manga chapter images</returns>
        Task<IMangaPages> GetMangaChapterContentAsync(string id, string volume, string chapter);
    }
}