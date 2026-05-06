using CriptoTrabalhoFinalInfraestrutura.DTOs.Binance;

namespace CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;

public interface IBinanceIntegration
{
    Task<IEnumerable<RecentTradeResponse>> GetRecentTradesAsync(string symbol, int limit = 500);
}
