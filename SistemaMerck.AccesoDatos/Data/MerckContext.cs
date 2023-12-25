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

    public virtual DbSet<Localidades> Localidades { get; set; }

    public virtual DbSet<Provincias> Provincias { get; set; }

    public virtual DbSet<TipoConsulta> TipoConsulta { get; set; }

    public virtual DbSet<Pais> Paises { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localidades>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__localida__3213E83FF5E2FF6C");

            entity.ToTable("localidades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");
            entity.Property(e => e.Localidad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("localidad");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pais__3213E83F00C3A5FA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Provincias>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__provinci__3213E83F51D64F26");

            entity.ToTable("provincias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPais).HasColumnName("id_pais");
            entity.Property(e => e.Provincia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("provincia");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK_provincias_paises");
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
