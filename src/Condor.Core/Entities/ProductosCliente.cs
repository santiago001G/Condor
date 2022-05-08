using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Condor.Core.Entities
{
    public partial class ProductosCliente
    {
        public ProductosCliente()
        {
            ProductoClienteAbonos = new HashSet<AbonoCliente>();
            Venta = new HashSet<Venta>();
        }

        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdMercaderia { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal? ValorCompra { get; set; }
        public string? Anotaciones { get; set; }
        public string EstadoPago { get; set; } = null!;
        public string? Retirado { get; set; }
        public decimal ValorCuota { get; set; }
        public DateTime? FechaUltimoAbono { get; set; }


        [NotMapped]
        public string NombreProducto { get; set; } 

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Mercaderia IdMercaderiaNavigation { get; set; } = null!;
        public virtual ICollection<AbonoCliente> ProductoClienteAbonos { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
