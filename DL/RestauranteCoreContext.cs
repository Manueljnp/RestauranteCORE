using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ML;

namespace DL;

public partial class RestauranteCoreContext : DbContext
{
    public RestauranteCoreContext()
    {
    }

    public RestauranteCoreContext(DbContextOptions<RestauranteCoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Restaurante> Restaurantes { get; set; }
    public virtual DbSet<RestauranteGetAllSP> RestauranteGetAllSP { get; set; }
    //public virtual DbSet<RestauranteGetByIdSP> RestauranteGetByIdSP { get; set; }
    //public virtual DbSet<RestauranteAddSP> RestauranteAddSP { get; set; }

    //CADENA DE CONEXIÓN EXPUESTA, se debe eliminar después de la inyección de dependencias, pero en este caso lo dejo de ejemplo

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=RestauranteCore; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RestauranteGetAllSP>(entity =>
        {
            entity.HasNoKey();
        });

        /*modelBuilder.Entity<RestauranteGetByIdSP>(entity =>
        {
            entity.HasNoKey();
        });*/

        /*modelBuilder.Entity<RestauranteAddSP>(entity =>
        {
            entity.HasNoKey();
        });*/

        modelBuilder.Entity<Restaurante>(entity =>
        {
            entity.HasKey(e => e.IdRestaurante).HasName("PK__Restaura__29CE64FA9491B85E");

            entity.ToTable("Restaurante");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(700)
                .IsUnicode(false);
            entity.Property(e => e.Eslogan)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
