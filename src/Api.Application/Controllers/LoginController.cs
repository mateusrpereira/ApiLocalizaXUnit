using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Realiza a autenticação de um usuário.
        /// </summary>
        /// <param name="loginDto">E-mail válido existente na base de dados, caso não possua, navegue até POST /api/Users</param>
        /// <param name="service">O serviço de autenticação.</param>
        /// <returns>Retorna o usuário autenticado se for bem-sucedido, caso contrário, retorna Não Encontrado. Caso seja bem sucessido, copiar a chave do "accessToken" e informar ir até o botão Authorize e infomar Bearer seguido do token de acesso para utilizar os recursos da API.</returns>
        /// <response code="200">Retorna o usuário autenticado.</response>
        /// <response code="404">Retorna Não Encontrado se o usuário não existir.</response>
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto loginDto, [FromServices] ILoginService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (loginDto == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await service.FindByLogin(loginDto);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}