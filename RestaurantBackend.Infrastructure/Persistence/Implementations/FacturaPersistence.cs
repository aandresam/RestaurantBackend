using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Application.DTOs.Factura;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Application.Repositories;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Persistence.Implementations;

public sealed class FacturaPersistence(
    RestaurantDbContext dbContext,
    IRepository<Factura, int> facturaRepository,
    IRepository<DetalleFactura, int> detalleRepository,
    IUnitOfWork unitOfWork) : IFacturaPersistence
{
    public async Task<FacturaDto> CreateFacturaAsync(CreateFacturaDto request, CancellationToken cancellationToken = default)
    {
        var nuevoNroFactura = await dbContext.Facturas
            .AsNoTracking()
            .MaxAsync(x => (int?)x.NroFactura, cancellationToken) ?? 0;

        nuevoNroFactura++;

        var factura = new Factura
        {
            NroFactura = nuevoNroFactura,
            IdCliente = request.IdCliente,
            IdMesa = request.IdMesa,
            IdMesero = request.IdMesero,
            Fecha = DateTime.UtcNow,
        };

        await facturaRepository.AddAsync(factura, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        decimal totalFacturado = 0m;

        foreach (var dto in request.Detalles)
        {
            var detalle = new DetalleFactura
            {
                IdFactura = factura.IdFactura,
                IdSupervisor = dto.IdSupervisor,
                Plato = dto.Plato,
                Valor = dto.Valor,
            };

            await detalleRepository.AddAsync(detalle, cancellationToken);
            totalFacturado += dto.Valor;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new FacturaDto
        {
            IdFactura = factura.IdFactura,
            NroFactura = factura.NroFactura,
            Fecha = factura.Fecha,
            TotalFacturado = totalFacturado,
        };
    }

    public async Task<IReadOnlyList<FacturaFullDto>> ListAsync(
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.Facturas.AsNoTracking();

        if (startDate is not null)
        {
            query = query.Where(x => x.Fecha >= startDate.Value);
        }

        if (endDate is not null)
        {
            query = query.Where(x => x.Fecha <= endDate.Value);
        }

        return await query
            .OrderByDescending(x => x.Fecha)
            .Select(x => new FacturaFullDto
            {
                IdFactura = x.IdFactura,
                NroFactura = x.NroFactura,
                Fecha = x.Fecha,
                IdCliente = x.IdCliente,
                ClienteNombreCompleto = x.IdClienteNavigation.Nombres + " " + x.IdClienteNavigation.Apellidos,
                IdMesa = x.IdMesa,
                IdMesero = x.IdMesero,
                MeseroNombreCompleto = x.IdMeseroNavigation.Nombres + " " + x.IdMeseroNavigation.Apellidos,
                TotalFacturado = x.DetalleFacturas.Sum(d => (decimal?)d.Valor) ?? 0m,
                Detalles = x.DetalleFacturas
                    .Select(d => new FacturaDetalleDto
                    {
                        IdDetalleFactura = d.IdDetalleFactura,
                        IdSupervisor = d.IdSupervisor,
                        SupervisorNombreCompleto = d.IdSupervisorNavigation.Nombres + " " + d.IdSupervisorNavigation.Apellidos,
                        Plato = d.Plato,
                        Valor = d.Valor,
                    })
                    .ToList(),
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<FacturaFullDto?> GetByIdAsync(int idFactura, CancellationToken cancellationToken = default)
    {
        return await dbContext.Facturas
            .AsNoTracking()
            .Where(x => x.IdFactura == idFactura)
            .Select(x => new FacturaFullDto
            {
                IdFactura = x.IdFactura,
                NroFactura = x.NroFactura,
                Fecha = x.Fecha,
                IdCliente = x.IdCliente,
                ClienteNombreCompleto = x.IdClienteNavigation.Nombres + " " + x.IdClienteNavigation.Apellidos,
                IdMesa = x.IdMesa,
                IdMesero = x.IdMesero,
                MeseroNombreCompleto = x.IdMeseroNavigation.Nombres + " " + x.IdMeseroNavigation.Apellidos,
                TotalFacturado = x.DetalleFacturas.Sum(d => (decimal?)d.Valor) ?? 0m,
                Detalles = x.DetalleFacturas
                    .Select(d => new FacturaDetalleDto
                    {
                        IdDetalleFactura = d.IdDetalleFactura,
                        IdSupervisor = d.IdSupervisor,
                        SupervisorNombreCompleto = d.IdSupervisorNavigation.Nombres + " " + d.IdSupervisorNavigation.Apellidos,
                        Plato = d.Plato,
                        Valor = d.Valor,
                    })
                    .ToList(),
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<FacturaFullDto?> AddDetalleAsync(int idFactura, AddDetalleFacturaDto request, CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.Facturas
            .AsNoTracking()
            .AnyAsync(x => x.IdFactura == idFactura, cancellationToken)
            .ConfigureAwait(false);

        if (!exists)
        {
            return null;
        }

        var detalle = new DetalleFactura
        {
            IdFactura = idFactura,
            IdSupervisor = request.IdSupervisor,
            Plato = request.Plato,
            Valor = request.Valor,
        };

        await detalleRepository.AddAsync(detalle, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(idFactura, cancellationToken);
    }

    public async Task<FacturaFullDto?> ReplaceDetallesAsync(int idFactura, ReplaceDetallesFacturaDto request, CancellationToken cancellationToken = default)
    {
        var facturaExists = await dbContext.Facturas
            .AsNoTracking()
            .AnyAsync(x => x.IdFactura == idFactura, cancellationToken);

        if (!facturaExists)
        {
            return null;
        }

        var detallesActuales = await dbContext.DetalleFacturas
            .Where(x => x.IdFactura == idFactura)
            .ToListAsync(cancellationToken);

        if (detallesActuales.Count > 0)
        {
            dbContext.DetalleFacturas.RemoveRange(detallesActuales);
        }

        foreach (var dto in request.Detalles)
        {
            var detalle = new DetalleFactura
            {
                IdFactura = idFactura,
                IdSupervisor = dto.IdSupervisor,
                Plato = dto.Plato,
                Valor = dto.Valor,
            };

            await dbContext.DetalleFacturas.AddAsync(detalle, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return await GetByIdAsync(idFactura, cancellationToken);
    }

    public async Task<bool> DeleteDetalleAsync(int idFactura, int idDetalleFactura, CancellationToken cancellationToken = default)
    {
        var detalle = await dbContext.DetalleFacturas
            .FirstOrDefaultAsync(x => x.IdFactura == idFactura && x.IdDetalleFactura == idDetalleFactura, cancellationToken);

        if (detalle is null)
        {
            return false;
        }

        dbContext.DetalleFacturas.Remove(detalle);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}