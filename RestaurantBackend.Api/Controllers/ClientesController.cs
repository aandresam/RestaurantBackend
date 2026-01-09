using Microsoft.AspNetCore.Mvc;
using RestaurantBackend.Application.DTOs.Cliente;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Api.Controllers;

[ApiController]
[Route("api/clientes")]
public sealed class ClientesController(IClienteService clienteService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
        => Ok(await clienteService.ListAsync(cancellationToken).ConfigureAwait(false));

    [HttpGet("{idCliente:int}")]
    public async Task<IActionResult> GetById([FromRoute] int idCliente, CancellationToken cancellationToken)
    {
        var result = await clienteService.GetByIdAsync(idCliente, cancellationToken).ConfigureAwait(false);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClienteDto request, CancellationToken cancellationToken)
    {
        var result = await clienteService.CreateAsync(request, cancellationToken).ConfigureAwait(false);
        return CreatedAtAction(nameof(GetById), new { idCliente = result.IdCliente }, result);
    }

    [HttpPut("{idCliente:int}")]
    public async Task<IActionResult> Update([FromRoute] int idCliente, [FromBody] UpdateClienteDto request, CancellationToken cancellationToken)
    {
        var updated = await clienteService.UpdateAsync(idCliente, request, cancellationToken).ConfigureAwait(false);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{idCliente:int}")]
    public async Task<IActionResult> Delete([FromRoute] int idCliente, CancellationToken cancellationToken)
    {
        var deleted = await clienteService.DeleteAsync(idCliente, cancellationToken).ConfigureAwait(false);
        return deleted ? NoContent() : NotFound();
    }
}