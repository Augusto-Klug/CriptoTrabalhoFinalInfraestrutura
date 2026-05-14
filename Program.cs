using CriptoTrabalhoFinalInfraestrutura.Extensions;
using CriptoTrabalhoFinalInfraestrutura.infraestrutura;
using CriptoTrabalhoFinalInfraestrutura.Repositories;
using Microsoft.EntityFrameworkCore;
using CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ILogRepository, LogRepository>();

// Binance Integration
builder.Services.AddHttpClient<IBinanceIntegration, BinanceIntegration>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Binance:BaseUrl"] ?? "https://api.binance.com/");
});

builder.Services.AddScoped<IBinanceService, BinanceService>();
builder.Services.AddScoped<IBinanceTickerService, BinanceTickerService>();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseAuthorization();

app.MapControllers();

await app.InitializeDatabaseAsync();

app.Run();
