using Microsoft.AspNetCore.Mvc;
using RestaurantBackend.Application.DTOs.Mesero;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Api.Controllers;

[ApiController]
[Route("api/meseros")]
public sealed class MeserosController(IMeseroService meseroService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
        => Ok(await meseroService.ListAsync(cancellationToken).ConfigureAwait(false));

    [HttpGet("{idMesero:int}")]
    public async Task<IActionResult> GetById([FromRoute] int idMesero, CancellationToken cancellationToken)
    {
        var result = await meseroService.GetByIdAsync(idMesero, cancellationToken).ConfigureAwait(false);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMeseroDto request, CancellationToken cancellationToken)
    {
        var result = await meseroService.CreateAsync(request, cancellationToken).ConfigureAwait(false);
        return CreatedAtAction(nameof(GetById), new { idMesero = result.IdMesero }, result);
    }

    [HttpPut("{idMesero:int}")]
    public async Task<IActionResult> Update([FromRoute] int idMesero, [FromBody] UpdateMeseroDto request, CancellationToken cancellationToken)
    {
        var updated = await meseroService.UpdateAsync(idMesero, request, cancellationToken).ConfigureAwait(false);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{idMesero:int}")]
    public async Task<IActionResult> Delete([FromRoute] int idMesero, CancellationToken cancellationToken)
    {
        var deleted = await meseroService.DeleteAsync(idMesero, cancellationToken).ConfigureAwait(false);
        return deleted ? NoContent() : NotFound();
    }
}