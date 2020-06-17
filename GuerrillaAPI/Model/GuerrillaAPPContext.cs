using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GuerrillaAPI.Model
{
    public partial class GuerrillaAPPContext : DbContext
    {
        public GuerrillaAPPContext()
        {
        }

        public GuerrillaAPPContext(DbContextOptions<GuerrillaAPPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Guerrilla> Guerrilla { get; set; }
        public virtual DbSet<GuerrillaResources> GuerrillaResources { get; set; }
        public virtual DbSet<GuerrillaUnits> GuerrillaUnits { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guerrilla>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("guerrilla");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(150);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.Faction)
                    .IsRequired()
                    .HasColumnName("faction")
                    .HasMaxLength(50);

                entity.Property(e => e.Rank).HasColumnName("rank");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            });

            modelBuilder.Entity<GuerrillaResources>(entity =>
            {
                entity.HasKey(e => new {e.Guerrilla, e.Resource });
                //entity.HasNoKey();

                entity.ToTable("guerrilla_resources");

                entity.Property(e => e.Guerrilla)
                    .IsRequired()
                    .HasColumnName("guerrilla")
                    .HasMaxLength(150);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Resource)
                    .IsRequired()
                    .HasColumnName("resource")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<GuerrillaUnits>(entity =>
            {
                entity.HasKey(e => new { e.Guerrilla, e.Unit });
                //entity.HasNoKey();

                entity.ToTable("guerrilla_units");

                entity.Property(e => e.Guerrilla)
                    .IsRequired()
                    .HasColumnName("guerrilla")
                    .HasMaxLength(150);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasColumnName("unit")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("unit");

                entity.Property(e => e.Assault).HasColumnName("assault");

                entity.Property(e => e.Bunker).HasColumnName("bunker");

                entity.Property(e => e.Defense).HasColumnName("defense");

                entity.Property(e => e.Engineer).HasColumnName("engineer");

                entity.Property(e => e.Loot).HasColumnName("loot");

                entity.Property(e => e.Money).HasColumnName("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150);

                entity.Property(e => e.Offense).HasColumnName("offense");

                entity.Property(e => e.Oil).HasColumnName("oil");

                entity.Property(e => e.People).HasColumnName("people");

                entity.Property(e => e.Tank).HasColumnName("tank");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
