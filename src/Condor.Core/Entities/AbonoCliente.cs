using System;
using System.Collections.Generic;

namespace Condor.Core.Entities
{
    public partial class AbonoCliente
    {
        public int Id { get; set; }
        public int IdProductoCliente { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaAbono { get; set; }
        public string? IdCobrador { get; set; }
        public bool EsInicial { get; set; }
        public bool RecibidoAdmin { get; set; }
        public DateTime FechaRecibidoAdmin { get; set; }

        public virtual ProductosCliente IdProductoClienteNavigation { get; set; } = null!;
    }
}
