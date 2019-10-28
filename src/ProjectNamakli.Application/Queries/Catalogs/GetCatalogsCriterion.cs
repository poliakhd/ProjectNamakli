// <copyright file="GetCatalogsCriterion.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>20/06/2019 9:34 PM</date>
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
    /// Get catalogs criterion
    /// </summary>
    public class GetCatalogsCriterion : IRequest<IEnumerable<ICatalog>>
    {
    }
}