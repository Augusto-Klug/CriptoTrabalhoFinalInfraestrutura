using CriptoTrabalhoFinalInfraestrutura.DTOs;

namespace CriptoTrabalhoFinalInfraestrutura.Services;

public interface IBinanceTickerService
{
    Task<TickerPriceDTO> GetPriceAsync(string symbol);
}
