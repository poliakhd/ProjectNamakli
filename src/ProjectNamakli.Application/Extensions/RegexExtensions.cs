// <copyright file="RegexExtensions.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>02/10/2019 11:50 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Text.RegularExpressions;

namespace ProjectNamakli.Application.Extensions
{
    /// <summary>
    /// Regex extension methods
    /// </summary>
    public static class RegexExtensions
    {
        /// <summary>
        /// Extension method for get regex value if match succeed
        /// </summary>
        /// <param name="match"><see cref="Match"/> instance</param>
        /// <param name="value">Matched value</param>
        /// <returns>Passed <see cref="Match"/> instance</returns>
        public static Match GetValue(this Match match, out string value)
        {
            value = null;

            if (match.Success)
            {
                value = match.Value;
            }

            return match;
        }

        /// <summary>
        /// Extension method for get regex value for specified group if match succeed
        /// </summary>
        /// <param name="match"><see cref="Match"/> instance</param>
        /// <param name="index">Group index</param>
        /// <param name="value">Matched group value</param>
        /// <returns>Passed <see cref="Match"/> instance</returns>
        public static Match GetGroupValue(this Match match, int index, out string value)
        {
            value = null;

            if (match.Success)
            {
                value = match.Groups[index].Value;
            }

            return match;
        }
    }
}