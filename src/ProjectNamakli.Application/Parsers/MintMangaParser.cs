// <copyright file="MintMangaParser.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>19/06/2019 11:52 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Net.Http;
using Microsoft.Extensions.Logging;
using ProjectNamakli.Application.Helpers.Interfaces;
using ProjectNamakli.Application.Parsers.Interfaces;

namespace ProjectNamakli.Application.Parsers
{
    /// <summary>
    /// MintManga site parser
    /// </summary>
    public class MintMangaParser
        : ReadMangaParser, IParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MintMangaParser" /> class
        /// </summary>
        /// <param name="logger">Instance of <see cref="ILogger"/></param>
        /// <param name="httpClientFactory">Instance of <see cref="IMultiton{TKey,TValue}"/></param>
        public MintMangaParser(
            ILogger<MintMangaParser> logger,
            IMultiton<string, HttpClient> httpClientFactory)
            : base(logger, httpClientFactory)
        {
        }

        /// <inheritdoc />
        public override string BaseUrl { get; set; } = "http://mintmanga.com";
    }
}