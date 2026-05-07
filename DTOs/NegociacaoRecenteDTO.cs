namespace CriptoTrabalhoFinalInfraestrutura.DTOs;

public class NegociacaoRecenteDTO
{
    public long Id { get; set; }
    public string Preco { get; set; } = string.Empty;
    public string Quantidade { get; set; } = string.Empty;
    public string QuantidadeCotacao { get; set; } = string.Empty;
    public long Horario { get; set; }
    public bool EhComprador { get; set; }
    public bool EhMelhorCorrespondencia { get; set; }
}
