// <copyright file="IMangaPages.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>18/06/2019 7:12 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System;
using System.Collections.Generic;

namespace ProjectNamakli.Domain.Models.Interfaces
{
    /// <summary>
    /// Manga images interface
    /// </summary>
    public interface IMangaPages
        : IIdentifiable
    {
        /// <summary>
        /// Gets or sets collection of images url
        /// </summary>
        IEnumerable<Uri> Images { get; set; }
    }
}