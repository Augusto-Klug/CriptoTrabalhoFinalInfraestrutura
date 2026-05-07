namespace CriptoTrabalhoFinalInfraestrutura.Entities;

public sealed class LogEntity
{
    public int Id { get; set; }

    public DateTime Horario { get; set; }

    public List<string> Criptos { get; set; } = [];

    public string Mensagem { get; set; } = string.Empty;
}
