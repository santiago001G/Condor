using Condor.Core.Entities;
using Condor.Core.Interfaces;
using Condor.Core.IService;

namespace Condor.Core.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IAbonosClienteRepository _abonosClienteRepository;
        public ClienteService(IClienteRepository clienteRepository, IAbonosClienteRepository abonosClienteRepository)
        {
            _clienteRepository = clienteRepository;
            _abonosClienteRepository = abonosClienteRepository;
        }

        public async Task<int> RegistrarCliente(Cliente cliente, int carteraAsignada)
        {
            int resultado = default(int);

            if (cliente != null)
            {
                cliente.IdCartera = carteraAsignada;
                cliente.FechaRegistro = DateTime.Now;
                cliente.Calificacion = "N";
                cliente.EstadoPagos = "AL DIA";
                resultado =  await _clienteRepository.RegistrarCliente(cliente);
            }

            return resultado;
        }

        public async Task<Cliente> ConsultarCliente(int idCliente)
        {
            return await _clienteRepository.ConsultarCliente(idCliente);
        }

        public async Task<Tuple<IEnumerable<Cliente>, IEnumerable<Cliente>>> ConsultarListaClientesPendientesCobrar(int idCartera)
        {
            
            DateTime diaActual = DateTime.Now.Date;
            DateTime diaSemanaAnterior = DateTime.Now.Date.AddDays(-7);
            var clientes = await _clienteRepository.ConsultarListaClientesDiariosPendienteCobrar(idCartera, diaActual);
            var clientesSemanales = await _clienteRepository.ConsultarListaClientesSemanalesPendienteCobrar(idCartera, diaSemanaAnterior);
            clientes.Concat(clientesSemanales);
            List<Cliente> clientesPendientes = new List<Cliente>();
            List<Cliente> clientesCobrados = new List<Cliente>();

            foreach (var cliente in clientes)
            {
                bool clienteCobrado = cliente.ProductosClientes.Any(x=>x.FechaUltimoAbono >= diaActual);
                cliente.ValorCuota = cliente.ProductosClientes.Sum(x=>x.ValorCuota);
                if (clienteCobrado)
                {
                    clientesCobrados.Add(cliente);                    
                }
                else
                {
                    clientesPendientes.Add(cliente);
                }
            }

            clientes.OrderBy(x => x.FechaRegistro);

            return new Tuple<IEnumerable<Cliente>, IEnumerable<Cliente>>(clientesPendientes,clientesCobrados);
        }

        public async Task<Tuple<Cliente?, decimal?, bool, decimal?>> ConsultarClienteProductos(int idCliente)
        {
            bool estadoCobrado = false;
            var cliente = await _clienteRepository.ConsultarResumenCliente(idCliente);
            decimal valorPagadoHoy = default(decimal);

            if (cliente != null)
            {
                var productosCliente = cliente.ProductosClientes;
                var abonos = productosCliente.SelectMany(x => x.ProductoClienteAbonos);

                // Verificacion del estado de cobro del cliente en el día actual
                //var fechaUltimoAbono = abonos.Select(x => x.FechaAbono).OrderByDescending(x => x).FirstOrDefault(); -- Revisar mejor opcion
                var fechaUltimoAbono = productosCliente.Select(x => x.FechaUltimoAbono).OrderByDescending(x => x).First();
                // Si el cliente tiene al menos 1 abono se verifica si fue cobrado el mismo día
                if (abonos.Count() > default(int))
                {
                    DateTime diaActual = DateTime.Now.Date;
                    var diaUltimoAbono = fechaUltimoAbono.Value.Date;
                    estadoCobrado = diaUltimoAbono == diaActual;

                    if (estadoCobrado)
                    {
                        valorPagadoHoy = abonos.Where(x => x.FechaAbono >= diaActual).Sum(x => x.Valor);
                    }
                }

                //// Operación del valor total de la deuda del cliente
                decimal? deudaInicial = cliente.ProductosClientes.Sum(x => x.ValorCompra);
                decimal totalAbonos = productosCliente.Select(x => x.ProductoClienteAbonos.Sum(z => z.Valor)).Sum();
                decimal? deudaTotal = deudaInicial - totalAbonos;

                cliente.ValorCuota = cliente.ProductosClientes.Sum(x => x.ValorCuota);
                // Asinga el nombre del producto del cliente referenciando la llave foranea
                cliente.ProductosClientes.ToList().ForEach(x => x.NombreProducto = x.IdMercaderiaNavigation.Nombre);

                return new Tuple<Cliente?, decimal?, bool, decimal?>(cliente, deudaTotal, estadoCobrado , valorPagadoHoy);
            }

            return new Tuple<Cliente?, decimal?, bool, decimal?>(cliente, default(decimal), estadoCobrado, valorPagadoHoy);
        }

        public async Task<IEnumerable<Cliente>> ConsultarClientePorNombre(string nombreCliente)
        {
            var clientes = await _clienteRepository.ConsultarClientePorNombre(nombreCliente.ToLower());

            return clientes;
        }
    }
}
