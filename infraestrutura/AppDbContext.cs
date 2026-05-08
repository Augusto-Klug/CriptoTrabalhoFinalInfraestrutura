using CriptoTrabalhoFinalInfraestrutura.Entities;
using Microsoft.EntityFrameworkCore;

namespace CriptoTrabalhoFinalInfraestrutura.infraestrutura;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<LogEntity> Logs => Set<LogEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var logEntry = modelBuilder.Entity<LogEntity>();

        logEntry.ToTable("logs");
        logEntry.HasKey(log => log.Id);
        logEntry.Property(log => log.Id).HasColumnName("id");
        logEntry.Property(log => log.Horario).HasColumnName("horario").HasColumnType("datetime2").IsRequired();
        logEntry.Property(log => log.Criptos)
            .HasColumnName("criptos")
            .HasColumnType("varchar(max)")
            .HasConversion(
                criptos => LogEntryConversions.SerializeCriptos(criptos),
                criptos => LogEntryConversions.DeserializeCriptos(criptos))
            .Metadata.SetValueComparer(LogEntryConversions.CriptosComparer);
        logEntry.Property(log => log.Mensagem).HasColumnName("mensagem").HasColumnType("varchar(max)").IsRequired();
    }
}
