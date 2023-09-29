using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoFinalLabIV.Data.Migrations
{
    public partial class Club_Jugadores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubes_Categorias_CategoriaId",
                table: "Clubes");

            migrationBuilder.DropIndex(
                name: "IX_Clubes_CategoriaId",
                table: "Clubes");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Clubes");

            migrationBuilder.AddColumn<int>(
                name: "JugadorId",
                table: "Clubes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clubes_JugadorId",
                table: "Clubes",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_ClubId",
                table: "Categorias",
                column: "ClubId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Clubes_ClubId",
                table: "Categorias",
                column: "ClubId",
                principalTable: "Clubes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clubes_Jugadores_JugadorId",
                table: "Clubes",
                column: "JugadorId",
                principalTable: "Jugadores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Clubes_ClubId",
                table: "Categorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Clubes_Jugadores_JugadorId",
                table: "Clubes");

            migrationBuilder.DropIndex(
                name: "IX_Clubes_JugadorId",
                table: "Clubes");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_ClubId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "JugadorId",
                table: "Clubes");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Categorias");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Clubes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clubes_CategoriaId",
                table: "Clubes",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubes_Categorias_CategoriaId",
                table: "Clubes",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
