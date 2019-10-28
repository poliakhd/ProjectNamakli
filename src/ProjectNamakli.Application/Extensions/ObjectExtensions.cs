// <copyright file="ObjectExtensions.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>02/10/2019 11:51 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

namespace ProjectNamakli.Application.Extensions
{
    /// <summary>
    /// <see cref="object"/> extension methods
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Method for cast object to the specified type
        /// </summary>
        /// <typeparam name="T">Type of to casting object</typeparam>
        /// <param name="object">Object to cast</param>
        /// <returns>Cast object</returns>
        public static T To<T>(this object @object)
            where T : class
        {
            return @object as T;
        }
    }
}