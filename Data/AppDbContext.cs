using CriptoTrabalhoFinalInfraestrutura.Models;
using Microsoft.EntityFrameworkCore;

namespace CriptoTrabalhoFinalInfraestrutura.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<LogEntry> Logs => Set<LogEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var logEntry = modelBuilder.Entity<LogEntry>();

        logEntry.ToTable("logs");
        logEntry.HasKey(log => log.Id);
        logEntry.Property(log => log.Id).HasColumnName("id");
        logEntry.Property(log => log.Horario).HasColumnName("horario").HasColumnType("datetime2").IsRequired();
        logEntry.Property(log => log.Criptos).HasColumnName("criptos").HasColumnType("varchar(max)").IsRequired();
        logEntry.Property(log => log.Mensagem).HasColumnName("mensagem").HasColumnType("varchar(max)").IsRequired();
    }
}
