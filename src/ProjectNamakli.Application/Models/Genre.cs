﻿// <copyright file="Genre.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>18/06/2019 7:12 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using ProjectNamakli.Domain.Models.Interfaces;

namespace ProjectNamakli.Application.Models
{
    /// <summary>
    /// Base <see cref="IGenre"/> implementation
    /// </summary>
    public class Genre
        : IGenre
    {
        /// <inheritdoc />
        public string Id { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <inheritdoc />
        public string Url { get; set; }
    }
}