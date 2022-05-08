using Condor.Core.Entities;

namespace Condor.Core.IService
{
    public interface ICobroService
    {
        Task<bool> ActualizarAbonos(AbonoCliente abono, int idCliente);
        Task<bool> EliminarAbonosCliente(int idCliente);
        Task<bool> RegistrarAbono(AbonoCliente abono, int idCliente);
        Task<bool> RegistrarAbonos(AbonoCliente abono, int idCliente);
    }
}
