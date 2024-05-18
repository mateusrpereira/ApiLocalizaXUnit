using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Municipio.QuandoRequisitarCreate
{
    public class Retorno_Created
    {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É possível Realizar o Created.")]
        public async Task E_Possivel_Invocar_a_Controller_Create()
        {
            var serviceMock = new Mock<IMunicipioService>();
            serviceMock.Setup(m => m.Post(It.IsAny<MunicipioDtoCreate>())).ReturnsAsync(
               new MunicipioDtoCreateResult
               {
                   Id = Guid.NewGuid(),
                   Nome = "Curitiba",
                   CreateAt = DateTime.Now
               }
           );

            _controller = new MunicipiosController(serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var municipioDtoCreate = new MunicipioDtoCreate
            {
                Nome = "Curitiba",
                CodIBGE = 1
            };

            var result = await _controller.Post(municipioDtoCreate);
            Assert.True(result is CreatedResult);
        }
    }
}
