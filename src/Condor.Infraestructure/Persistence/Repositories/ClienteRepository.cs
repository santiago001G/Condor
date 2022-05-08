using Condor.Core.Constantes;
using Condor.Core.Entities;
using Condor.Core.Interfaces;
using Condor.Infraestructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Condor.Infraestructure.Persistence.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private CondorContext _context;

        public ClienteRepository(CondorContext context)
        {
            _context = context;
        }

        public async Task<int> RegistrarCliente(Cliente clienteRegistrar)
        {
            _context.Cliente.Add(clienteRegistrar);
            var resultado = await _context.SaveChangesAsync();

            return clienteRegistrar.Id;
        }

        public async Task<Cliente> ConsultarCliente(int idCliente)
        {
            return await _context.Cliente.AsNoTracking().FirstOrDefaultAsync(x => x.Id == idCliente);
        }

        public async Task<IEnumerable<Cliente>> ObtenerListaClientes()
        {
            return await _context.Cliente.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObtenerListaClientesPorCartera(int idCartera)
        {
            return await _context.Cliente.AsNoTracking().Where(x => x.IdCartera == idCartera).ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObtenerListaClienteProductos(int idCartera)
        {
            return await _context.Cliente
                .AsNoTracking()
                .Where(x => x.IdCartera == idCartera)
                .Include(x => x.ProductosClientes.Where(x => x.EstadoPago != ConstantesGlobales.PAGADO && x.EstadoPago != ConstantesGlobales.RETIRADO))
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ConsultarListaClientesDiariosPendienteCobrar(int idCartera, DateTime fechaBusqueda)
        {
            return await _context.Cliente
                .Where(x => x.IdCartera == idCartera && x.PeridicidadCobro == ConstantesGlobales.PERIODO_DIARIO)
                .Include(x => x.ProductosClientes
                .Where(x => x.FechaCompra < fechaBusqueda &&
                           x.EstadoPago != ConstantesGlobales.RETIRADO &&
                           x.EstadoPago != ConstantesGlobales.PAGADO))
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ConsultarListaClientesSemanalesPendienteCobrar(int idCartera, DateTime fechaBusqueda)
        {
            return await _context.Cliente
                .Where(x => x.IdCartera == idCartera && x.PeridicidadCobro == ConstantesGlobales.PERIODO_SEMANAL)
                .Include(x => x.ProductosClientes
                .Where(x => x.FechaUltimoAbono < fechaBusqueda && x.Retirado != ConstantesGlobales.RETIRADO && x.EstadoPago != ConstantesGlobales.PAGADO))
                .ToListAsync();
        }

        public async Task<Cliente> ConsultarResumenCliente(int idCliente)
        {
            return await _context.Cliente
                .AsNoTracking()
                .Where(x => x.Id == idCliente)
                .Include(x => x.ProductosClientes.Where(x => x.EstadoPago != ConstantesGlobales.PAGADO && x.Retirado != ConstantesGlobales.RETIRADO)).ThenInclude(x => x.ProductoClienteAbonos)
                .Include(x => x.ProductosClientes).ThenInclude(x => x.IdMercaderiaNavigation)
                .FirstAsync();
        }

        public async Task<IEnumerable<Cliente>> ConsultarClientePorNombre(string nombreCliente)
        {
            return await _context.Cliente
                .AsNoTracking()
                .Where(x => x.Nombre.ToLower().Contains(nombreCliente))
                .ToListAsync();
        }
    }
}
