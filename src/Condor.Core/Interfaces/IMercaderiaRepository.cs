using Condor.Core.Entities;

namespace Condor.Core.Interfaces
{
    public interface IMercaderiaRepository
    {
        Task<IEnumerable<Mercaderia>> ConsultarMercaderia();
        Task<IEnumerable<Mercaderia>> ConsultarMercaderiaPorIds(IEnumerable<int> idsMercaderias);
    }
}
