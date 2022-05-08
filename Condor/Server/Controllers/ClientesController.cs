using AutoMapper;
using Condor.Core.Entities;
using Condor.Core.IService;
using Condor.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Condor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        public ClientesController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpGet("clientesPendientes")]
        public async Task<ActionResult<Tuple<IEnumerable<ClienteDto>, IEnumerable<ClienteDto>>>> GetClientesPendientes()
        {
            var clientes = await _clienteService.ConsultarListaClientesPendientesCobrar(1);
            IEnumerable<ClienteDto> clientesPendientes = _mapper.Map<IEnumerable<ClienteDto>>(clientes.Item1);
            IEnumerable<ClienteDto> clientesCobrados = _mapper.Map<IEnumerable<ClienteDto>>(clientes.Item2);
            clientesPendientes.ToList().ForEach(x => x.PagadoHoy = false);
            clientesCobrados.ToList().ForEach(x => x.PagadoHoy = true);

            return new Tuple<IEnumerable<ClienteDto>, IEnumerable<ClienteDto>>(clientesPendientes, clientesCobrados);
        }

        [HttpGet("clienteProductos/{idCliente}")]
        public async Task<ActionResult<ResumenClienteProductosDTO>> GetClienteProductos(int idCliente)
        {
            var resultado = await _clienteService.ConsultarClienteProductos(idCliente);
            var cliente = resultado.Item1;
            var productosCliente = new ResumenClienteProductosDTO()
            {
                ClienteDto = _mapper.Map<ClienteDto>(cliente),
                ProductosClienteDto = _mapper.Map<List<ProductosClienteDTO>>(cliente?.ProductosClientes),
                DeudaTotal = resultado.Item2,
                EstadoCobrado = resultado.Item3,
                ValorCobradoHoy = resultado.Item4
            };

            return productosCliente;
        }

        [HttpPost]
        public async Task<ActionResult<int>> RegistrarCliente(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            // Cambiar por la cartera asignada para el usuario actual
            int resultado = await _clienteService.RegistrarCliente(cliente, 1);

            return resultado;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> ConsultarCliente(int id)
        {
            var cliente = await _clienteService.ConsultarCliente(id);
            if(cliente == null)
            {
                return NotFound();
            }
            var clienteDto = _mapper.Map<ClienteDto>(cliente);

            return clienteDto;
        }


        [HttpGet("clientePorNombre/{nombreCliente}")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> ConsultarClientePorNombre(string nombreCliente)
        {
            var clientes = await _clienteService.ConsultarClientePorNombre(nombreCliente);
            var clientesDto = _mapper.Map<IEnumerable<ClienteDto>>(clientes);
            var listaClientes = new List<ClienteDto>();
            listaClientes.AddRange(clientesDto);

            return listaClientes;
        }
    }
}
