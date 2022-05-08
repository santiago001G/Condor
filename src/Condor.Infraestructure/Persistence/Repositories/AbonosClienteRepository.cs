using Condor.Core.Entities;
using Condor.Core.Interfaces;
using Condor.Infraestructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Condor.Infraestructure.Persistence.Repositories
{
    public class AbonosClienteRepository : IAbonosClienteRepository
    {
        private readonly CondorContext _context;

        public AbonosClienteRepository(CondorContext context)
        {
            _context = context;
        }

        public async Task<bool> RegistrarAbono(AbonoCliente abono)
        {
            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var producto = await _context.ProductosCliente.FirstOrDefaultAsync(x => x.Id == abono.IdProductoCliente);
                    producto.FechaUltimoAbono = abono.FechaAbono;
                    _context.AbonosCliente.Add(abono);
                    int resultado = await _context.SaveChangesAsync();

                    transaccion.Commit();
                    return resultado > default(int);
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return false;
                }

            }
        }

        public async Task<bool> RegistrarAbonos(IEnumerable<AbonoCliente> abonos)
        {
            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    List<int> idsProductos = abonos.Select(x => x.IdProductoCliente).ToList();
                    var productos = await _context.ProductosCliente.Where(x => idsProductos.Contains(x.Id)).ToListAsync();
                    productos.ForEach(x => x.FechaUltimoAbono = abonos.First().FechaAbono);
                    await _context.SaveChangesAsync();
                    _context.AbonosCliente.AddRange(abonos);
                    int resultado = await _context.SaveChangesAsync();

                    transaccion.Commit();

                    return resultado > default(int);
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }

        // Método para actualzación de pila de registros
        public async Task<bool> ActualizarAbonos(IEnumerable<AbonoCliente> abonos)
        {
            List<int> idsAbonos = abonos.Select(x => x.Id).ToList();

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var abonosActualizables = await _context.AbonosCliente.Where(x => idsAbonos.Contains(x.Id)).ToListAsync();

                    foreach (var abono in abonosActualizables)
                    {
                        abono.Valor = abonos.FirstOrDefault(x => x.Id == abono.Id).Valor;
                    }

                    int resultado = await _context.SaveChangesAsync();
                    transaccion.Commit();
                    return resultado > default(int);

                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }

        public async Task<IEnumerable<ProductosCliente>> ConsultarAbonosClientePorFecha(int idCliente, DateTime fecha)
        {
            var abonos = await _context.ProductosCliente.AsNoTracking()
                .Where(x => x.IdCliente == idCliente && x.FechaUltimoAbono >= fecha)
                .Include(x => x.ProductoClienteAbonos.Where(x => x.RecibidoAdmin == false && x.FechaAbono >= fecha)).ToListAsync();

            return abonos;
        }

        public async Task<bool> EliminarAbonosCliente(IEnumerable<AbonoCliente> abonosEliminar, IEnumerable<ProductosCliente> productosCliente)
        {
            bool respuesta = false;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    // Elimina los abonos del día actual
                    _context.AbonosCliente.RemoveRange(abonosEliminar);
                    int resultado = await _context.SaveChangesAsync();

                    List<int> idsProductos = productosCliente.Select(x => x.Id).ToList();
                    // Actualiza la fecha del ultimo abono en el producto
                    foreach (var item in productosCliente)
                    {
                        _context.Entry(item).Property(x => x.FechaUltimoAbono).IsModified = true;
                    }

                    await _context.SaveChangesAsync();

                    transaccion.Commit();

                    return resultado > default(int);
                }
                catch (Exception e)
                {
                    transaccion.Rollback();
                    return respuesta;
                }
            }
        }

        public async Task<IEnumerable<AbonoCliente>> ProductosAbonosPagoAnterior(IEnumerable<int> idsProductosHistorial, DateTime diaActual)
        {
            var abonos = await _context.AbonosCliente.AsNoTracking()
                .Where(x => idsProductosHistorial.Contains(x.IdProductoCliente) && x.FechaAbono < diaActual)
                .OrderByDescending(x => x.FechaAbono).ToListAsync();

            return abonos;
        }
    }
}