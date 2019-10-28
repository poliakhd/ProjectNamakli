// <copyright file="Manga.cs" company="10Apps">
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
using Newtonsoft.Json;
using ProjectNamakli.Application.Converters.Json;
using ProjectNamakli.Domain.Models.Interfaces;
using ProjectNamakli.Domain.Types;

namespace ProjectNamakli.Application.Models
{
    /// <summary>
    /// Base <see cref="IManga"/> implementation
    /// </summary>
    public class Manga
        : IManga
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

        /// <inheritdoc />
        public IEnumerable<string> AlternateTitles { get; set; }

        /// <inheritdoc />
        public StatusType Status { get; set; }

        /// <inheritdoc />
        public int Published { get; set; }

        /// <inheritdoc />
        public string Volumes { get; set; }

        /// <inheritdoc />
        public int Views { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        [JsonConverter(typeof(TypeConverter<IEnumerable<Genre>>))]
        public IEnumerable<IGenre> Genres { get; set; }

        /// <inheritdoc />
        [JsonConverter(typeof(TypeConverter<IEnumerable<Author>>))]
        public IEnumerable<IAuthor> Authors { get; set; }

        /// <inheritdoc />
        [JsonConverter(typeof(TypeConverter<IEnumerable<Translator>>))]
        public IEnumerable<ITranslator> Translators { get; set; }

        /// <inheritdoc />
        [JsonConverter(typeof(TypeConverter<IEnumerable<Chapter>>))]
        public IEnumerable<IChapter> Chapters { get; set; }
    }
}