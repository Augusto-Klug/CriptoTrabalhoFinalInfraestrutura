using CriptoTrabalhoFinalInfraestrutura.DTOs;
using CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;
using System.Net.Http.Json;

namespace CriptoTrabalhoFinalInfraestrutura.Services;

public class BinanceService : IBinanceService
{
    private readonly IBinanceIntegration _binanceIntegration;
    private readonly HttpClient _httpClient;

    public BinanceService(HttpClient httpClient, IBinanceIntegration binanceIntegration)
    {
        _httpClient = httpClient;
        _binanceIntegration = binanceIntegration;
    }

    public async Task<IEnumerable<NegociacaoRecenteDTO>> GetRecentTradesAsync(string symbol, int limit)
    {
        var trades = await _binanceIntegration.GetRecentTradesAsync(symbol, limit);

        return trades.Select(t => new NegociacaoRecenteDTO
        {
            Id = t.Id,
            Preco = t.Price,
            Quantidade = t.Qty,
            QuantidadeCotacao = t.QuoteQty,
            Horario = t.Time,
            EhComprador = t.IsBuyerMaker,
            EhMelhorCorrespondencia = t.IsBestMatch
        });
    }

    public async Task<object?> GetReferencePriceAsync(string symbol)
    {
        var response = await _httpClient.GetAsync($"api/v3/referencePrice?symbol={symbol}");
        response.EnsureSuccessStatusCode();

        var referencePrice = await response.Content.ReadFromJsonAsync<object>();
        return referencePrice;
    }
}
