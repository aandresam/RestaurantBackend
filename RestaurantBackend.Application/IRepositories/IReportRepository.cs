using RestaurantBackend.Application.DTOs.Reports;

namespace RestaurantBackend.Application.Repositories;

public interface IReportRepository
{
    Task<IReadOnlyList<MeseroVentaDto>> GetTotalSoldByWaiterAsync(
        DateTime startDate,
        DateTime endDateInclusive,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ClienteConsumoDto>> GetCustomersWithMinimumSpendAsync(
        DateTime startDate,
        DateTime endDateInclusive,
        decimal minimumSpend,
        CancellationToken cancellationToken = default);

    Task<ProductoMasVendidoDto?> GetCustomersWithMinimumSpendAsync(
        int year,
        int month,
        CancellationToken cancellationToken = default);
}