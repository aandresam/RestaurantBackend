using Microsoft.AspNetCore.Mvc;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Api.Controllers;

[ApiController]
[Route("api/reportes")]
public class ReportsController(IReportService reportService) : ControllerBase
{
    [HttpGet("ventas-por-mesero")]
    public async Task<IActionResult> GetVentasPorMesero(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        CancellationToken cancellationToken)
    {
        var result = await reportService.GetTotalVendidoPorMeseroAsync(startDate, endDate, cancellationToken);
        return Ok(result);
    }

    [HttpGet("clientes-consumo-minimo")]
    public async Task<IActionResult> GetClientesConConsumoMinimo(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] decimal minimumSpend,
        CancellationToken cancellationToken)
    {
        var result = await reportService.GetClientesConConsumoMinimoAsync(startDate, endDate, minimumSpend, cancellationToken);
        return Ok(result);
    }

    [HttpGet("producto-mas-vendido")]
    public async Task<IActionResult> GetProductoMasVendido(
        [FromQuery] int year,
        [FromQuery] int month,
        CancellationToken cancellationToken)
    {
        var result = await reportService.GetProductoMasVendidoDelMesAsync(year, month, cancellationToken);
        return Ok(result);
    }
}