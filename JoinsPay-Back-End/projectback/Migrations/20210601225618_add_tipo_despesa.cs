using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projectback.Migrations
{
    public partial class add_tipo_despesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipo_Despesa",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sigla = table.Column<string>(maxLength: 6, nullable: false),
                    descricao = table.Column<string>(maxLength: 30, nullable: false),
                    color = table.Column<string>(maxLength: 10, nullable: true),
                    dataCadastro = table.Column<DateTime>(nullable: false),
                    deletado = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Despesa", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipo_Despesa");
        }
    }
}
