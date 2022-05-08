using Condor.Core.Entities;
using Condor.Core.Interfaces;
using Condor.Infraestructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace Condor.Infraestructure.Persistence.Repositories
{
    public class MercaderiaRepository: IMercaderiaRepository
    {
        private readonly CondorContext _context;

        public MercaderiaRepository(CondorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mercaderia>> ConsultarMercaderiaPorIds(IEnumerable<int> idsMercaderias)
        {
            return await (from a in _context.Mercaderia
                          join b in idsMercaderias
                          on a.Id equals b
                          select new Mercaderia
                          {
                              Id = a.Id,
                              Nombre = a.Nombre
                          } ).ToListAsync();
        }

        public async Task<IEnumerable<Mercaderia>> ConsultarMercaderia()
        {
            return await _context.Mercaderia.ToListAsync();
        }

    }
}
