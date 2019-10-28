// <copyright file="HttpExtensions.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>02/10/2019 11:46 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectNamakli.Application.Extensions
{
    /// <summary>
    /// Http extension methods
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// Extension method for get content from response as string
        /// </summary>
        /// <param name="httpResponse"><see cref="HttpResponseMessage" /> instance</param>
        /// <returns><see cref="HttpResponseMessage" /> content as <see cref="string" /></returns>
        public static Task<string> GetContent(this HttpResponseMessage httpResponse)
        {
            return httpResponse.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Extension method to post model as json
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="client"><see cref="HttpClient"/> instance</param>
        /// <param name="url">Site url</param>
        /// <param name="model">Model to post</param>
        /// <returns><see cref="HttpResponseMessage"/> instance</returns>
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string url, T model)
        {
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(model),
                Encoding.Unicode,
                "application/json");

            return client.PostAsync(url, requestContent);
        }

        /// <summary>
        /// Extension method to put model as json
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="client"><see cref="HttpClient"/> instance</param>
        /// <param name="url">Site url</param>
        /// <param name="model">Model to put</param>
        /// <returns><see cref="HttpResponseMessage"/> instance</returns>
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string url, T model)
        {
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(model),
                Encoding.Unicode,
                "application/json");

            return client.PutAsync(url, requestContent);
        }

        /// <summary>
        /// Extension method to cast json response to specified type object
        /// </summary>
        /// <typeparam name="T">Type to cast</typeparam>
        /// <param name="httpResponse"><see cref="HttpResponseMessage"/> instance</param>
        /// <returns>Typed converted instance</returns>
        public static async Task<T> ContentCastToAsync<T>(this HttpResponseMessage httpResponse)
            where T : class
        {
            var content = await httpResponse.GetContent();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}