// <copyright file="GetMangaChapterContentQuery.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 8:08 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectNamakli.Application.Extensions;
using ProjectNamakli.Application.Parsers.Interfaces;
using ProjectNamakli.Domain.Models.Interfaces;
using ProjectNamakli.Domain.Types;

namespace ProjectNamakli.Application.Queries.Manga
{
    /// <summary>
    /// Get manga chapter content query
    /// </summary>
    public class GetMangaChapterContentQuery : IRequestHandler<GetMangaChapterContentCriterion, IMangaPages>
    {
        private readonly IParserFactory _parserFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMangaChapterContentQuery"/> class.
        /// </summary>
        /// <param name="parserFactory"><see cref="IParserFactory"/> instance</param>
        public GetMangaChapterContentQuery(IParserFactory parserFactory)
        {
            _parserFactory = parserFactory;
        }

        /// <inheritdoc />
        public async Task<IMangaPages> Handle(GetMangaChapterContentCriterion criterion, CancellationToken cancellationToken)
        {
            var catalog = criterion.Catalog.GetEnumValue<CatalogType>();

            return await _parserFactory.GetParser(catalog)
                .GetMangaChapterContentAsync(criterion.Manga, criterion.Volume, criterion.Chapter);
        }
    }
}