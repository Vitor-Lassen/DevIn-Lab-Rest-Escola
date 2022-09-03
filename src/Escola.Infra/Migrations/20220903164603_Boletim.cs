using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Escola.Infra.Migrations
{
    public partial class Boletim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SOBRENOME",
                table: "ALUNO",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                table: "ALUNO",
                type: "VARCHAR(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "ALUNO",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "Boletim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Periodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faltas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boletim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boletim_ALUNO_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "ALUNO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotasMateria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    BoletimId = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasMateria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotasMateria_Boletim_BoletimId",
                        column: x => x.BoletimId,
                        principalTable: "Boletim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotasMateria_Materia_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALUNO_Matricula",
                table: "ALUNO",
                column: "Matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boletim_AlunoId",
                table: "Boletim",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasMateria_BoletimId",
                table: "NotasMateria",
                column: "BoletimId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasMateria_MateriaId",
                table: "NotasMateria",
                column: "MateriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotasMateria");

            migrationBuilder.DropTable(
                name: "Boletim");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropIndex(
                name: "IX_ALUNO_Matricula",
                table: "ALUNO");

            migrationBuilder.AlterColumn<string>(
                name: "SOBRENOME",
                table: "ALUNO",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NOME",
                table: "ALUNO",
                type: "VARCHAR(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)",
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "ALUNO",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
