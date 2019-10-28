// <copyright file="MangaController.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>21/06/2019 4:35 PM</date>
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
using ProjectNamakli.Application.Queries.Manga;
using ProjectNamakli.Domain.Models.Interfaces;

namespace ProjectNamakli.Api.Controllers
{
    /// <summary>
    /// Manga controller
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MangaController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MangaController"/> class
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/> instance</param>
        public MangaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method for get manga content
        /// </summary>
        /// <param name="catalog">Catalog id</param>
        /// <param name="manga">Manga id</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v1/manga/mangafox/onepunch_man
        ///
        /// </remarks>
        /// <returns>List of manga representing by <see cref="IManga"/> or <see cref="IMangaPages"/></returns>
        /// <response code="200">Returns if request was valid and there were no errors</response>
        /// <response code="400">Returns if request was invalid</response>
        /// <response code="500">Returns if something went wrong</response>
        [HttpGet("{catalog}/{manga}")]
        [ProducesResponseType(typeof(IEnumerable<IManga>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMangaContent(
            string catalog,
            string manga)
        {
            var request = new GetMangaContentCriterion(catalog, manga);
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Method for get manga pages
        /// </summary>
        /// <param name="catalog">Catalog id</param>
        /// <param name="manga">Manga id</param>
        /// <param name="volume">Volume name</param>
        /// <param name="chapter">Chapter number</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v1/manga/mangafox/onepunch_man/v01/c001
        ///
        /// </remarks>
        /// <returns>List of manga representing by <see cref="IManga"/> or <see cref="IMangaPages"/></returns>
        /// <response code="200">Returns if request was valid and there were no errors</response>
        /// <response code="400">Returns if request was invalid</response>
        /// <response code="500">Returns if something went wrong</response>
        [HttpGet("{catalog}/{manga}/{volume}/{chapter}")]
        [ProducesResponseType(typeof(IEnumerable<IMangaPages>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMangaPages(
            string catalog,
            string manga,
            string volume,
            string chapter)
        {
            var request = new GetMangaChapterContentCriterion(catalog, manga, volume, catalog);
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}