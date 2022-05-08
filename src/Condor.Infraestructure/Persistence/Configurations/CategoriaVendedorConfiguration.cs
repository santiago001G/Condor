using Condor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Condor.Infraestructure.Persistence.Configurations
{
    public class CategoriaVendedorConfiguration : IEntityTypeConfiguration<CategoriaVendedor>
    {
        public void Configure(EntityTypeBuilder<CategoriaVendedor> entity)
        {
            entity.ToTable("TBL_CATEGORIA_VENDEDORES");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Categoria)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CATEGORIA_VENDEDOR");

            entity.Property(e => e.IdUsuario)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ID_USUARIO");

            entity.Property(e => e.PorcentajeComision).HasColumnName("PORCENTAJE_COMISION");
        }
    }
}
