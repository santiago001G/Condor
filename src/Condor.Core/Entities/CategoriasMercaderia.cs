namespace Condor.Core.Entities
{
    public partial class CategoriasMercaderia
    {
        public CategoriasMercaderia()
        {
            Mercaderia = new HashSet<Mercaderia>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }
        public DateTime FechaModificacion { get; set; }

        public virtual ICollection<Mercaderia> Mercaderia { get; set; }
    }
}
