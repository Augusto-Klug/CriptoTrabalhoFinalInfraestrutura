using System.ComponentModel.DataAnnotations;

namespace CriptoTrabalhoFinalInfraestrutura.Models;

public class PrecoReferenciaRequest
{
    [Required(ErrorMessage = "O símbolo é obrigatório.")]
    public string Symbol { get; set; } = string.Empty;
}
