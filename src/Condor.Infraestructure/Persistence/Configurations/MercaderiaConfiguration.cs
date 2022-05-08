using Condor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Condor.Infraestructure.Persistence.Configurations
{
    public class MercaderiaConfiguration : IEntityTypeConfiguration<Mercaderia>
    {
        public void Configure(EntityTypeBuilder<Mercaderia> entity)
        {
            entity.ToTable("TBL_MERCADERIAS");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.CantidadStock).HasColumnName("CANTIDAD_STOCK");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");

            entity.Property(e => e.Estado).HasColumnName("ESTADO");

            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_MODIFICACION");

            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            entity.Property(e => e.PrecioCompra)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PRECIO_COMPRA");

            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PRECIO_VENTA");

            entity.Property(e => e.IdCategoria).HasColumnName("ID_CATEGORIA");

            entity.HasOne(d => d.IdCategoriaNavigation)
                .WithMany(p => p.Mercaderia)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_MERCADERIAS_TBL_CATEGORIAS_MERCADERIA");
        }
    }
}
