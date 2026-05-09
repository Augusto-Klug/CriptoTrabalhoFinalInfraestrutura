using CriptoTrabalhoFinalInfraestrutura.Models;
using CriptoTrabalhoFinalInfraestrutura.Services;
using Microsoft.AspNetCore.Mvc;

namespace CriptoTrabalhoFinalInfraestrutura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrecoReferenciasController : ControllerBase
{
    private readonly IBinanceService _binanceService;

    public PrecoReferenciasController(IBinanceService binanceService)
    {
        _binanceService = binanceService;
    }

    [HttpGet("reference-price")]
    public async Task<IActionResult> GetReferencePrice([FromQuery] PrecoReferenciaRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _binanceService.GetReferencePriceAsync(request.Symbol.ToUpperInvariant());
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
