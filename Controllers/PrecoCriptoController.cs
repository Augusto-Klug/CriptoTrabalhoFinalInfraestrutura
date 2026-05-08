using CriptoTrabalhoFinalInfraestrutura.Models;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Microsoft.AspNetCore.Mvc;

namespace CriptoTrabalhoFinalInfraestrutura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrecoCriptoController : ControllerBase
{
    private readonly IBinanceTickerService _binanceTickerService;

    public PrecoCriptoController(IBinanceTickerService binanceTickerService)
    {
        _binanceTickerService = binanceTickerService;
    }

    [HttpGet("price")]
    public async Task<IActionResult> GetPrice([FromQuery] TickerPriceRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _binanceTickerService.GetPriceAsync(request.Symbol.ToUpper());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar a solicitacao: {ex.Message}");
        }
    }
}
