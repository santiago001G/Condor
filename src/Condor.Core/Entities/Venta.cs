using System;
using System.Collections.Generic;

namespace Condor.Core.Entities
{
    public partial class Venta
    {
        public int Id { get; set; }
        public int IdProductoCliente { get; set; }
        public string? IdUsuarioVendedor { get; set; }
        public bool EstadoLiquidacion { get; set; }
        public decimal? ValorLiquidado { get; set; }
        public DateTime? FechaLiquidacion { get; set; }
        public string? VentaRetirada { get; set; }

        public virtual ProductosCliente IdProductoClienteNavigation { get; set; } = null!;
    }
}
