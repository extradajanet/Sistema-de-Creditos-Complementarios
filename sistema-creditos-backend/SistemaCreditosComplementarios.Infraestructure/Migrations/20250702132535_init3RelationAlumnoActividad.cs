using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaCreditosComplementarios.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class init3RelationAlumnoActividad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Alumnos");

            migrationBuilder.AddColumn<int>(
                name: "CoordinadorId",
                table: "Carreras",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Carreras",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroControl",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Semestre",
                table: "Alumnos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCreditos",
                table: "Alumnos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AlumnosActividades",
                columns: table => new
                {
                    IdAlumno = table.Column<int>(type: "integer", nullable: false),
                    IdActividad = table.Column<int>(type: "integer", nullable: false),
                    EstadoActividad = table.Column<int>(type: "integer", nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnosActividades", x => new { x.IdAlumno, x.IdActividad });
                    table.ForeignKey(
                        name: "FK_AlumnosActividades_Actividades_IdActividad",
                        column: x => x.IdActividad,
                        principalTable: "Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlumnosActividades_Alumnos_IdAlumno",
                        column: x => x.IdAlumno,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CoordinadorId", "DepartamentoId" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_CoordinadorId",
                table: "Carreras",
                column: "CoordinadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_DepartamentoId",
                table: "Carreras",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnosActividades_IdActividad",
                table: "AlumnosActividades",
                column: "IdActividad");

            migrationBuilder.AddForeignKey(
                name: "FK_Carreras_Coordinadores_CoordinadorId",
                table: "Carreras",
                column: "CoordinadorId",
                principalTable: "Coordinadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carreras_Departamentos_DepartamentoId",
                table: "Carreras",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carreras_Coordinadores_CoordinadorId",
                table: "Carreras");

            migrationBuilder.DropForeignKey(
                name: "FK_Carreras_Departamentos_DepartamentoId",
                table: "Carreras");

            migrationBuilder.DropTable(
                name: "AlumnosActividades");

            migrationBuilder.DropIndex(
                name: "IX_Carreras_CoordinadorId",
                table: "Carreras");

            migrationBuilder.DropIndex(
                name: "IX_Carreras_DepartamentoId",
                table: "Carreras");

            migrationBuilder.DropColumn(
                name: "CoordinadorId",
                table: "Carreras");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Carreras");

            migrationBuilder.DropColumn(
                name: "NumeroControl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Semestre",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "TotalCreditos",
                table: "Alumnos");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Alumnos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
