using Condor.Core.Entities;

namespace Condor.Core.Interfaces
{
    public  interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObtenerListaClientes();
        Task<IEnumerable<Cliente>> ObtenerListaClientesPorCartera(int idCartera);
        Task<Cliente> ConsultarCliente(int idCliente);
        Task<IEnumerable<Cliente>> ObtenerListaClienteProductos(int idCartera);
        Task<Cliente> ConsultarResumenCliente(int idCliente);
        Task<IEnumerable<Cliente>> ConsultarListaClientesDiariosPendienteCobrar(int idCartera, DateTime fechaBusqueda);
        Task<IEnumerable<Cliente>> ConsultarListaClientesSemanalesPendienteCobrar(int idCartera, DateTime fechaBusqueda);
        Task<int> RegistrarCliente(Cliente clienteRegistrar);
        Task<IEnumerable<Cliente>> ConsultarClientePorNombre(string nombreCliente);
    }
}
