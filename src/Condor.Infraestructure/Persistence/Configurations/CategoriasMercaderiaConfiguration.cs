using Condor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Condor.Infraestructure.Persistence.Configurations
{
    public class CategoriasMercaderiaConfiguration : IEntityTypeConfiguration<CategoriasMercaderia>
    {
        public void Configure(EntityTypeBuilder<CategoriasMercaderia> entity)
        {
            entity.ToTable("TBL_CATEGORIAS_MERCADERIA");

            entity.Property(e => e.Id).HasColumnName("ID");

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
        }
    }
}
