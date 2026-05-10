using CriptoTrabalhoFinalInfraestrutura.DTOs.Binance;
using CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Moq;

namespace CriptoTrabalhoFinalInfraestrutura.Tests;

public class BinanceTickerServiceTests
{
    private readonly Mock<IBinanceIntegration> _binanceIntegrationMock;
    private readonly BinanceTickerService _binanceTickerService;

    public BinanceTickerServiceTests()
    {
        _binanceIntegrationMock = new Mock<IBinanceIntegration>();
        _binanceTickerService = new BinanceTickerService(_binanceIntegrationMock.Object);
    }

    [Fact]
    public async Task GetPriceAsync_ShouldReturnMappedPrice()
    {
        // Arrange
        var symbol = "BTCUSDT";
        var tickerResponse = new TickerPriceResponse
        {
            Symbol = symbol,
            Price = "50000.00"
        };

        _binanceIntegrationMock.Setup(x => x.GetTickerPriceAsync(symbol))
            .ReturnsAsync(tickerResponse);

        // Act
        var result = await _binanceTickerService.GetPriceAsync(symbol);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(symbol, result.Symbol);
        Assert.Equal("50000.00", result.Price);
    }
}
