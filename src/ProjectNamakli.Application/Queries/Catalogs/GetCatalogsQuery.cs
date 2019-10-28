// <copyright file="GetCatalogsQuery.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 7:50 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProjectNamakli.Application.Models;
using ProjectNamakli.Domain.Models.Interfaces;

namespace ProjectNamakli.Application.Queries.Catalogs
{
    /// <summary>
    /// Get catalogs query
    /// </summary>
    public class GetCatalogsQuery : IRequestHandler<GetCatalogsCriterion, IEnumerable<ICatalog>>
    {
        /// <inheritdoc />
        public async Task<IEnumerable<ICatalog>> Handle(GetCatalogsCriterion criterion, CancellationToken cancellationToken)
        {
            var catalogs = new[]
            {
                new Catalog
                {
                    Id = "mangafox",
                    Url = "http://fanfox.net/",
                    Title = "MangaFox",
                    Icon = "http://fanfox.net/apple-touch-icon.png"
                },
                new Catalog
                {
                    Id = "readmanga",
                    Url = "http://readmanga.me/",
                    Title = "ReadManga",
                    Icon = "http://res.readmanga.me/static/apple-touch-icon-a401a05b79c2dad93553ebc3523ad5fe.png"
                },
                new Catalog
                {
                    Id = "mintmanga",
                    Url = "http://mintmanga.com/",
                    Title = "MintManga",
                    Icon = "http://res.mintmanga.com/static/apple-touch-icon-a401a05b79c2dad93553ebc3523ad5fe.png"
                }
            };

            return await Task.FromResult(catalogs);
        }
    }
}