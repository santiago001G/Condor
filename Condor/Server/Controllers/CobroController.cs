using AutoMapper;
using Condor.Core.Entities;
using Condor.Core.IService;
using Condor.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Condor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobroController : ControllerBase
    {
        private readonly ICobroService _cobroService;
        private readonly IMapper _mapper;

        public CobroController(ICobroService cobroService, IMapper mapper)
        {
            _cobroService = cobroService;
            _mapper = mapper;
        }

        /*Pendiente crear un clase de respuesta de resultados*/
        [HttpPost("registrarAbono")]
        public async Task<ActionResult<bool>> RegistrarAbono(AbonoDto abonoDto)
        {
            AbonoCliente abono = _mapper.Map<AbonoCliente>(abonoDto);
            var resultado = await _cobroService.RegistrarAbono(abono, abonoDto.IdCliente);

            return resultado;
        }

        [HttpPost("registrarAbonoTodos")]
        public async Task<ActionResult<bool>> RegistrarAbonoTodos(AbonoDto abonoDto)
        {
            AbonoCliente abono = _mapper.Map<AbonoCliente>(abonoDto);
            var resultado = await _cobroService.RegistrarAbonos(abono, abonoDto.IdCliente);

            return resultado;
        }

        [HttpPost("actualizarAbonoTodos")]
        public async Task<ActionResult<bool>> actualizarAbonoTodos(AbonoDto abonoDto)
        {
            AbonoCliente abono = _mapper.Map<AbonoCliente>(abonoDto);
            var resultado = await _cobroService.ActualizarAbonos(abono, abonoDto.IdCliente);

            return resultado;
        }


        [HttpDelete("eliminarAbono/{idCliente}")]
        public async Task<ActionResult<bool>> EliminarAbono(int idCliente)
        {
            var resultado = await _cobroService.EliminarAbonosCliente(idCliente);

            if (resultado)
            {
                return NoContent();
            }

            return NotFound();
        }

    }
}
