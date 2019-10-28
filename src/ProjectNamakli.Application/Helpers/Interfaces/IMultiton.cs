// <copyright file="IMultiton.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 8:57 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

namespace ProjectNamakli.Application.Helpers.Interfaces
{
    /// <summary>
    /// Multiton interface
    /// </summary>
    /// <typeparam name="TKey">Type of key</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    public interface IMultiton<in TKey, TValue>
    {
        /// <summary>
        /// Gets or creates instance
        /// </summary>
        /// <param name="key">Key of instance</param>
        /// <returns>Found or created instance</returns>
        TValue GetInstance(TKey key);

        /// <summary>
        /// Gets existing instance
        /// </summary>
        /// <param name="key">Key of instance</param>
        /// <param name="instance">Found instance</param>
        /// <returns>
        /// <value>true</value> if instance exists by given key; otherwise <value>false</value>
        /// </returns>
        bool GetExistingInstance(TKey key, out TValue instance);

        /// <summary>
        /// Removes all created instances
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// Remove created instance by key
        /// </summary>
        /// <param name="key">Key of instance</param>
        void Remove(TKey key);
    }
}