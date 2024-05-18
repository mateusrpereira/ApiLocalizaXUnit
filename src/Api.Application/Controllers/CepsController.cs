using System.Net;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CepsController : ControllerBase
    {
        public ICepService _service { get; set; }
        public CepsController(ICepService service)
        {
            _service = service;
        }

        /// <summary>
        /// Recupera um CEP pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único do CEP.</param>
        /// <returns>Retorna o CEP se encontrado, caso contrário, retorna Não Encontrado.</returns>
        /// <response code="200">Retorna o CEP se encontrado.</response>
        /// <response code="404">Retorna Não Encontrado se o CEP não existir.</response>
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetCepWithId")]
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
        /// Recupera um CEP pelo seu valor de CEP.
        /// </summary>
        /// <param name="cep">O valor do CEP a ser pesquisado.</param>
        /// <returns>Retorna o CEP se encontrado, caso contrário, retorna Não Encontrado.</returns>
        /// <response code="200">Retorna o CEP se encontrado.</response>
        /// <response code="404">Retorna Não Encontrado se o CEP não existir.</response>
        [AllowAnonymous]
        [HttpGet]
        [Route("byCep/{cep}")]
        public async Task<ActionResult> Get(string cep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Get(cep);
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
        /// Cria um novo recurso de CEP.
        /// </summary>
        /// <param name="dtoCreate">Os dados para criar um novo CEP.</param>
        /// <returns>Retorna o CEP recém-criado se for bem-sucedido, caso contrário, retorna BadRequest.</returns>
        /// <remarks>
        /// Exemplo de solicitação:
        ///
        ///     POST /api/Ceps
        ///     {
        ///        "cep": "12345678",
        ///        "logradouro": "Rua exemplo",
        ///        "numero": "111",
        ///        "municipioId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna o CEP criado com sucesso.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CepDtoCreate dtoCreate)
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
                    return Created(new Uri(Url.Link("GetCepWithId", new { id = result.Id })), result);
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
        /// Atualiza um recurso de CEP existente.
        /// </summary>
        /// <param name="dtoUpdate">Os dados para atualizar o CEP.</param>
        /// <returns>Retorna o CEP atualizado se for bem-sucedido, caso contrário, retorna BadRequest.</returns>
        /// <response code="200">Retorna o CEP atualizado.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CepDtoUpdate dtoUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.Put(dtoUpdate);
                if (result == null)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Exclui um recurso de CEP pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único do CEP a ser excluído.</param>
        /// <returns>Retorna um status indicando o sucesso da operação de exclusão.</returns>
        /// <response code="200">Retorna Ok se o CEP foi excluído com sucesso.</response>
        /// <response code="404">Retorna Não Encontrado se o CEP não existir.</response>
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
