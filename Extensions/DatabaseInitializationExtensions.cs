using CriptoTrabalhoFinalInfraestrutura.infraestrutura;
using Microsoft.EntityFrameworkCore;

namespace CriptoTrabalhoFinalInfraestrutura.Extensions;

public static class DatabaseInitializationExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
            .CreateLogger("DatabaseInitialization");
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var migrationMaxRetries = configuration.GetValue<int?>("Database:MigrationMaxRetries") ?? 20;
        var retryDelaySeconds = configuration.GetValue<int?>("Database:RetryDelaySeconds") ?? 5;

        for (var attempt = 1; attempt <= migrationMaxRetries; attempt++)
        {
            try
            {
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("Database migrations applied successfully.");
                return;
            }
            catch (Exception ex) when (attempt < migrationMaxRetries)
            {
                logger.LogWarning(
                    ex,
                    "Database migration attempt {Attempt} of {MaxAttempts} failed. Retrying in {DelaySeconds} seconds.",
                    attempt,
                    migrationMaxRetries,
                    retryDelaySeconds);

                await Task.Delay(TimeSpan.FromSeconds(retryDelaySeconds));
            }
        }

        await dbContext.Database.MigrateAsync();
    }
}
