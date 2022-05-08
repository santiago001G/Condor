using Condor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Condor.Infraestructure.Persistence.Configurations
{
    public class CarteraConfiguration : IEntityTypeConfiguration<Cartera>
    {
        public void Configure(EntityTypeBuilder<Cartera> entity)
        {
            entity.ToTable("TBL_CARTERAS");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CREACION");

            entity.Property(e => e.IdCobrador)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ID_COBRADOR");

            entity.Property(e => e.IdSupervisor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ID_SUPERVISOR");

            entity.Property(e => e.IdVendedor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ID_VENDEDOR");

            entity.Property(e => e.NombreCartera)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_CARTERA");
        }
    }
}
