// <copyright file="ParserFactory.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>22/06/2019 1:49 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System;
using Autofac;
using ProjectNamakli.Application.Parsers.Interfaces;
using ProjectNamakli.Domain.Types;

namespace ProjectNamakli.Application.Parsers
{
    /// <summary>
    /// Parser factory
    /// </summary>
    public class ParserFactory : IParserFactory
    {
        private readonly IComponentContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserFactory" /> class
        /// </summary>
        /// <param name="context"><see cref="IComponentContext"/> instance</param>
        public ParserFactory(IComponentContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public IParser GetParser(CatalogType catalogType)
        {
            switch (catalogType)
            {
                case CatalogType.ReadManga:
                    return _context.ResolveNamed<IParser>("ReadManga");
                case CatalogType.MintManga:
                    return _context.ResolveNamed<IParser>("MintManga");
                case CatalogType.MangaFox:
                    return _context.ResolveNamed<IParser>("MangaFox");
                default:
                    throw new ArgumentException(nameof(catalogType));
            }
        }
    }
}