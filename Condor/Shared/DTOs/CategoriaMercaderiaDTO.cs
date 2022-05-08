
namespace Condor.Shared.DTOs
{
    public  class CategoriaMercaderiaDTO
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
