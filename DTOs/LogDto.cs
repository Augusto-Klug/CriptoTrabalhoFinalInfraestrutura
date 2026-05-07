namespace CriptoTrabalhoFinalInfraestrutura.DTOs;

public sealed class LogDto
{
    public int Id { get; init; }

    public DateTime Horario { get; init; }

    public string Criptos { get; init; } = string.Empty;

    public string Mensagem { get; init; } = string.Empty;
}
