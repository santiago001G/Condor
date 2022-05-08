using System;
using System.Collections.Generic;

namespace Condor.Core.Entities
{
    public partial class CategoriaVendedor
    {
        public int Id { get; set; }
        public string? IdUsuario { get; set; }
        public int PorcentajeComision { get; set; }
        public string? Categoria { get; set; }
    }
}
