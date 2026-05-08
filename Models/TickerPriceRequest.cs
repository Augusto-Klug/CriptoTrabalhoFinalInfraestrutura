using System.ComponentModel.DataAnnotations;

namespace CriptoTrabalhoFinalInfraestrutura.Models;

public class TickerPriceRequest
{
    [Required]
    public string Symbol { get; set; } = string.Empty;
}
