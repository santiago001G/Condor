using Condor.Core.Entities;

namespace Condor.Core.Interfaces
{
    public interface IProductosClienteRepository
    {
        Task<ProductosCliente> ConsultarProductoCliente(int idProductoCliente);
        Task<IEnumerable<ProductosCliente>> ObtenerProductosPendientesCliente(int idCliente);
    }
}
