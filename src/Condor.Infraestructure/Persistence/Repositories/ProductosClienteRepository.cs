using Condor.Core.Constantes;
using Condor.Core.Entities;
using Condor.Core.Interfaces;
using Condor.Infraestructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Condor.Infraestructure.Persistence.Repositories
{
    public class ProductosClienteRepository : IProductosClienteRepository
    {
        private readonly CondorContext _context;

        public ProductosClienteRepository(CondorContext context)
        {
            _context = context;
        }

        public async Task<ProductosCliente> ConsultarProductoCliente(int idProductoCliente)
        {
            return await _context.ProductosCliente.AsNoTracking()
                .Where(x => x.Id == idProductoCliente && x.EstadoPago != ConstantesGlobales.PAGADO).FirstAsync();
        }

        public async Task<IEnumerable<ProductosCliente>> ObtenerProductosPendientesCliente(int idCliente)
        {
            return await _context.ProductosCliente.AsNoTracking()
                .Where(x => x.IdCliente == idCliente && x.EstadoPago != ConstantesGlobales.PAGADO).ToListAsync();
        }
    }
}
