using CriptoTrabalhoFinalInfraestrutura.DTOs.Binance;

namespace CriptoTrabalhoFinalInfraestrutura.Services;

public interface IBinanceService
{
    Task<IEnumerable<RecentTradeResponse>> GetRecentTradesAsync(string symbol, int limit);
}
