using Microsoft.AspNetCore.Mvc;
using RestaurantBackend.Application.DTOs.Supervisor;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Api.Controllers;

[ApiController]
[Route("api/supervisores")]
public sealed class SupervisoresController(ISupervisorService supervisorService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
        => Ok(await supervisorService.ListAsync(cancellationToken));

    [HttpGet("{idSupervisor:int}")]
    public async Task<IActionResult> GetById([FromRoute] int idSupervisor, CancellationToken cancellationToken)
    {
        var result = await supervisorService.GetByIdAsync(idSupervisor, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSupervisorDto request, CancellationToken cancellationToken)
    {
        var result = await supervisorService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { idSupervisor = result.IdSupervisor }, result);
    }

    [HttpPut("{idSupervisor:int}")]
    public async Task<IActionResult> Update([FromRoute] int idSupervisor, [FromBody] UpdateSupervisorDto request, CancellationToken cancellationToken)
    {
        var updated = await supervisorService.UpdateAsync(idSupervisor, request, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{idSupervisor:int}")]
    public async Task<IActionResult> Delete([FromRoute] int idSupervisor, CancellationToken cancellationToken)
    {
        var deleted = await supervisorService.DeleteAsync(idSupervisor, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}