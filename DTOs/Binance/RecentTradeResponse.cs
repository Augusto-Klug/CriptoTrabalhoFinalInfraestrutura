namespace CriptoTrabalhoFinalInfraestrutura.DTOs.Binance;

public class RecentTradeResponse
{
    public long Id { get; set; }
    public string Price { get; set; } = string.Empty;
    public string Qty { get; set; } = string.Empty;
    public string QuoteQty { get; set; } = string.Empty;
    public long Time { get; set; }
    public bool IsBuyerMaker { get; set; }
    public bool IsBestMatch { get; set; }
}
