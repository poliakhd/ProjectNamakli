// <copyright file="BaseContext.cs" company="10Apps">
// Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>Daniil Poliakh</author>
// <date>27/01/2018 12:26 PM</date>
// <summary>
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace ProjectNamakli.Api.Tests
{
    /// <summary>
    /// Test context
    /// </summary>
    public class TestContext : IDisposable
    {
        private readonly TestServer _testServer;

        private bool _disposed;

        public TestContext()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();

            _testServer = new TestServer(builder);

            Client = _testServer.CreateClient();
            ServiceProvider = _testServer.Host.Services;
        }

        public HttpClient Client { get; }

        public IServiceProvider ServiceProvider { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _testServer?.Dispose();
                    Client?.Dispose();
                }

                _disposed = true;
            }
        }
    }
}