namespace Condor.Shared.DTOs
{
    public  class MercaderiaDto
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }
        public decimal? PrecioVenta { get; set; }
        public decimal? PrecioCompra { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int CantidadStock { get; set; }
    }
}
