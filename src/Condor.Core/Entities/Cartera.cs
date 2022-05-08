using System;
using System.Collections.Generic;

namespace Condor.Core.Entities
{
    public partial class Cartera
    {
        public Cartera()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int Id { get; set; }
        public string NombreCartera { get; set; } = null!;
        public string? IdCobrador { get; set; }
        public string? IdVendedor { get; set; }
        public string? IdSupervisor { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
