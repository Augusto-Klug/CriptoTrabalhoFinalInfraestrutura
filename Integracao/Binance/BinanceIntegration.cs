using CriptoTrabalhoFinalInfraestrutura.DTOs.Binance;
using System.Net.Http.Json;

namespace CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;

public class BinanceIntegration : IBinanceIntegration
{
    private readonly HttpClient _httpClient;

    public BinanceIntegration(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<RecentTradeResponse>> GetRecentTradesAsync(string symbol, int limit = 500)
    {
        var response = await _httpClient.GetAsync($"api/v3/trades?symbol={symbol}&limit={limit}");
        response.EnsureSuccessStatusCode();

        var trades = await response.Content.ReadFromJsonAsync<IEnumerable<RecentTradeResponse>>();
        return trades ?? Enumerable.Empty<RecentTradeResponse>();
    }
}
