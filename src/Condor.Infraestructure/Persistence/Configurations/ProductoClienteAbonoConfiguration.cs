using Condor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Condor.Infraestructure.Persistence.Configurations
{
    public class ProductoClienteAbonoConfiguration : IEntityTypeConfiguration<AbonoCliente>
    {
        public void Configure(EntityTypeBuilder<AbonoCliente> entity)
        {
            entity.ToTable("TBL_PRODUCTO_CLIENTE_ABONOS");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.EsInicial).HasColumnName("ES_INICIAL");

            entity.Property(e => e.FechaAbono)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_ABONO");

            entity.Property(e => e.IdCobrador)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ID_COBRADOR");

            entity.Property(e => e.IdProductoCliente).HasColumnName("ID_PRODUCTO_CLIENTE");

            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("VALOR");

            entity.Property(e => e.RecibidoAdmin).HasColumnName("RECIBIDO_ADMIN");

            entity.Property(e => e.FechaRecibidoAdmin)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_RECIBIDO_ADMIN");

            entity.HasOne(d => d.IdProductoClienteNavigation)
                .WithMany(p => p.ProductoClienteAbonos)
                .HasForeignKey(d => d.IdProductoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_PRODUCTO_CLIENTE_ABONOS_TBL_PRODUCTOS_CLIENTE");
        }
    }
}
