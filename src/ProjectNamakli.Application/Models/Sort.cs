﻿// <copyright file="Sort.cs" company="10Apps">
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
    /// Base <see cref="ISort"/> implementation
    /// </summary>
    public class Sort
        : ISort
    {
        /// <inheritdoc />
        public string Id { get; set; }

        /// <inheritdoc />
        public string Type { get; set; }
    }
}