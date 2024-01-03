using Microsoft.EntityFrameworkCore;
using SistemaMerck.Modelos;
using SistemaMerck.Modelos.ViewModels;

namespace SistemaMerck.AccesoDatos.Data;

public partial class MerckContext : DbContext
{
    public MerckContext()
    {
    }

    public MerckContext(DbContextOptions<MerckContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DatosFormulario> DatosFormularios { get; set; }
    public virtual DbSet<Administradores> Administradores { get; set; }

    public virtual DbSet<Localidades> Localidades { get; set; }

    public virtual DbSet<Paises> Paises { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<TipoConsulta> TipoConsulta { get; set; }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administradores>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Administ__3214EC07E4BE0A9E");

            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(255);
        });

        modelBuilder.Entity<DatosFormulario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DatosFor__3214EC07E0FA5F57");

            entity.ToTable("DatosFormulario");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FechaHora).HasColumnType("datetime");
        });

        modelBuilder.Entity<Localidades>(entity =>
        {
            entity.HasKey(e => e.LocalidadId).HasName("PK__Localida__6E289F4254A2C31F");

            entity.Property(e => e.LocalidadId)
                .ValueGeneratedNever()
                .HasColumnName("LocalidadID");
            entity.Property(e => e.NombreLocalidad).HasMaxLength(50);
            entity.Property(e => e.ProvinciaId).HasColumnName("ProvinciaID");

            entity.HasOne(d => d.Provincia).WithMany(p => p.Localidades)
                .HasForeignKey(d => d.ProvinciaId)
                .HasConstraintName("FK__Localidad__Provi__5DCAEF64");
        });

        modelBuilder.Entity<Paises>(entity =>
        {
            entity.HasKey(e => e.PaisId).HasName("PK__Paises__B501E1A54317EEB0");

            entity.Property(e => e.PaisId)
                .ValueGeneratedNever()
                .HasColumnName("PaisID");
            entity.Property(e => e.NombrePais).HasMaxLength(50);
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.ProvinciaId).HasName("PK__Provinci__F7CBC757FD8F8D54");

            entity.Property(e => e.ProvinciaId)
                .ValueGeneratedNever()
                .HasColumnName("ProvinciaID");
            entity.Property(e => e.NombreProvincia).HasMaxLength(50);
            entity.Property(e => e.PaisId).HasColumnName("PaisID");

            entity.HasOne(d => d.Pais).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.PaisId)
                .HasConstraintName("FK__Provincia__PaisI__5AEE82B9");
        });

        modelBuilder.Entity<TipoConsulta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipo_con__3213E83FA523060E");

            entity.ToTable("Tipo_consulta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Consulta)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("consulta");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
