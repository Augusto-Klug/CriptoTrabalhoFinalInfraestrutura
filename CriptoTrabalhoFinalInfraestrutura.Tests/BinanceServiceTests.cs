using System.Net;
using CriptoTrabalhoFinalInfraestrutura.DTOs.Binance;
using CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Moq;
using Moq.Protected;

namespace CriptoTrabalhoFinalInfraestrutura.Tests;

public class BinanceServiceTests
{
    private readonly Mock<IBinanceIntegration> _binanceIntegrationMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly BinanceService _binanceService;

    public BinanceServiceTests()
    {
        _binanceIntegrationMock = new Mock<IBinanceIntegration>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.binance.com/")
        };
        _binanceService = new BinanceService(_httpClient, _binanceIntegrationMock.Object);
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
    }

    [Fact]
    public async Task GetRecentTradesAsync_ShouldThrowException_WhenIntegrationFails()
    {
        // Arrange
        var symbol = "BTCUSDT";
        _binanceIntegrationMock.Setup(x => x.GetRecentTradesAsync(symbol, It.IsAny<int>()))
            .ThrowsAsync(new Exception("Integration failure"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _binanceService.GetRecentTradesAsync(symbol, 10));
    }

    [Fact]
    public async Task GetReferencePriceAsync_ShouldReturnData_WhenSuccessful()
    {
        // Arrange
        var symbol = "BTCUSDT";
        var jsonResponse = "{\"symbol\":\"BTCUSDT\",\"price\":\"50000.00\"}";
        
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri != null && req.RequestUri.ToString().Contains("api/v3/referencePrice")),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        // Act
        var result = await _binanceService.GetReferencePriceAsync(symbol);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetReferencePriceAsync_ShouldThrowException_WhenApiReturnsError()
    {
        // Arrange
        var symbol = "BTCUSDT";
        
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _binanceService.GetReferencePriceAsync(symbol));
    }
}
