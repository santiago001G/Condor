using Condor.Core.Entities;

namespace Condor.Core.Interfaces
{
    public interface IAbonosClienteRepository
    {
        Task<IEnumerable<ProductosCliente>> ConsultarAbonosClientePorFecha(int idCliente, DateTime fecha);
        Task<bool> ActualizarAbonos(IEnumerable<AbonoCliente> abonos);
        Task<bool> RegistrarAbono(AbonoCliente abono);
        Task<bool> RegistrarAbonos(IEnumerable<AbonoCliente> abono);
        Task<bool> EliminarAbonosCliente(IEnumerable<AbonoCliente> abonosEliminar, IEnumerable<ProductosCliente> productosCliente);
        Task<IEnumerable<AbonoCliente>> ProductosAbonosPagoAnterior(IEnumerable<int> idProductosHistorial, DateTime diaActual);
    }
}
