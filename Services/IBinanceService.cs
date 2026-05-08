using CriptoTrabalhoFinalInfraestrutura.DTOs;

namespace CriptoTrabalhoFinalInfraestrutura.Services;

public interface IBinanceService
{
    Task<IEnumerable<NegociacaoRecenteDTO>> GetRecentTradesAsync(string symbol, int limit);
}
