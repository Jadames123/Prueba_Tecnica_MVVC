using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVC_Prueba_Tecnica.Models;

public partial class BddgiiContext : DbContext
{
    public BddgiiContext()
    {
    }

    public BddgiiContext(DbContextOptions<BddgiiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TablaComprobantesFiscale> TablaComprobantesFiscales { get; set; }
    public virtual DbSet<TablaContribuyente> TablaContribuyentes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("server=JUISA\\SQLEXPRESS; database=BDDGII; TrustServerCertificate=True; User ID=sa; Password=Adames123; Persist Security Info=True;MultipleActiveResultSets=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TablaComprobantesFiscale>(entity =>
        {
            entity.HasKey(e => e.IdComprobante).HasName("PK__TComprobante");

            entity.ToTable("Tabla_Comprobantes_Fiscales");

            entity.Property(e => e.IdComprobante).HasColumnName("Id_Comprobante");
            entity.Property(e => e.Itbis18).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NCF)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NCF");
            entity.Property(e => e.RncCedula)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("rncCedula");

            entity.HasOne(d => d.RncCedulaNavigation).WithMany(p => p.TablaComprobantesFiscales)
                .HasForeignKey(d => d.RncCedula)
                .HasConstraintName("FK_comprobantes_contribuyentes");
        });

        modelBuilder.Entity<TablaContribuyente>(entity =>
        {
            entity.HasKey(e => e.RncCedula).HasName("PK__TContribuyente");

            entity.ToTable("Tabla_Contribuyentes");

            entity.Property(e => e.RncCedula)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("rncCedula");
            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdContribuyente)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id_Contribuyente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        /*modelBuilder.Entity<TablaContribuyente>()
             .HasMany(c => c.TablaComprobantesFiscales)
             .WithOne(o => o.RncCedulaNavigation)
             .HasForeignKey(o => o.RncCedula);*/


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
