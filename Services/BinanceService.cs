using CriptoTrabalhoFinalInfraestrutura.DTOs.Binance;
using CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;

namespace CriptoTrabalhoFinalInfraestrutura.Services;

public class BinanceService : IBinanceService
{
    private readonly IBinanceIntegration _binanceIntegration;

    public BinanceService(IBinanceIntegration binanceIntegration)
    {
        _binanceIntegration = binanceIntegration;
    }

    public async Task<IEnumerable<RecentTradeResponse>> GetRecentTradesAsync(string symbol, int limit)
    {
        // Aqui poderiam ser aplicadas regras de negócio, transformações ou cache.
        return await _binanceIntegration.GetRecentTradesAsync(symbol, limit);
    }
}
