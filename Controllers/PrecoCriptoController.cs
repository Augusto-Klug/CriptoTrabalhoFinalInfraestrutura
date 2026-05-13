using CriptoTrabalhoFinalInfraestrutura.Entities;
using CriptoTrabalhoFinalInfraestrutura.Models;
using CriptoTrabalhoFinalInfraestrutura.Repositories;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Microsoft.AspNetCore.Mvc;

namespace CriptoTrabalhoFinalInfraestrutura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrecoCriptoController : ControllerBase
{
    private readonly IBinanceTickerService _binanceTickerService;
    private readonly ILogRepository _logRepository;

    public PrecoCriptoController(IBinanceTickerService binanceTickerService, ILogRepository logRepository)
    {
        _binanceTickerService = binanceTickerService;
        _logRepository = logRepository;
    }

    [HttpGet("price")]
    public async Task<IActionResult> GetPrice([FromQuery] TickerPriceRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var symbol = request.Symbol.ToUpperInvariant();
            var result = await _binanceTickerService.GetPriceAsync(symbol);

            await _logRepository.AddAsync(new LogEntity
            {
                Horario = DateTime.UtcNow,
                Criptos = [symbol],
                Mensagem = $"Consulta de preco atual para {symbol}."
            }, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar a solicitacao: {ex.Message}");
        }
    }
}
