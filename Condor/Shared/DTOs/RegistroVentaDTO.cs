using System.ComponentModel.DataAnnotations;

namespace Condor.Shared.DTOs
{
    public class RegistroVentaDTO
    {
        public decimal CuotaInicial { get; set; }
        [Required(ErrorMessage = "Se debe establecer el valor de la cuota")]
        public decimal ValorCuota { get; set;}

        [Required(ErrorMessage = "Deben haberse agregado al menos 1 producto")]
        public IEnumerable<MercaderiaDto>? MercaderiaVenta { get; set;}
    }
}
