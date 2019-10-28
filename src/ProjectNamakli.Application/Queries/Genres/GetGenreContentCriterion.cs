// <copyright file="GetGenreContentCriterion.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 8:01 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Collections.Generic;
using MediatR;
using ProjectNamakli.Domain.Models.Interfaces;

namespace ProjectNamakli.Application.Queries.Genres
{
    /// <summary>
    /// Get genre content criterion
    /// </summary>
    public class GetGenreContentCriterion : IRequest<IEnumerable<IMangaPreview>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGenreContentCriterion" /> class
        /// </summary>
        /// <param name="catalog">Catalog name</param>
        /// <param name="genre">Genre name</param>
        /// <param name="sort">Type of sort</param>
        /// <param name="page">Page number</param>
        public GetGenreContentCriterion(string catalog, string genre, string sort, int page)
        {
            Catalog = catalog;
            Genre = genre;
            Sort = sort;
            Page = page;
        }

        /// <summary>
        /// Gets catalog name
        /// </summary>
        public string Catalog { get; }

        /// <summary>
        /// Gets genre name
        /// </summary>
        public string Genre { get; }

        /// <summary>
        /// Gets sort type
        /// </summary>
        public string Sort { get; }

        /// <summary>
        /// Gets page number
        /// </summary>
        public int Page { get; }
    }
}