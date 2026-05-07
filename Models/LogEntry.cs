namespace CriptoTrabalhoFinalInfraestrutura.Models;

public sealed class LogEntry
{
    public int Id { get; set; }

    public DateTime Horario { get; set; }

    public string Criptos { get; set; } = string.Empty;

    public string Mensagem { get; set; } = string.Empty;
}
