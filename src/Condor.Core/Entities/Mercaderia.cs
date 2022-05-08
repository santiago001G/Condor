using System;
using System.Collections.Generic;

namespace Condor.Core.Entities
{
    public partial class Mercaderia
    {
        public Mercaderia()
        {
            MercaderiasRetiros = new HashSet<MercaderiasRetiro>();
            ProductosClientes = new HashSet<ProductosCliente>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }
        public decimal? PrecioVenta { get; set; }
        public decimal? PrecioCompra { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int CantidadStock { get; set; }
        public int IdCategoria { get; set; }

        public virtual ICollection<MercaderiasRetiro> MercaderiasRetiros { get; set; }
        public virtual ICollection<ProductosCliente> ProductosClientes { get; set; }

        public virtual CategoriasMercaderia IdCategoriaNavigation { get; set; } = null!;
    }
}
