using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CriptoTrabalhoFinalInfraestrutura.infraestrutura;

public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=localhost,11433;Database=CriptoTrabalhoFinalInfraestrutura;User Id=sa;Password=CriptoTrabalhoFinal@2026;TrustServerCertificate=True;Encrypt=True;MultipleActiveResultSets=True");

        return new AppDbContext(optionsBuilder.Options);
    }
}
