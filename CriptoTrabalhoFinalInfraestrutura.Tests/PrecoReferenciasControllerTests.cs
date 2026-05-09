using CriptoTrabalhoFinalInfraestrutura.Controllers;
using CriptoTrabalhoFinalInfraestrutura.Models;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CriptoTrabalhoFinalInfraestrutura.Tests;

public class PrecoReferenciasControllerTests
{
    // Valida ModelState e devolve BadRequest quando o request for inválido.
    [Fact]
    public async Task DeveRetornarBadRequestQuandoModelstateEhInvalido()
    {
        var mockService = new Mock<IBinanceService>();
        var controller = new PrecoReferenciasController(mockService.Object);
        controller.ModelState.AddModelError("Symbol", "Required");

        var result = await controller.GetReferencePrice(new PrecoReferenciaRequest());

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // Garante que o símbolo é convertido para maiúsculo antes de chamar o serviço.
    [Fact]
    public async Task DeveConverterSimboloParaMaiusculoAntesDeChamarServico()
    {
        var mockService = new Mock<IBinanceService>();
        var expectedResponse = new { price = "12345.67" };
        mockService
            .Setup(s => s.GetReferencePriceAsync("BTCUSDT"))
            .ReturnsAsync(expectedResponse);

        var controller = new PrecoReferenciasController(mockService.Object);
        var request = new PrecoReferenciaRequest { Symbol = "btcusdt" };

        var result = await controller.GetReferencePrice(request);

        Assert.IsType<OkObjectResult>(result);
        mockService.Verify(s => s.GetReferencePriceAsync("BTCUSDT"), Times.Once);
    }

    // Verifica o retorno OkObjectResult quando o serviço responde com sucesso.
    [Fact]
    public async Task DeveRetornarOkQuandoServicoRespondeComSucesso()
    {
        var mockService = new Mock<IBinanceService>();
        var expectedResponse = new { price = "12345.67" };
        mockService
            .Setup(s => s.GetReferencePriceAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedResponse);

        var controller = new PrecoReferenciasController(mockService.Object);
        var request = new PrecoReferenciaRequest { Symbol = "BTCUSDT" };

        var actionResult = await controller.GetReferencePrice(request);

        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(expectedResponse, okResult.Value);
    }

    // Retorna 500 quando o serviço lança uma exceção.
    [Fact]
    public async Task DeveRetornar500QuandoServicoLancarExcecao()
    {
        var mockService = new Mock<IBinanceService>();
        mockService
            .Setup(s => s.GetReferencePriceAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Erro de teste"));

        var controller = new PrecoReferenciasController(mockService.Object);
        var request = new PrecoReferenciaRequest { Symbol = "BTCUSDT" };

        var actionResult = await controller.GetReferencePrice(request);

        var statusResult = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(500, statusResult.StatusCode);
    }

    // Garante que o método do serviço é chamado exatamente uma vez.
    [Fact]
    public async Task DeveChamarGetReferencePriceAsyncExatamenteUmaVez()
    {
        var mockService = new Mock<IBinanceService>();
        mockService
            .Setup(s => s.GetReferencePriceAsync(It.IsAny<string>()))
            .ReturnsAsync(new { price = "12345.67" });

        var controller = new PrecoReferenciasController(mockService.Object);
        var request = new PrecoReferenciaRequest { Symbol = "BTCUSDT" };

        await controller.GetReferencePrice(request);

        mockService.Verify(s => s.GetReferencePriceAsync(It.IsAny<string>()), Times.Once);
    }
}
