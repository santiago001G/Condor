using Condor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Condor.Infraestructure.Persistence.Configurations
{
    public class VentaConfiguration : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> entity)
        {
            entity.ToTable("TBL_VENTAS");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.EstadoLiquidacion).HasColumnName("ESTADO_LIQUIDACION");

            entity.Property(e => e.FechaLiquidacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_LIQUIDACION");

            entity.Property(e => e.IdProductoCliente).HasColumnName("ID_PRODUCTO_CLIENTE");

            entity.Property(e => e.IdUsuarioVendedor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ID_USUARIO_VENDEDOR");

            entity.Property(e => e.ValorLiquidado)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("VALOR_LIQUIDADO");

            entity.Property(e => e.VentaRetirada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VENTA_RETIRADA");

            entity.HasOne(d => d.IdProductoClienteNavigation)
                .WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdProductoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_VENTAS_TBL_PRODUCTOS_CLIENTE");
        }
    }
}
