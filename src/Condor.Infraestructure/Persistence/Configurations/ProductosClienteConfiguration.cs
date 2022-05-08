using Condor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Condor.Infraestructure.Persistence.Configurations
{
    public class ProductosClienteConfiguration : IEntityTypeConfiguration<ProductosCliente>
    {
        public void Configure(EntityTypeBuilder<ProductosCliente> entity)
        {
            entity.ToTable("TBL_PRODUCTOS_CLIENTE");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Anotaciones)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ANOTACIONES");

            entity.Property(e => e.EstadoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESTADO_PAGO");

            entity.Property(e => e.FechaCompra)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_COMPRA");

            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

            entity.Property(e => e.IdMercaderia).HasColumnName("ID_MERCADERIA");

            entity.Property(e => e.Retirado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RETIRADO");

            entity.Property(e => e.ValorCompra)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("VALOR_COMPRA");

            entity.Property(e => e.ValorCuota)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("VALOR_COUTA");

            entity.Property(e => e.FechaUltimoAbono)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_ULTIMO_ABONO");

            entity.HasOne(d => d.IdClienteNavigation)
                .WithMany(p => p.ProductosClientes)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PRODUCTOS_CLIENTE_TBL_CLIENTES");

            entity.HasOne(d => d.IdMercaderiaNavigation)
                .WithMany(p => p.ProductosClientes)
                .HasForeignKey(d => d.IdMercaderia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PRODUCTOS_CLIENTE_TBL_MERCADERIAS");
        }
    }
}
