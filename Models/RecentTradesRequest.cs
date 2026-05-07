using System.ComponentModel.DataAnnotations;

namespace CriptoTrabalhoFinalInfraestrutura.Models;

public class RecentTradesRequest
{
    [Required]
    public string Symbol { get; set; } = string.Empty;

    [Range(1, 1000)]
    public int Limit { get; set; } = 500;
}
