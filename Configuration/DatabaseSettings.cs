namespace CriptoTrabalhoFinalInfraestrutura.Configuration;

public sealed class DatabaseSettings
{
    public const string SectionName = "Database";

    public int MigrationMaxRetries { get; init; } = 15;

    public int RetryDelaySeconds { get; init; } = 5;

    public int DefaultLatestLogsCount { get; init; } = 50;
}
