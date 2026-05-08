using CriptoTrabalhoFinalInfraestrutura.DTOs;
using CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;

namespace CriptoTrabalhoFinalInfraestrutura.Services;

public class BinanceTickerService : IBinanceTickerService
{
    private readonly IBinanceIntegration _binanceIntegration;

    public BinanceTickerService(IBinanceIntegration binanceIntegration)
    {
        _binanceIntegration = binanceIntegration;
    }

    public async Task<TickerPriceDTO> GetPriceAsync(string symbol)
    {
        var ticker = await _binanceIntegration.GetTickerPriceAsync(symbol);

        return new TickerPriceDTO
        {
            Symbol = ticker.Symbol,
            Price = ticker.Price
        };
    }
}
