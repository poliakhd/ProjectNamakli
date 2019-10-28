// <copyright file="CatalogType.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>18/06/2019 7:14 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.ComponentModel;

namespace ProjectNamakli.Domain.Types
{
    /// <summary>
    /// Catalog types
    /// </summary>
    public enum CatalogType
    {
        /// <summary>
        /// MangaFox source
        /// </summary>
        [Description("mangafox")]
        MangaFox = 1,

        /// <summary>
        /// ReadManga source
        /// </summary>
        [Description("readmanga")]
        ReadManga = 2,

        /// <summary>
        /// MintManga source
        /// </summary>
        [Description("mintmanga")]
        MintManga = 3
    }
}