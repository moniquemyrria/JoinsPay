using Microsoft.EntityFrameworkCore.Migrations;

namespace projectback.Migrations
{
    public partial class add_campo_tipo_conta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "padrao",
                table: "Tipo_Conta",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "padrao",
                table: "Tipo_Conta");
        }
    }
}
