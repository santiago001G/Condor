using Condor.Core.Entities;
using Condor.Core.Interfaces;
using Condor.Core.IService;

namespace Condor.Core.Service
{
    public class CobroService : ICobroService
    {
        private readonly IAbonosClienteRepository _abonosClienteRepository;
        private readonly IProductosClienteRepository _productosClienteRepository;

        public CobroService(IAbonosClienteRepository abonosClienteRepository, IProductosClienteRepository productosClienteRepository)
        {
            _abonosClienteRepository = abonosClienteRepository;
            _productosClienteRepository = productosClienteRepository;
        }

        public async Task<bool> RegistrarAbono(AbonoCliente abono, int idCliente)
        {
            var producto = await _productosClienteRepository.ConsultarProductoCliente(abono.IdProductoCliente);
            AbonoCliente abonoInsertar = new AbonoCliente()
            {
                EsInicial = false,
                FechaAbono = DateTime.Now,
                IdProductoCliente = producto.Id,
                Valor = abono.Valor,
                IdCobrador = "CAMBIAR EL ID",
                FechaRecibidoAdmin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue
            };

            bool resultado = await _abonosClienteRepository.RegistrarAbono(abonoInsertar);

            return resultado;
        }

        public async Task<bool> RegistrarAbonos(AbonoCliente abono, int idCliente)
        {
            var productosPendientes = await _productosClienteRepository.ObtenerProductosPendientesCliente(idCliente);
            var coutaTotal = productosPendientes.Sum(x => x.ValorCuota);
            bool resultado = false;
            decimal totalCuotasRegistradas = default;

            if (productosPendientes.Count() > default(int))
            {
                List<AbonoCliente> abonosInsertar = new List<AbonoCliente>();
                DateTime fechaActual = DateTime.Now;

                foreach (var producto in productosPendientes)
                {
                    var porcentajeCuota = (producto.ValorCuota / coutaTotal) * 100;
                    var valorCuotaRegistrar = (abono.Valor * porcentajeCuota) / 100;

                    abonosInsertar.Add(new AbonoCliente
                    {
                        EsInicial = false,
                        FechaAbono = fechaActual,
                        IdProductoCliente = producto.Id,
                        Valor = valorCuotaRegistrar,
                        IdCobrador = "CAMBIAR EL ID",
                        FechaRecibidoAdmin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue

                    });

                    totalCuotasRegistradas += valorCuotaRegistrar;
                }

                resultado = await _abonosClienteRepository.RegistrarAbonos(abonosInsertar);
            }

            return resultado;
        }

        public async Task<bool> ActualizarAbonos(AbonoCliente abono, int idCliente)
        {
            DateTime diaActual = DateTime.Now.Date;
            // Solo se debe permitir actualizar los abonos del día de hoy
            var productosAbonados = await _abonosClienteRepository.ConsultarAbonosClientePorFecha(idCliente, diaActual);
            var coutaTotal = productosAbonados.Sum(x => x.ValorCuota);
            bool resultado = false;
            decimal totalCuotasRegistradas = default;

            if (productosAbonados.Count() > default(int))
            {
                List<AbonoCliente> abonosActualizar = new List<AbonoCliente>();
                DateTime fechaActual = DateTime.Now;

                foreach (var producto in productosAbonados)
                {
                    var porcentajeCuota = (producto.ValorCuota / coutaTotal) * 100;
                    var valorCuotaRegistrar = (abono.Valor * porcentajeCuota) / 100;
                    int idAbonoRealizado = producto.ProductoClienteAbonos.First().Id;

                    abonosActualizar.Add(new AbonoCliente
                    {
                        Id = idAbonoRealizado,
                        Valor = valorCuotaRegistrar
                    });

                    totalCuotasRegistradas += valorCuotaRegistrar;
                }

                resultado = await _abonosClienteRepository.ActualizarAbonos(abonosActualizar);
            }

            return resultado;
        }

        public async Task<bool> EliminarAbonosCliente(int idCliente)
        {
            try
            {
                DateTime diaActual = DateTime.Now.Date;
                var productosCliente = await _abonosClienteRepository.ConsultarAbonosClientePorFecha(idCliente, diaActual);
                var abonosValidos = productosCliente.SelectMany(x => x.ProductoClienteAbonos).ToList();
                var abonosAnteriores = await _abonosClienteRepository.ProductosAbonosPagoAnterior(productosCliente.Select(x => x.Id), diaActual);

                bool respuesta = false;

                if (abonosValidos != null)
                {
                    foreach (var producto in productosCliente)
                    {
                        AbonoCliente ultimoAbonoProducto = new AbonoCliente();
                        if (abonosAnteriores.Count() > default(int))
                        {
                            ultimoAbonoProducto = abonosAnteriores.Where(x => x.IdProductoCliente == producto.Id).OrderByDescending(x => x.FechaAbono).First();
                        }

                        if (ultimoAbonoProducto.Id > default(int))
                        {
                            producto.FechaUltimoAbono = ultimoAbonoProducto.FechaAbono;
                        }
                        else
                        {
                            producto.FechaUltimoAbono = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                        }
                    }

                    respuesta = await _abonosClienteRepository.EliminarAbonosCliente(abonosValidos, productosCliente);
                }

                return respuesta;
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}
