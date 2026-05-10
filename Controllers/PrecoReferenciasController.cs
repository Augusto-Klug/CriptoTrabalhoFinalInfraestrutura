using CriptoTrabalhoFinalInfraestrutura.Entities;
using CriptoTrabalhoFinalInfraestrutura.Models;
using CriptoTrabalhoFinalInfraestrutura.Repositories;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Microsoft.AspNetCore.Mvc;

namespace CriptoTrabalhoFinalInfraestrutura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrecoReferenciasController : ControllerBase
{
    private readonly IBinanceService _binanceService;
    private readonly ILogRepository _logRepository;

    public PrecoReferenciasController(IBinanceService binanceService, ILogRepository logRepository)
    {
        _binanceService = binanceService;
        _logRepository = logRepository;
    }

    [HttpGet("reference-price")]
    public async Task<IActionResult> GetReferencePrice([FromQuery] PrecoReferenciaRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var symbol = request.Symbol.ToUpperInvariant();
            var result = await _binanceService.GetReferencePriceAsync(symbol);

            await _logRepository.AddAsync(new LogEntity
            {
                Horario = DateTime.UtcNow,
                Criptos = [symbol],
                Mensagem = $"Consulta de preco de referencia para {symbol}."
            }, cancellationToken);

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500, new
            {
                message = "Erro ao consultar a API da Binance."
            });
        }
    }
}
