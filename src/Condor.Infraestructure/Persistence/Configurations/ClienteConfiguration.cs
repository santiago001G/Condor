using Condor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Condor.Infraestructure.Persistence.Configurations
{
    public  class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> entity)
        {
            entity.ToTable("TBL_CLIENTES");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Calificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CALIFICACION");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");

            entity.Property(e => e.Direccion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");

            entity.Property(e => e.EstadoPagos)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ESTADO_PAGOS");

            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_REGISTRO");

            entity.Property(e => e.IdCartera).HasColumnName("ID_CARTERA");

            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            entity.Property(e => e.Latitud)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LATITUD");

            entity.Property(e => e.Logitud)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LONGITUD");

            entity.Property(e => e.PeridicidadCobro)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PERIDICIDAD_COBRO");

            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.Property(e => e.UrlImagen)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("URL_IMAGEN");

            entity.HasOne(d => d.IdCarteraNavigation)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdCartera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_CLIENTES_TBL_CARTERAS");
        }
    }
}
