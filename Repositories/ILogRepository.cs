using CriptoTrabalhoFinalInfraestrutura.Models;

namespace CriptoTrabalhoFinalInfraestrutura.Repositories;

public interface ILogRepository
{
    Task<IReadOnlyList<LogEntry>> GetLatestAsync(int count, CancellationToken cancellationToken = default);

    Task<LogEntry?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<LogEntry> AddAsync(LogEntry logEntry, CancellationToken cancellationToken = default);
}
