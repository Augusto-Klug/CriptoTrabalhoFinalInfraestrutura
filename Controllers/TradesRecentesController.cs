using CriptoTrabalhoFinalInfraestrutura.Entities;
using CriptoTrabalhoFinalInfraestrutura.Models;
using CriptoTrabalhoFinalInfraestrutura.Repositories;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Microsoft.AspNetCore.Mvc;

namespace CriptoTrabalhoFinalInfraestrutura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradesRecentesController : ControllerBase
{
    private readonly IBinanceService _binanceService;
    private readonly ILogRepository _logRepository;

    public TradesRecentesController(IBinanceService binanceService, ILogRepository logRepository)
    {
        _binanceService = binanceService;
        _logRepository = logRepository;
    }

    [HttpGet("recent-trades")]
    public async Task<IActionResult> GetRecentTrades([FromQuery] RecentTradesRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var symbol = request.Symbol.ToUpperInvariant();
            var result = await _binanceService.GetRecentTradesAsync(symbol, request.Limit);

            await _logRepository.AddAsync(new LogEntity
            {
                Horario = DateTime.UtcNow,
                Criptos = [symbol],
                Mensagem = $"Consulta de trades recentes para {symbol} com limite {request.Limit}."
            }, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar a solicitação: {ex.Message}");
        }
    }
}
