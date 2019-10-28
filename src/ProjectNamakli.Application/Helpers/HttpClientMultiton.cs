// <copyright file="HttpClientMultiton.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 8:57 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using ProjectNamakli.Application.Helpers.Interfaces;

namespace ProjectNamakli.Application.Helpers
{
    /// <summary>
    /// Http client multiton
    /// </summary>
    public class HttpClientMultiton
        : IMultiton<string, HttpClient>
    {
        /// <summary>
        /// Dictionary for store <see cref="HttpClient" /> instances by key
        /// </summary>
        private readonly Dictionary<string, HttpClient> _instances = new Dictionary<string, HttpClient>();

        /// <inheritdoc />
        public HttpClient GetInstance(string key)
        {
            HttpClient instance;

            lock (_instances)
            {
                if (_instances.TryGetValue(key, out instance))
                {
                    return instance;
                }

                instance = CreateClient(key);

                _instances.Add(key, instance);

                ServicePointManager.FindServicePoint(new Uri(key)).ConnectionLeaseTimeout = 60 * 1000;
            }

            return instance;
        }

        /// <inheritdoc />
        public bool GetExistingInstance(string key, out HttpClient instance)
        {
            return _instances.TryGetValue(key, out instance);
        }

        /// <inheritdoc />
        public void RemoveAll()
        {
            _instances.Clear();
        }

        /// <inheritdoc />
        public void Remove(string key)
        {
            _instances.Remove(key);
        }

        /// <summary>
        /// Factory method to create <see cref="HttpClient" /> instance
        /// </summary>
        /// <param name="key">Key of instance</param>
        /// <returns><see cref="HttpClient" /> instance</returns>
        private HttpClient CreateClient(string key)
        {
            return new HttpClient
            {
                BaseAddress = new Uri(key),
                DefaultRequestHeaders =
                {
                    ConnectionClose = false
                }
            };
        }
    }
}