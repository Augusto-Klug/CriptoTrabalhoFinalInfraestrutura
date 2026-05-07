namespace CriptoTrabalhoFinalInfraestrutura.DTOs;

public sealed class LogDto
{
    public int Id { get; init; }

    public DateTime Horario { get; init; }

    public IReadOnlyList<string> Criptos { get; init; } = [];

    public string Mensagem { get; init; } = string.Empty;
}
