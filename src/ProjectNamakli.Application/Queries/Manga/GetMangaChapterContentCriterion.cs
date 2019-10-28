// <copyright file="GetMangaChapterContentCriterion.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 8:07 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using MediatR;
using ProjectNamakli.Domain.Models.Interfaces;

namespace ProjectNamakli.Application.Queries.Manga
{
    /// <summary>
    /// Get manga chapter criterion
    /// </summary>
    public class GetMangaChapterContentCriterion : IRequest<IMangaPages>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMangaChapterContentCriterion" /> class
        /// </summary>
        /// <param name="catalog">Catalog name</param>
        /// <param name="manga">Manga id</param>
        /// <param name="chapter">Chapter id</param>
        /// <param name="volume">Volume id</param>
        public GetMangaChapterContentCriterion(string catalog, string manga, string chapter, string volume)
        {
            Catalog = catalog;
            Manga = manga;
            Chapter = chapter;
            Volume = volume;
        }

        /// <summary>
        /// Gets or sets catalog name
        /// </summary>
        public string Catalog { get; set; }

        /// <summary>
        /// Gets or sets manga
        /// </summary>
        public string Manga { get; set; }

        /// <summary>
        /// Gets or sets chapter
        /// </summary>
        public string Chapter { get; set; }

        /// <summary>
        /// Gets or sets volume
        /// </summary>
        public string Volume { get; set; }
    }
}