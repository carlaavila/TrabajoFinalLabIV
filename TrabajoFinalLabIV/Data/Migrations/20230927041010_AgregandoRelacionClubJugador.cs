using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoFinalLabIV.Data.Migrations
{
    public partial class AgregandoRelacionClubJugador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubes_Categorias_CategoriaId1",
                table: "Clubes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clubes_Jugadores_JugadorId",
                table: "Clubes");

            migrationBuilder.DropIndex(
                name: "IX_Clubes_CategoriaId1",
                table: "Clubes");

            migrationBuilder.DropIndex(
                name: "IX_Clubes_JugadorId",
                table: "Clubes");

            migrationBuilder.DropColumn(
                name: "CategoriaId1",
                table: "Clubes");

            migrationBuilder.DropColumn(
                name: "JugadorId",
                table: "Clubes");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Categorias");

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Jugadores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jugadores_ClubId",
                table: "Jugadores",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubes_CategoriaId",
                table: "Clubes",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubes_Categorias_CategoriaId",
                table: "Clubes",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jugadores_Clubes_ClubId",
                table: "Jugadores",
                column: "ClubId",
                principalTable: "Clubes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubes_Categorias_CategoriaId",
                table: "Clubes");

            migrationBuilder.DropForeignKey(
                name: "FK_Jugadores_Clubes_ClubId",
                table: "Jugadores");

            migrationBuilder.DropIndex(
                name: "IX_Jugadores_ClubId",
                table: "Jugadores");

            migrationBuilder.DropIndex(
                name: "IX_Clubes_CategoriaId",
                table: "Clubes");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Jugadores");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId1",
                table: "Clubes",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Clubes_CategoriaId1",
                table: "Clubes",
                column: "CategoriaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Clubes_JugadorId",
                table: "Clubes",
                column: "JugadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubes_Categorias_CategoriaId1",
                table: "Clubes",
                column: "CategoriaId1",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clubes_Jugadores_JugadorId",
                table: "Clubes",
                column: "JugadorId",
                principalTable: "Jugadores",
                principalColumn: "Id");
        }
    }
}
