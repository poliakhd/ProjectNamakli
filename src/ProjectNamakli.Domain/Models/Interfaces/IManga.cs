// <copyright file="IManga.cs" company="10Apps">
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
using ProjectNamakli.Domain.Types;

namespace ProjectNamakli.Domain.Models.Interfaces
{
    /// <summary>
    /// Manga interface
    /// </summary>
    public interface IManga
        : IMangaBase
    {
        /// <summary>
        /// Gets or sets alternative titles
        /// </summary>
        IEnumerable<string> AlternateTitles { get; set; }

        /// <summary>
        /// Gets or sets status
        /// </summary>
        StatusType Status { get; set; }

        /// <summary>
        /// Gets or sets year of publishing
        /// </summary>
        int Published { get; set; }

        /// <summary>
        /// Gets or sets number of volumes
        /// </summary>
        string Volumes { get; set; }

        /// <summary>
        /// Gets or sets number of views
        /// </summary>
        int Views { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets collection of genres
        /// </summary>
        IEnumerable<IGenre> Genres { get; set; }

        /// <summary>
        /// Gets or sets collection of authors
        /// </summary>
        IEnumerable<IAuthor> Authors { get; set; }

        /// <summary>
        /// Gets or sets collection of translators
        /// </summary>
        IEnumerable<ITranslator> Translators { get; set; }

        /// <summary>
        /// Gets or sets collection of chapters
        /// </summary>
        IEnumerable<IChapter> Chapters { get; set; }
    }
}