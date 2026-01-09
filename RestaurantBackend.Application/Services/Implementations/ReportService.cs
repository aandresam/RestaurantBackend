
using RestaurantBackend.Application.DTOs.Reports;
using RestaurantBackend.Application.Services.Interfaces;
using RestaurantBackend.Application.Repositories;

namespace RestaurantBackend.Application.Services.Implementations;

public sealed class ReportService(IReportRepository reportRepository) : IReportService
{
    public Task<IReadOnlyList<MeseroVentaDto>> GetTotalVendidoPorMeseroAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
        => reportRepository.GetTotalSoldByWaiterAsync(startDate, endDate, cancellationToken);

    public Task<IReadOnlyList<ClienteConsumoDto>> GetClientesConConsumoMinimoAsync(
        DateTime startDate,
        DateTime endDate,
        decimal minimumSpend,
        CancellationToken cancellationToken = default)
        => reportRepository.GetCustomersWithMinimumSpendAsync(startDate, endDate, minimumSpend, cancellationToken);

    public Task<ProductoMasVendidoDto?> GetProductoMasVendidoDelMesAsync(
        int year,
        int month,
        CancellationToken cancellationToken = default)
        => reportRepository.GetCustomersWithMinimumSpendAsync(year, month, cancellationToken);
}