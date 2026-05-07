using CriptoTrabalhoFinalInfraestrutura.Entities;

namespace CriptoTrabalhoFinalInfraestrutura.Repositories;

public interface ILogRepository
{
    Task<IReadOnlyList<LogEntity>> GetLatestAsync(int count, CancellationToken cancellationToken = default);

    Task<LogEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<LogEntity> AddAsync(LogEntity logEntity, CancellationToken cancellationToken = default);
}
