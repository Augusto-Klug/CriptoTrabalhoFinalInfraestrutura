using CriptoTrabalhoFinalInfraestrutura.DTOs.Binance;
using CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Moq;
using Xunit;

namespace CriptoTrabalhoFinalInfraestrutura.Tests;

public class BinanceServiceTests
{
    private readonly Mock<IBinanceIntegration> _binanceIntegrationMock;
    private readonly BinanceService _binanceService;

    public BinanceServiceTests()
    {
        _binanceIntegrationMock = new Mock<IBinanceIntegration>();
        _binanceService = new BinanceService(_binanceIntegrationMock.Object);
    }

    [Fact]
    public async Task GetRecentTradesAsync_ShouldReturnMappedTrades()
    {
        // Arrange
        var symbol = "BTCUSDT";
        var limit = 10;
        var tradesResponse = new List<RecentTradeResponse>
        {
            new RecentTradeResponse
            {
                Id = 1,
                Price = "50000.00",
                Qty = "0.01",
                QuoteQty = "500.00",
                Time = 1620000000000,
                IsBuyerMaker = true,
                IsBestMatch = true
            }
        };

        _binanceIntegrationMock.Setup(x => x.GetRecentTradesAsync(symbol, limit))
            .ReturnsAsync(tradesResponse);

        // Act
        var result = await _binanceService.GetRecentTradesAsync(symbol, limit);

        // Assert
        Assert.NotNull(result);
        var trade = Assert.Single(result);
        Assert.Equal(1, trade.Id);
        Assert.Equal("50000.00", trade.Preco);
        Assert.Equal("0.01", trade.Quantidade);
        Assert.Equal("500.00", trade.QuantidadeCotacao);
        Assert.Equal(1620000000000, trade.Horario);
        Assert.True(trade.EhComprador);
        Assert.True(trade.EhMelhorCorrespondencia);
    }
}
