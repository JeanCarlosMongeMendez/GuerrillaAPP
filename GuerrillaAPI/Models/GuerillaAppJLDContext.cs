using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GuerrillaAPI.Models
{
    public partial class GuerillaAppJLDContext : DbContext
    {
        public GuerillaAppJLDContext()
        {
        }

        public GuerillaAppJLDContext(DbContextOptions<GuerillaAppJLDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Guerrilla> Guerrilla { get; set; }
        public virtual DbSet<Recurso> Recurso { get; set; }
        public virtual DbSet<RecursoDeGuerrilla> RecursoDeGuerrilla { get; set; }
        public virtual DbSet<UnidadDeBatalla> UnidadesDeBatalla { get; set; }
        public virtual DbSet<UnidadesDeGuerrilla> UnidadesDeGuerrilla { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guerrilla>(entity =>
            {
                entity.HasKey(e => e.IdGuerrilla);

                entity.ToTable("guerrilla");

                entity.Property(e => e.IdGuerrilla).HasColumnName("id_guerrilla");

                entity.Property(e => e.rank).HasColumnName("ranking");

                entity.Property(e => e.email)
                    .HasColumnName("correo")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.guerrillaName)
                    .HasColumnName("nombre")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.faction)
                    .HasColumnName("tipo_guerrilla")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Recurso>(entity =>
            {
                entity.HasKey(e => e.IdRecurso);

                entity.ToTable("recurso");

                entity.Property(e => e.IdRecurso)
                    .HasColumnName("id_recurso")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UnidadDeBatalla>(entity =>
            {
                entity.HasKey(e => e.idUnidad);

                entity.ToTable("unidades_de_batalla");

                entity.Property(e => e.idUnidad)
                    .HasColumnName("id_unidad")
                    .ValueGeneratedNever();

                entity.Property(e => e.ataque)
                    .HasColumnName("ataque")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.costoDinero)
                    .HasColumnName("costo_dinero")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.costoPetroleo)
                    .HasColumnName("costo_petroleo")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.costoUnidades)
                    .HasColumnName("costo_unidades")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.defensa)
                    .HasColumnName("defensa")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.nombreUnidad)
                    .HasColumnName("nombre_unidad")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.pillaje)
                    .HasColumnName("pillaje")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<UnidadesDeGuerrilla>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<RecursoDeGuerrilla>(entity =>
            {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
