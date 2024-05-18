using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {
        public IMunicipioService _service { get; set; }
        public MunicipiosController(IMunicipioService service)
        {
            _service = service;
        }

        /// <summary>
        /// Recupera todos os municípios.
        /// </summary>
        /// <returns>Retorna uma lista de todos os municípios.</returns>
        /// <response code="200">Retorna a lista de municípios.</response>
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
        /// Recupera um município pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único do município.</param>
        /// <returns>Retorna o município se encontrado, caso contrário, retorna Não Encontrado.</returns>
        /// <response code="200">Retorna o município se encontrado.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        /// <response code="404">Retorna Não Encontrado se o município não existir.</response>
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetMunicipioWithId")]
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

        /// <summary>
        /// Recupera um município completo (com detalhes) pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único do município.</param>
        /// <returns>Retorna o município completo se encontrado, caso contrário, retorna Não Encontrado.</returns>
        /// <response code="200">Retorna o município completo se encontrado.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        /// <response code="404">Retorna Não Encontrado se o município não existir.</response>
        [Authorize("Bearer")]
        [HttpGet]
        [Route("Complete/{id}")]
        public async Task<ActionResult> GetCompleteById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.GetCompleteById(id);
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

        /// <summary>
        /// Recupera um município completo (com detalhes) pelo seu código IBGE.
        /// </summary>
        /// <param name="codigoIBGE">O código IBGE do município.</param>
        /// <returns>Retorna o município completo se encontrado, caso contrário, retorna Não Encontrado.</returns>
        /// <response code="200">Retorna o município completo se encontrado.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        /// <response code="404">Retorna Não Encontrado se o município não existir.</response>
        [Authorize("Bearer")]
        [HttpGet]
        [Route("byIBGE/{codigoIBGE}")]
        public async Task<ActionResult> GetCompleteByIBGE(int codigoIBGE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.GetCompleteByIBGE(codigoIBGE);
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

        /// <summary>
        /// Cria um novo município.
        /// </summary>
        /// <param name="dtoCreate">Os dados para criar um novo município.</param>
        /// <returns>Retorna o município recém-criado se for bem-sucedido, caso contrário, retorna BadRequest.</returns>
        /// <remarks>
        /// Exemplo de solicitação:
        ///
        ///     POST /api/Municipios
        ///     {
        ///        "nome": "Município Exemplo",
        ///        "codigoIBGE": 123456,
        ///        "ufId": "guid-da-uf"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna o município criado com sucesso.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MunicipioDtoCreate dtoCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Post(dtoCreate);
                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetMunicipioWithId", new { id = result.Id })), result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Atualiza um município existente.
        /// </summary>
        /// <param name="dtoUpdate">Os dados para atualizar um município.</param>
        /// <returns>Retorna o município atualizado se for bem-sucedido, caso contrário, retorna BadRequest.</returns>
        /// <remarks>
        /// Exemplo de solicitação:
        ///
        ///     PUT /api/Municipios
        ///     {
        ///        "id": "guid-do-municipio",
        ///        "nome": "Município Atualizado"
        ///        "codIBGE": 1234567,
        ///        "ufId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna o município atualizado com sucesso.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] MunicipioDtoUpdate dtoUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.Put(dtoUpdate);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Exclui um município pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único do município a ser excluído.</param>
        /// <returns>Retorna NoContent se a exclusão for bem-sucedida, caso contrário, retorna BadRequest.</returns>
        /// <remarks>
        /// Exemplo de solicitação:
        ///
        ///     DELETE /api/Municipios/guid-do-municipio
        ///
        /// </remarks>
        /// <response code="204">Retorna NoContent se o município for excluído com sucesso.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}