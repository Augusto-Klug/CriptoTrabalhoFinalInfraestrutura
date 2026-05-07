using CriptoTrabalhoFinalInfraestrutura.Models;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Microsoft.AspNetCore.Mvc;

namespace CriptoTrabalhoFinalInfraestrutura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradesRecentesController : ControllerBase
{
    private readonly IBinanceService _binanceService;

    public TradesRecentesController(IBinanceService binanceService)
    {
        _binanceService = binanceService;
    }

    [HttpGet("recent-trades")]
    public async Task<IActionResult> GetRecentTrades([FromQuery] RecentTradesRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _binanceService.GetRecentTradesAsync(request.Symbol.ToUpper(), request.Limit);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar a solicitação: {ex.Message}");
        }
    }
}
