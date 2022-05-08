using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Condor.Core.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            ProductosClientes = new HashSet<ProductosCliente>();
        }

        public int Id { get; set; }
        public int IdCartera { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string Direccion { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public string? Calificacion { get; set; }
        public string PeridicidadCobro { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }
        public string? EstadoPagos { get; set; }
        public string? Latitud { get; set; }
        public string? Logitud { get; set; }

        [NotMapped]
        public decimal ValorCuota { get; set; }

        public virtual Cartera IdCarteraNavigation { get; set; } = null!;
        public virtual ICollection<ProductosCliente> ProductosClientes { get; set; }
    }
}
