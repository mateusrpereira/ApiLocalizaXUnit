using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoUpdate : MunicipioTestes
    {
        private IMunicipioService _service;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possível executar o Método Update.")]
        public async Task E_Possivel_Executar_Metodo_Update()
        {
            // _serviceMock = new Mock<IMunicipioService>();
            // _serviceMock.Setup(m => m.Post(municipioDtoCreate)).ReturnsAsync(municipioDtoCreateResult);
            // _service = _serviceMock.Object;

            // var result = await _service.Post(municipioDtoCreate);
            // Assert.NotNull(result);
            // Assert.Equal(NomeMunicipio, result.Nome);
            // Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
            // Assert.Equal(IdUf, result.UfId);

            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Put(municipioDtoUpdate)).ReturnsAsync(municipioDtoUpdateResult);
            _service = _serviceMock.Object;

            var resultUpdate = await _service.Put(municipioDtoUpdate);
            Assert.NotNull(resultUpdate);
            Assert.Equal(NomeMunicipioAlterdo, resultUpdate.Nome);
            Assert.Equal(CodigoIBGEMunicipioAlterado, resultUpdate.CodIBGE);
            Assert.Equal(IdUf, resultUpdate.UfId);
        }
    }
}
