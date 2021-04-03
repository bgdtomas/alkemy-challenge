using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace alkemyChallengeCSharp.Migrations
{
    public partial class TerceraVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistroAlumnosInscriptos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlumnoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MateriaId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroAlumnosInscriptos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroAlumnosInscriptos_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroAlumnosInscriptos_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAlumnosInscriptos_AlumnoId",
                table: "RegistroAlumnosInscriptos",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroAlumnosInscriptos_MateriaId",
                table: "RegistroAlumnosInscriptos",
                column: "MateriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroAlumnosInscriptos");
        }
    }
}
