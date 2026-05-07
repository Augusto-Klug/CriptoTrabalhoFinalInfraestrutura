using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriptoTrabalhoFinalInfraestrutura.Migrations
{
    public partial class InitialCreateLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    horario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    criptos = table.Column<string>(type: "varchar(max)", nullable: false),
                    mensagem = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");
        }
    }
}
