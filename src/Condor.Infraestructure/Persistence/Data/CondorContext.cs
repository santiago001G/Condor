using System;
using System.Collections.Generic;
using System.Reflection;
using Condor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Condor.Infraestructure.Persistence.Data
{
    public partial class CondorContext : DbContext
    {
        public CondorContext()
        {
        }

        public CondorContext(DbContextOptions<CondorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cartera> Cartera { get; set; } = null!;
        public virtual DbSet<CategoriaVendedor> CategoriaVendedor { get; set; } = null!;
        public virtual DbSet<Cliente> Cliente { get; set; } = null!;
        public virtual DbSet<Mercaderia> Mercaderia { get; set; } = null!;
        public virtual DbSet<MercaderiasRetiro> MercaderiasRetiro { get; set; } = null!;
        public virtual DbSet<CategoriasMercaderia> CategoriasMercaderia { get; set; } = null!;
        public virtual DbSet<AbonoCliente> AbonosCliente { get; set; } = null!;
        public virtual DbSet<ProductosCliente> ProductosCliente { get; set; } = null!;
        public virtual DbSet<Venta> Venta { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
