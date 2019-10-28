// <copyright file="GetCatalogContentCriterion.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 7:53 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Collections.Generic;
using MediatR;
using ProjectNamakli.Domain.Models.Interfaces;

namespace ProjectNamakli.Application.Queries.Catalogs
{
    /// <summary>
    /// Get catalog content criterion
    /// </summary>
    public class GetCatalogContentCriterion : IRequest<IEnumerable<IMangaPreview>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCatalogContentCriterion"/> class.
        /// </summary>
        /// <param name="catalog">Catalog name</param>
        /// <param name="sort">Sort type</param>
        /// <param name="page">Page number</param>
        public GetCatalogContentCriterion(string catalog, string sort, int page)
        {
            Catalog = catalog;
            Sort = sort;
            Page = page;
        }

        /// <summary>
        /// Gets catalog id
        /// </summary>
        public string Catalog { get; }

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