using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condor.Shared.DTOs
{
    public class ResumenClienteProductosDTO
    {

        public ClienteDto? ClienteDto { get; set; }
        public List<ProductosClienteDTO>? ProductosClienteDto { get; set; }

        public decimal? DeudaTotal { get; set; }

        public decimal? ValorCobradoHoy { get; set; }

        public bool EstadoCobrado { get; set; }

    }
}
