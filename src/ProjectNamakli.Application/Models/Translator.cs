﻿// <copyright file="Translator.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>18/06/2019 7:13 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using ProjectNamakli.Domain.Models.Interfaces;

namespace ProjectNamakli.Application.Models
{
    /// <summary>
    /// Base <see cref="ITranslator"/> implementation
    /// </summary>
    public class Translator
        : ITranslator
    {
        /// <inheritdoc />
        public string Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Url { get; set; }
    }
}