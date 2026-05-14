using CriptoTrabalhoFinalInfraestrutura.DTOs.Binance;

namespace CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;

public interface IBinanceIntegration
{
    Task<IEnumerable<RecentTradeResponse>> GetRecentTradesAsync(string symbol, int limit = 500);
    Task<TickerPriceResponse> GetTickerPriceAsync(string symbol);
    Task<object?> GetReferencePriceAsync(string symbol);
}
