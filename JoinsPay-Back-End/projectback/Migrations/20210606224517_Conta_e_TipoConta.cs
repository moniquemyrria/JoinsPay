using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projectback.Migrations
{
    public partial class Conta_e_TipoConta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipo_Conta",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sigla = table.Column<string>(maxLength: 6, nullable: false),
                    descricao = table.Column<string>(maxLength: 30, nullable: false),
                    deletado = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Conta", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Conta",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idTipoConta = table.Column<long>(nullable: false),
                    codigo = table.Column<string>(maxLength: 10, nullable: false),
                    nome = table.Column<string>(maxLength: 50, nullable: false),
                    agencia = table.Column<string>(maxLength: 10, nullable: false),
                    numeroConta = table.Column<string>(maxLength: 10, nullable: false),
                    dataCadastro = table.Column<DateTime>(maxLength: 1, nullable: false),
                    deletado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conta", x => x.id);
                    table.ForeignKey(
                        name: "FK_Conta_Tipo_Conta_idTipoConta",
                        column: x => x.idTipoConta,
                        principalTable: "Tipo_Conta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conta_idTipoConta",
                table: "Conta",
                column: "idTipoConta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conta");

            migrationBuilder.DropTable(
                name: "Tipo_Conta");
        }
    }
}
