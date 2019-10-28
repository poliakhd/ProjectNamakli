// <copyright file="PerformancePipelineBehavior.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>30/09/2019 8:19 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ProjectNamakli.Application.PipelineBehaviors
{
    /// <summary>
    /// Requests performance pipeline
    /// </summary>
    /// <typeparam name="TRequest">Type of request</typeparam>
    /// <typeparam name="TResponse">Type of response</typeparam>
    public class PerformancePipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformancePipelineBehavior{TRequest, TResponse}"/> class
        /// </summary>
        /// <param name="logger"><see cref="ILogger{TRequest}"/> instance</param>
        public PerformancePipelineBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var watch = Stopwatch.StartNew();

            var result = await next();

            watch.Stop();

            _logger.LogInformation($"Time spent on processing request: {watch.Elapsed.Milliseconds}ms");

            return result;
        }
    }
}