using CriptoTrabalhoFinalInfraestrutura.Entities;
using CriptoTrabalhoFinalInfraestrutura.infraestrutura;
using Microsoft.EntityFrameworkCore;

namespace CriptoTrabalhoFinalInfraestrutura.Repositories;

public sealed class LogRepository(AppDbContext dbContext) : ILogRepository
{
    public async Task<IReadOnlyList<LogEntity>> GetLatestAsync(int count, CancellationToken cancellationToken = default)
    {
        return await dbContext.Logs
            .AsNoTracking()
            .OrderByDescending(log => log.Horario)
            .ThenByDescending(log => log.Id)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public Task<LogEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return dbContext.Logs
            .AsNoTracking()
            .FirstOrDefaultAsync(log => log.Id == id, cancellationToken);
    }

    public async Task<LogEntity> AddAsync(LogEntity logEntity, CancellationToken cancellationToken = default)
    {
        dbContext.Logs.Add(logEntity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return logEntity;
    }
}
