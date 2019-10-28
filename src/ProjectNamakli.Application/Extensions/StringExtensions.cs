// <copyright file="StringExtensions.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>02/10/2019 11:50 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

namespace ProjectNamakli.Application.Extensions
{
    /// <summary>
    /// String extension methods
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Extension method for get empty string if current is null
        /// </summary>
        /// <param name="value"><see cref="string"/> instance to check</param>
        /// <returns>Returns <see cref="string.Empty"/> if value is null; otherwise returns current value</returns>
        public static string EmptyStringIfNull(this string value)
        {
            return value ?? string.Empty;
        }
    }
}