using CriptoTrabalhoFinalInfraestrutura.Configuration;
using CriptoTrabalhoFinalInfraestrutura.DTOs;
using CriptoTrabalhoFinalInfraestrutura.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CriptoTrabalhoFinalInfraestrutura.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LogsController(
    ILogRepository logRepository,
    IOptions<DatabaseSettings> databaseSettings) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LogDto>>> GetLatestAsync(
        [FromQuery] int? quantidade,
        CancellationToken cancellationToken)
    {
        var count = quantidade.GetValueOrDefault(databaseSettings.Value.DefaultLatestLogsCount);
        if (count <= 0)
        {
            return BadRequest("A quantidade deve ser maior que zero.");
        }

        var logs = await logRepository.GetLatestAsync(count, cancellationToken);

        return Ok(logs.Select(log => new LogDto
        {
            Id = log.Id,
            Horario = log.Horario,
            Criptos = log.Criptos,
            Mensagem = log.Mensagem
        }).ToList());
    }
}
