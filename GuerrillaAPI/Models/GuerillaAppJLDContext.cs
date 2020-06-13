﻿using System;
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
        public virtual DbSet<UnidadesDeBatalla> UnidadesDeBatalla { get; set; }
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

                entity.Property(e => e.Correo)
                    .HasColumnName("correo")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TipoGuerrilla)
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

            modelBuilder.Entity<RecursoDeGuerrilla>(entity =>
            {
                entity.HasKey(e => e.IdGuerrilla);

                entity.ToTable("recurso_de_guerrilla");

                entity.Property(e => e.IdGuerrilla)
                    .HasColumnName("id_guerrilla")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdRecurso).HasColumnName("id_recurso");

                entity.HasOne(d => d.IdGuerrillaNavigation)
                    .WithOne(p => p.RecursoDeGuerrilla)
                    .HasForeignKey<RecursoDeGuerrilla>(d => d.IdGuerrilla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_guerrilla");

                entity.HasOne(d => d.IdGuerrilla1)
                    .WithOne(p => p.RecursoDeGuerrilla)
                    .HasForeignKey<RecursoDeGuerrilla>(d => d.IdGuerrilla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_recurso");
            });

            modelBuilder.Entity<UnidadesDeBatalla>(entity =>
            {
                entity.HasKey(e => e.IdUnidad);

                entity.ToTable("unidades_de_batalla");

                entity.Property(e => e.IdUnidad)
                    .HasColumnName("id_unidad")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ataque)
                    .HasColumnName("ataque")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CostoDinero)
                    .HasColumnName("costo_dinero")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CostoPetroleo)
                    .HasColumnName("costo_petroleo")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CostoUnidades)
                    .HasColumnName("costo_unidades")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Defensa)
                    .HasColumnName("defensa")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.NombreUnidad)
                    .HasColumnName("nombre_unidad")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Pillaje)
                    .HasColumnName("pillaje")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<UnidadesDeGuerrilla>(entity =>
            {
                entity.HasKey(e => e.IdGuerrilla);

                entity.ToTable("unidades_de_guerrilla");

                entity.Property(e => e.IdGuerrilla)
                    .HasColumnName("id_guerrilla")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdUnidad).HasColumnName("id_unidad");

                entity.HasOne(d => d.IdGuerrillaNavigation)
                    .WithOne(p => p.UnidadesDeGuerrilla)
                    .HasForeignKey<UnidadesDeGuerrilla>(d => d.IdGuerrilla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("guerrillaunidad");

                entity.HasOne(d => d.IdUnidadNavigation)
                    .WithMany(p => p.UnidadesDeGuerrilla)
                    .HasForeignKey(d => d.IdUnidad)
                    .HasConstraintName("unidadguerrilla");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}