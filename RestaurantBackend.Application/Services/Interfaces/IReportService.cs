using RestaurantBackend.Application.DTOs.Reports;

namespace RestaurantBackend.Application.Services.Interfaces;

public interface IReportService
{
    Task<IReadOnlyList<MeseroVentaDto>> GetTotalVendidoPorMeseroAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ClienteConsumoDto>> GetClientesConConsumoMinimoAsync(
        DateTime startDate,
        DateTime endDate,
        decimal minimumSpend,
        CancellationToken cancellationToken = default);

    Task<ProductoMasVendidoDto?> GetProductoMasVendidoDelMesAsync(
        int year,
        int month,
        CancellationToken cancellationToken = default);
}