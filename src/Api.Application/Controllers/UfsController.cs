using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UfsController : ControllerBase
    {
        public IUfService _service { get; set; }
        public UfsController(IUfService service)
        {
            _service = service;
        }

        /// <summary>
        /// Recupera todas as Unidades Federativas (UFs).
        /// </summary>
        /// <returns>Retorna uma lista de todas as UFs.</returns>
        /// <response code="200">Retorna a lista de UFs.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Recupera uma Unidade Federativa (UF) pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único da UF.</param>
        /// <returns>Retorna a UF se encontrada, caso contrário, retorna Não Encontrado.</returns>
        /// <response code="200">Retorna a UF se encontrada.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        /// <response code="404">Retorna Não Encontrado se a UF não existir.</response>
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Get(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
