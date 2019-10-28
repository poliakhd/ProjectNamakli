// <copyright file="GetGenresCriterion.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 7:58 PM</date>
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
    /// Get genres criterion
    /// </summary>
    public class GetGenresCriterion : IRequest<IEnumerable<IGenre>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGenresCriterion" /> class
        /// </summary>
        /// <param name="catalog">Catalog name</param>
        public GetGenresCriterion(string catalog)
        {
            Catalog = catalog;
        }

        /// <summary>
        /// Gets catalog name
        /// </summary>
        public string Catalog { get; }
    }
}