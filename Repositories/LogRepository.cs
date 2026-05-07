using CriptoTrabalhoFinalInfraestrutura.Data;
using CriptoTrabalhoFinalInfraestrutura.Models;
using Microsoft.EntityFrameworkCore;

namespace CriptoTrabalhoFinalInfraestrutura.Repositories;

public sealed class LogRepository(AppDbContext dbContext) : ILogRepository
{
    public async Task<IReadOnlyList<LogEntry>> GetLatestAsync(int count, CancellationToken cancellationToken = default)
    {
        return await dbContext.Logs
            .AsNoTracking()
            .OrderByDescending(log => log.Horario)
            .ThenByDescending(log => log.Id)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public Task<LogEntry?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return dbContext.Logs
            .AsNoTracking()
            .FirstOrDefaultAsync(log => log.Id == id, cancellationToken);
    }

    public async Task<LogEntry> AddAsync(LogEntry logEntry, CancellationToken cancellationToken = default)
    {
        dbContext.Logs.Add(logEntry);
        await dbContext.SaveChangesAsync(cancellationToken);
        return logEntry;
    }
}
