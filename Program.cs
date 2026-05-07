using CriptoTrabalhoFinalInfraestrutura.Integracao.Binance;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Binance Integration
builder.Services.AddHttpClient<IBinanceIntegration, BinanceIntegration>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Binance:BaseUrl"] ?? "https://api.binance.com/");
});

builder.Services.AddScoped<IBinanceService, BinanceService>();
builder.Services.AddScoped<IBinanceTickerService, BinanceTickerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
