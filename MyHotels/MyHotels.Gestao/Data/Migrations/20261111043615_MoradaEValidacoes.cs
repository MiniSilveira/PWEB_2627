using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHotels.Gestao.Migrations
{
    /// <inheritdoc />
    public partial class MoradaEValidacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MoradaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Moradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rua = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Localidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moradas", x => x.Id);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Produtos_PrecoBase",
                table: "Produtos",
                sql: "[PrecoBase] > 0");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Nome",
                table: "Categorias",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MoradaId",
                table: "AspNetUsers",
                column: "MoradaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Moradas_MoradaId",
                table: "AspNetUsers",
                column: "MoradaId",
                principalTable: "Moradas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Moradas_MoradaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Moradas");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Produtos_PrecoBase",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_Nome",
                table: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MoradaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MoradaId",
                table: "AspNetUsers");
        }
    }
}
