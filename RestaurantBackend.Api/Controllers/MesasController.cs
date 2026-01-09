using Microsoft.AspNetCore.Mvc;
using RestaurantBackend.Application.DTOs.Mesa;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Api.Controllers;

[ApiController]
[Route("api/mesas")]
public sealed class MesasController(IMesaService mesaService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
        => Ok(await mesaService.ListAsync(cancellationToken));

    [HttpGet("{nroMesa:int}")]
    public async Task<IActionResult> GetByNroMesa([FromRoute] int nroMesa, CancellationToken cancellationToken)
    {
        var result = await mesaService.GetByNroMesaAsync(nroMesa, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMesaDto request, CancellationToken cancellationToken)
    {
        var result = await mesaService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetByNroMesa), new { nroMesa = result.NroMesa }, result);
    }

    [HttpPut("{nroMesa:int}")]
    public async Task<IActionResult> Update([FromRoute] int nroMesa, [FromBody] UpdateMesaDto request, CancellationToken cancellationToken)
    {
        var updated = await mesaService.UpdateAsync(nroMesa, request, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{nroMesa:int}")]
    public async Task<IActionResult> Delete([FromRoute] int nroMesa, CancellationToken cancellationToken)
    {
        var deleted = await mesaService.DeleteAsync(nroMesa, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}