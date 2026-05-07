using CriptoTrabalhoFinalInfraestrutura.Configuration;
using CriptoTrabalhoFinalInfraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CriptoTrabalhoFinalInfraestrutura.Extensions;

public static class DatabaseInitializationExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
            .CreateLogger("DatabaseInitialization");
        var settings = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        for (var attempt = 1; attempt <= settings.MigrationMaxRetries; attempt++)
        {
            try
            {
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("Database migrations applied successfully.");
                return;
            }
            catch (Exception ex) when (attempt < settings.MigrationMaxRetries)
            {
                logger.LogWarning(
                    ex,
                    "Database migration attempt {Attempt} of {MaxAttempts} failed. Retrying in {DelaySeconds} seconds.",
                    attempt,
                    settings.MigrationMaxRetries,
                    settings.RetryDelaySeconds);

                await Task.Delay(TimeSpan.FromSeconds(settings.RetryDelaySeconds));
            }
        }

        await dbContext.Database.MigrateAsync();
    }
}
