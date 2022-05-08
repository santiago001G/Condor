using Condor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Condor.Infraestructure.Persistence.Configurations
{
    public class MercaderiasRetiroConfiguration : IEntityTypeConfiguration<MercaderiasRetiro>
    {
        public void Configure(EntityTypeBuilder<MercaderiasRetiro> entity)
        {
            entity.ToTable("TBL_MERCADERIAS_RETIROS");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.EsVendible).HasColumnName("ES_VENDIBLE");

            entity.Property(e => e.IdMercaderia).HasColumnName("ID_MERCADERIA");

            entity.Property(e => e.IdProductoCliente).HasColumnName("ID_PRODUCTO_CLIENTE");

            entity.Property(e => e.Observaciones)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("OBSERVACIONES");

            entity.HasOne(d => d.IdMercaderiaNavigation)
                .WithMany(p => p.MercaderiasRetiros)
                .HasForeignKey(d => d.IdMercaderia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_MERCADERIAS_RETIROS_TBL_MERCADERIAS");
        }
    }
}
