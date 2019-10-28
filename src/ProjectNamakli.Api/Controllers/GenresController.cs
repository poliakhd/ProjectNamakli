// <copyright file="GenresController.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>21/06/2019 3:39 PM</date>
// <summary>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </summary>

using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectNamakli.Application.Queries.Genres;
using ProjectNamakli.Domain.Models.Interfaces;

namespace ProjectNamakli.Api.Controllers
{
    /// <summary>
    /// Genres controller
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GenresController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenresController"/> class
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/> instance</param>
        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method for get catalog genres
        /// </summary>
        /// <param name="catalog">Catalog id</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v1/genres/mangafox
        ///
        /// </remarks>
        /// <returns>List of genres representing by <see cref="IGenre"/></returns>
        /// <response code="200">Returns if request was valid and there were no errors</response>
        /// <response code="400">Returns if request was invalid</response>
        /// <response code="500">Returns if something went wrong</response>
        [HttpGet("{catalog}")]
        [ProducesResponseType(typeof(IEnumerable<IGenre>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGenres(string catalog)
        {
            var request = new GetGenresCriterion(catalog);
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Method for get genre content (manga list)
        /// </summary>
        /// <param name="catalog">Catalog id</param>
        /// <param name="genre">Genre id</param>
        /// <param name="sort">Sort type</param>
        /// <param name="page">Page number</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v1/genres/mangafox/action
        ///
        /// </remarks>
        /// <returns>List of manga representing by <see cref="IMangaPreview"/></returns>
        /// <response code="200">Returns if request was valid and there were no errors</response>
        /// <response code="400">Returns if request was invalid</response>
        /// <response code="500">Returns if something went wrong</response>
        [HttpGet("{catalog}/{genre}")]
        [ProducesResponseType(typeof(IEnumerable<IMangaPreview>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCatalogContent(
            string catalog,
            string genre,
            [FromQuery] string sort = "popular",
            [FromQuery] int page = 0)
        {
            var request = new GetGenreContentCriterion(catalog, genre, sort, page);
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}