// <copyright file="IChapter.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>18/06/2019 7:11 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

namespace ProjectNamakli.Domain.Models.Interfaces
{
    /// <summary>
    /// Chapter interface
    /// </summary>
    public interface IChapter
        : IIdentifiable
    {
        /// <summary>
        /// Gets or sets chapter volume
        /// </summary>
        string Volume { get; set; }

        /// <summary>
        /// Gets or sets chapter name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets chapter url
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Gets or sets chapter release date
        /// </summary>
        string Date { get; set; }
    }
}