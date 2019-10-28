// <copyright file="CatalogsController.cs" company="10Apps">
// Copyright (c) 10Apps. All rights reserved.
// </copyright>
// <author>Daniil Poliakh</author>
// <date>20/06/2019 9:07 PM</date>
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
using ProjectNamakli.Application.Queries.Catalogs;
using ProjectNamakli.Domain.Models.Interfaces;

namespace ProjectNamakli.Api.Controllers
{
    /// <summary>
    /// Catalogs controller
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CatalogsController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogsController"/> class
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/> instance</param>
        public CatalogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method for get available catalogs
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v1/catalogs
        ///
        /// </remarks>
        /// <returns>List of catalogs representing by <see cref="ICatalog"/></returns>
        /// <response code="200">Returns if request was valid and there were no errors</response>
        /// <response code="400">Returns if request was invalid</response>
        /// <response code="500">Returns if something went wrong</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ICatalog>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCatalogs()
        {
            var request = new GetCatalogsCriterion();
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Method for get catalog content (manga list)
        /// </summary>
        /// <param name="catalog">Catalog id</param>
        /// <param name="sort">Sort type</param>
        /// <param name="page">Page number</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v1/catalogs/mangafox
        ///
        /// </remarks>
        /// <returns>List of manga representing by <see cref="IMangaPreview"/></returns>
        /// <response code="200">Returns if request was valid and there were no errors</response>
        /// <response code="400">Returns if request was invalid</response>
        /// <response code="500">Returns if something went wrong</response>
        [HttpGet("{catalog}")]
        [ProducesResponseType(typeof(IEnumerable<IMangaPreview>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCatalogContent(
            string catalog,
            [FromQuery] string sort = "popular",
            [FromQuery] int page = 0)
        {
            var request = new GetCatalogContentCriterion(catalog, sort, page);
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}