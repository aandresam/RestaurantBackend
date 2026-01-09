using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Application.DTOs.Reports;
using RestaurantBackend.Application.Repositories;
using RestaurantBackend.Infrastructure.Persistence;

namespace RestaurantBackend.Infrastructure.Repositories;

public sealed class ReportRepository(RestaurantDbContext dbContext) : IReportRepository
{
    public async Task<IReadOnlyList<MeseroVentaDto>> GetTotalSoldByWaiterAsync(
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Meseros
            .AsNoTracking()
            .GroupJoin(
                dbContext.Facturas.AsNoTracking().Where(f => f.Fecha >= startDate && f.Fecha <= endDate),
                m => m.IdMesero,
                f => f.IdMesero,
                (m, facturas) => new { m, facturas })
            .SelectMany(
                x => x.facturas.DefaultIfEmpty(),
                (x, f) => new { x.m, Factura = f })
            .GroupJoin(
                dbContext.DetalleFacturas.AsNoTracking(),
                x => x.Factura != null ? x.Factura.IdFactura : 0m,
                d => d.IdFactura,
                (x, detalles) => new { x.m, detalles })
            .GroupBy(x => new { x.m.IdMesero, x.m.Nombres, x.m.Apellidos })
            .Select(g => new MeseroVentaDto
            {
                IdMesero = g.Key.IdMesero,
                Nombres = g.Key.Nombres,
                Apellidos = g.Key.Apellidos,
                TotalVendido = g.SelectMany(x => x.detalles).Sum(d => (decimal?)d.Valor) ?? 0m,
            })
            .OrderByDescending(x => x.TotalVendido)
            .ThenBy(x => x.Nombres)
            .ThenBy(x => x.Apellidos)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ClienteConsumoDto>> GetCustomersWithMinimumSpendAsync(
        DateTime startDate,
        DateTime endDate,
        decimal minimumSpend,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Clientes
            .AsNoTracking()
            .Join(
                dbContext.Facturas.AsNoTracking().Where(f => f.Fecha >= startDate && f.Fecha <= endDate),
                c => c.IdCliente,
                f => f.IdCliente,
                (c, f) => new { c, f })
            .Join(
                dbContext.DetalleFacturas.AsNoTracking(),
                x => x.f.IdFactura,
                d => d.IdFactura,
                (x, d) => new { x.c, Detalle = d })
            .GroupBy(x => new { x.c.IdCliente, x.c.Identificacion, x.c.Nombres, x.c.Apellidos })
            .Select(g => new ClienteConsumoDto
            {
                IdCliente = g.Key.IdCliente,
                Identificacion = g.Key.Identificacion,
                Nombres = g.Key.Nombres,
                Apellidos = g.Key.Apellidos,
                TotalConsumo = g.Sum(x => x.Detalle.Valor),
            })
            .Where(x => x.TotalConsumo >= minimumSpend)
            .OrderByDescending(x => x.TotalConsumo)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductoMasVendidoDto?> GetCustomersWithMinimumSpendAsync(
        int year,
        int month,
        CancellationToken cancellationToken = default)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1);

        return await dbContext.Facturas
            .AsNoTracking()
            .Where(f => f.Fecha >= startDate && f.Fecha < endDate)
            .Join(
                dbContext.DetalleFacturas.AsNoTracking(),
                f => f.IdFactura,
                d => d.IdFactura,
                (_, d) => d)
            .GroupBy(d => d.Plato)
            .Select(g => new ProductoMasVendidoDto
            {
                Plato = g.Key,
                Cantidad = g.Count(),
                TotalFacturado = g.Sum(x => x.Valor),
            })
            .OrderByDescending(x => x.Cantidad)
            .ThenByDescending(x => x.TotalFacturado)
            .FirstOrDefaultAsync(cancellationToken);
    }
}