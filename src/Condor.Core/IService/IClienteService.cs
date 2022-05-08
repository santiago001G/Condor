using Condor.Core.Entities;

namespace Condor.Core.IService
{
    public interface IClienteService
    {
        Task<Cliente> ConsultarCliente(int idCliente);
        Task<IEnumerable<Cliente>> ConsultarClientePorNombre(string nombreCliente);
        Task<Tuple<Cliente?, decimal?, bool, decimal?>> ConsultarClienteProductos(int idCliente);
        Task<Tuple<IEnumerable<Cliente>, IEnumerable<Cliente>>> ConsultarListaClientesPendientesCobrar(int idCartera);
        Task<int> RegistrarCliente(Cliente cliente, int carteraAsignada);
    }
}
