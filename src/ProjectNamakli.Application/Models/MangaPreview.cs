// <copyright file="MangaPreview.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>18/06/2019 7:12 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Collections.Generic;
using ProjectNamakli.Domain.Models.Interfaces;

namespace ProjectNamakli.Application.Models
{
    /// <summary>
    /// Base <see cref="IMangaPreview"/> implementation
    /// </summary>
    public class MangaPreview
        : IMangaPreview
    {
        /// <inheritdoc />
        public string Id { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public string Url { get; set; }

        /// <inheritdoc />
        public IEnumerable<string> Covers { get; set; }

        /// <inheritdoc />
        public float Rating { get; set; }

        /// <inheritdoc />
        public float RatingMax { get; set; }
    }
}