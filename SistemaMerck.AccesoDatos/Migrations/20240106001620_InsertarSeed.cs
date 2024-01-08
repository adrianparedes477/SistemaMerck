using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMerck.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class InsertarSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agrega los comandos SQL para insertar usuarios y roles
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('1', 'Admin', 'ADMIN')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('2', 'Usuario', 'USUARIO')");

            // Ajusta las columnas según la estructura real de tu tabla UsuarioAplicacion
            migrationBuilder.Sql("INSERT INTO UsuarioAplicacion (Id, UserName, Email, Nombres, Apellidos, Direccion, Ciudad, Pais, Role) " +
                                 "VALUES ('1', 'admin@example.com', 'admin@example.com', 'Admin', 'Admin', 'Dirección de Admin', 'Ciudad de Admin', 'País de Admin', 'Admin')");

            // Asegúrate de asignar los roles a los usuarios
            migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('1', '1')");
        }

    }

}
