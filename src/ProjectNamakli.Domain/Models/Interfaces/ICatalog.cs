// <copyright file="ICatalog.cs" company="10Apps">
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
    /// Catalog interface
    /// </summary>
    public interface ICatalog
        : IIdentifiable
    {
        /// <summary>
        /// Gets or sets catalog url
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Gets or sets catalog title
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets catalog icon
        /// </summary>
        string Icon { get; set; }
    }
}