using Microsoft.AspNetCore.Mvc;
using RestaurantBackend.Application.DTOs.Factura;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Api.Controllers;

[ApiController]
[Route("api/facturas")]
public class FacturasController(IFacturaService facturaService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? hasta,
        CancellationToken cancellationToken)
    {
        var result = await facturaService.ListAsync(startDate, hasta, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("{idFactura:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int idFactura,
        CancellationToken cancellationToken)
    {
        var result = await facturaService.GetByIdAsync(idFactura, cancellationToken).ConfigureAwait(false);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFactura(
        [FromBody] CreateFacturaDto request,
        CancellationToken cancellationToken)
    {
        var result = await facturaService.CreateFacturaAsync(request, cancellationToken).ConfigureAwait(false);
        return CreatedAtAction(nameof(GetById), new { idFactura = result.IdFactura }, result);
    }

    [HttpPost("{idFactura:int}/detalles")]
    public async Task<IActionResult> AddDetalle(
        [FromRoute] int idFactura,
        [FromBody] AddDetalleFacturaDto request,
        CancellationToken cancellationToken)
    {
        var result = await facturaService.AddDetalleAsync(idFactura, request, cancellationToken).ConfigureAwait(false);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{idFactura:int}/detalles")]
    public async Task<IActionResult> ReplaceDetalles(
        [FromRoute] int idFactura,
        [FromBody] ReplaceDetallesFacturaDto request,
        CancellationToken cancellationToken)
    {
        var result = await facturaService.ReplaceDetallesAsync(idFactura, request, cancellationToken).ConfigureAwait(false);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{idFactura:int}/detalles/{idDetalleFactura:int}")]
    public async Task<IActionResult> DeleteDetalle(
        [FromRoute] int idFactura,
        [FromRoute] int idDetalleFactura,
        CancellationToken cancellationToken)
    {
        var deleted = await facturaService.DeleteDetalleAsync(idFactura, idDetalleFactura, cancellationToken).ConfigureAwait(false);
        return deleted ? NoContent() : NotFound();
    }
}