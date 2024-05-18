using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Recupera todos os usuários.
        /// </summary>
        /// <returns>Retorna uma lista de todos os usuários.</returns>
        /// <response code="200">Retorna a lista de usuários.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
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
        /// Recupera um usuário pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único do usuário.</param>
        /// <returns>Retorna o usuário se encontrado, caso contrário, retorna Não Encontrado.</returns>
        /// <response code="200">Retorna o usuário se encontrado.</response>
        /// <response code="404">Retorna Não Encontrado se o usuário não existir.</response>
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicitação inválida
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
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="user">Os dados para criar um novo usuário.</param>
        /// <returns>Retorna o usuário recém-criado se for bem-sucedido, caso contrário, retorna BadRequest.</returns>
        /// <remarks>
        /// Exemplo de solicitação:
        ///
        ///     POST /api/Users
        ///     {
        ///        "name": "Fulano de Tal",
        ///        "email": "exemplo@dominio.com"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna o usuário criado com sucesso.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDtoCreate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Post(user);
                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetWithId", new { id = result.Id })), result);
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
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="user">Os dados para atualizar o usuário.</param>
        /// <returns>Retorna o usuário atualizado se for bem-sucedido, caso contrário, retorna BadRequest.</returns>
        /// <response code="200">Retorna o usuário atualizado.</response>
        /// <response code="400">Retorna BadRequest se a solicitação for inválida.</response>
        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDtoUpdate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Put(user);
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
        /// Exclui um usuário pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único do usuário a ser excluído.</param>
        /// <returns>Retorna um status indicando o sucesso da operação de exclusão.</returns>
        /// <response code="200">Retorna Ok se o usuário foi excluído com sucesso.</response>
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
