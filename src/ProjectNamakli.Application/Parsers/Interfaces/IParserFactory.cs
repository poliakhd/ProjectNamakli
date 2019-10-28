// <copyright file="IParserFactory.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>22/06/2019 1:47 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using ProjectNamakli.Domain.Types;

namespace ProjectNamakli.Application.Parsers.Interfaces
{
    /// <summary>
    /// Parser factory interface
    /// </summary>
    public interface IParserFactory
    {
        /// <summary>
        /// Method for get parser for specified catalog
        /// </summary>
        /// <param name="catalogType">Catalog type</param>
        /// <returns><see cref="IParser"/> instance</returns>
        IParser GetParser(CatalogType catalogType);
    }
}