// <copyright file="GetGenresQuery.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 7:59 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectNamakli.Application.Extensions;
using ProjectNamakli.Application.Parsers.Interfaces;
using ProjectNamakli.Domain.Models.Interfaces;
using ProjectNamakli.Domain.Types;

namespace ProjectNamakli.Application.Queries.Genres
{
    /// <summary>
    /// Get genres query
    /// </summary>
    public class GetGenresQuery : IRequestHandler<GetGenresCriterion, IEnumerable<IGenre>>
    {
        private readonly IParserFactory _parserFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetGenresQuery"/> class.
        /// </summary>
        /// <param name="parserFactory"><see cref="IParserFactory"/> instance</param>
        public GetGenresQuery(IParserFactory parserFactory)
        {
            _parserFactory = parserFactory;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IGenre>> Handle(GetGenresCriterion criterion, CancellationToken cancellationToken)
        {
            var catalog = criterion.Catalog.GetEnumValue<CatalogType>();

            return await _parserFactory.GetParser(catalog)
                .GetGenresAsync();
        }
    }
}