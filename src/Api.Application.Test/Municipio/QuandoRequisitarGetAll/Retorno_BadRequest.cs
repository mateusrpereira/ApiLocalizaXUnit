using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Municipio.QuandoRequisitarGetAll
{
    public class Retorno_BadRequest
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É possível Realizar o GetAll.")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<MunicipioDto>
                {
                    new MunicipioDto
                    {
                        Id = Guid.NewGuid(),
                        Nome = "Curitiba",
                    },
                    new MunicipioDto
                    {
                        Id = Guid.NewGuid(),
                        Nome = "São Paulo",
                    },
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
