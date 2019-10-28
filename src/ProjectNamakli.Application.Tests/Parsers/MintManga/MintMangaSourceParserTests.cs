// <copyright file="MintMangaSourceParserTests.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>20/06/2019 12:06 AM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectNamakli.Application.Helpers;
using ProjectNamakli.Application.Parsers;
using ProjectNamakli.Application.Parsers.Interfaces;
using Xunit;

namespace ProjectNamakli.Infrastructure.Tests.Parsers.MintManga
{
    public partial class MintMangaSourceParserTests
    {
        private readonly IParser _parser =
           new MintMangaParser(Mock.Of<ILogger<MintMangaParser>>(), new HttpClientMultiton());

        private readonly Random _random = new Random();

        [Fact(DisplayName = "Infrastructure -> MintMangaSourceParser -> GetSortOptions")]
        public async Task GetSortOptionsAsync_ShouldBe4()
        {
            var sortOptions = await _parser.GetSortOptionsAsync();

            sortOptions.Count()
                .Should()
                .Be(4);
        }
    }
}