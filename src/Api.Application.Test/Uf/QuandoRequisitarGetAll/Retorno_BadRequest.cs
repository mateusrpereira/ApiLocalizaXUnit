using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Uf.QuandoRequisitarGetAll
{
    public class Retorno_BadRequest
    {
        private UfsController _controller;

        [Fact(DisplayName = "É possível Realizar o GetAll.")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            var serviceMock = new Mock<IUfService>();
            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
               new List<UfDto>
               {
                   new UfDto
                   {
                        Id = Guid.NewGuid(),
                        Nome = "Curitiba",
                        Sigla = "PR"
                   },
                    new UfDto
                   {
                        Id = Guid.NewGuid(),
                        Nome = "São Paulo",
                        Sigla = "SP"
                   }
               }
           );

            _controller = new UfsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
