// <copyright file="IMangaBase.cs" company="10Apps">
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

namespace ProjectNamakli.Domain.Models.Interfaces
{
    /// <summary>
    /// Manga base interface
    /// </summary>
    public interface IMangaBase
        : IIdentifiable
    {
        /// <summary>
        /// Gets or sets title
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets url
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Gets or sets collection of covers urls
        /// </summary>
        IEnumerable<string> Covers { get; set; }

        /// <summary>
        /// Gets or sets rating
        /// </summary>
        float Rating { get; set; }

        /// <summary>
        /// Gets or sets rating limit
        /// </summary>
        float RatingMax { get; set; }
    }
}