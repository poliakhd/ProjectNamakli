// <copyright file="GetMangaContentCriterion.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 8:05 PM</date>
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
    /// Get manga content criterion
    /// </summary>
    public class GetMangaContentCriterion : IRequest<IManga>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMangaContentCriterion" /> class
        /// </summary>
        /// <param name="catalog">Catalog name</param>
        /// <param name="manga">Manga id</param>
        public GetMangaContentCriterion(string catalog, string manga)
        {
            Catalog = catalog;
            Manga = manga;
        }

        /// <summary>
        /// Gets catalog name
        /// </summary>
        public string Catalog { get; }

        /// <summary>
        /// Gets manga id
        /// </summary>
        public string Manga { get; }
    }
}