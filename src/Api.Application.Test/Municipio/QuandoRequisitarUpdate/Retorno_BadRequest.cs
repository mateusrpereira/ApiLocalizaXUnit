using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Municipio.QuandoRequisitarUpdate
{
    public class Retorno_BadRequest
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É possível Realizar o Updated.")]
        public async Task E_Possivel_Invocar_a_Controller_Update()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Put(It.IsAny<MunicipioDtoUpdate>())).ReturnsAsync(
               new MunicipioDtoUpdateResult
               {
                   Id = Guid.NewGuid(),
                   Nome = "Curitiba",
                   UpdateAt = DateTime.Now
               }
           );

            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Nome", "É um campo obrigatório");

            var municipioDtoUpdate = new MunicipioDtoUpdate
            {
                Nome = "Curitiba",
                CodIBGE = 1
            };

            var result = await _controller.Put(municipioDtoUpdate);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
