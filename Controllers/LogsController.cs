using CriptoTrabalhoFinalInfraestrutura.DTOs;
using CriptoTrabalhoFinalInfraestrutura.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CriptoTrabalhoFinalInfraestrutura.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LogsController(
    ILogRepository logRepository,
    IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LogDto>>> GetLatestAsync(
        [FromQuery] int? quantidade,kjsdfjkjhal
        CancellationToken cancellationToken)
    {
        var defaultLatestLogsCount = configuration.GetValue<int?>("Database:DefaultLatestLogsCount") ?? 50;
        var count = quantidade.GetValueOrDefault(defaultLatestLogsCount);
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
