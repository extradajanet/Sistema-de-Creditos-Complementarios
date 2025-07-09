using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaCreditosComplementarios.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class init4AuthUpdateCantidadAlumnos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstadoActividad",
                table: "AlumnosActividades",
                newName: "EstadoAlumnoActividad");

            migrationBuilder.AddColumn<int>(
                name: "CapacidadMaxima",
                table: "Actividades",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadMaxima",
                table: "Actividades");

            migrationBuilder.RenameColumn(
                name: "EstadoAlumnoActividad",
                table: "AlumnosActividades",
                newName: "EstadoActividad");
        }
    }
}
