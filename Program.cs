using CriptoTrabalhoFinalInfraestrutura.Configuration;
using CriptoTrabalhoFinalInfraestrutura.Data;
using CriptoTrabalhoFinalInfraestrutura.Extensions;
using CriptoTrabalhoFinalInfraestrutura.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(DatabaseSettings.SectionName));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ILogRepository, LogRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

await app.InitializeDatabaseAsync();

app.Run();
