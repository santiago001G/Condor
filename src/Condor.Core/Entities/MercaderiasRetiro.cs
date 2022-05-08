using System;
using System.Collections.Generic;

namespace Condor.Core.Entities
{
    public partial class MercaderiasRetiro
    {
        public int Id { get; set; }
        public int IdMercaderia { get; set; }
        public int? IdProductoCliente { get; set; }
        public bool EsVendible { get; set; }
        public string? Observaciones { get; set; }

        public virtual Mercaderia IdMercaderiaNavigation { get; set; } = null!;
    }
}
