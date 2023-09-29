using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoFinalLabIV.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.EnsureSchema(
                name: "Seguridad");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "UsuariosToken",
                newSchema: "Seguridad");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "Usuarios",
                newSchema: "Seguridad");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "UsuariosRoles",
                newSchema: "Seguridad");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "UsuariosLogin",
                newSchema: "Seguridad");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "UsuariosClaims",
                newSchema: "Seguridad");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Roles",
                newSchema: "Seguridad");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "RolesClaims",
                newSchema: "Seguridad");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "Seguridad",
                table: "UsuariosRoles",
                newName: "IX_UsuariosRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "Seguridad",
                table: "UsuariosLogin",
                newName: "IX_UsuariosLogin_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "Seguridad",
                table: "UsuariosClaims",
                newName: "IX_UsuariosClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "Seguridad",
                table: "RolesClaims",
                newName: "IX_RolesClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosToken",
                schema: "Seguridad",
                table: "UsuariosToken",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                schema: "Seguridad",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosRoles",
                schema: "Seguridad",
                table: "UsuariosRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosLogin",
                schema: "Seguridad",
                table: "UsuariosLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosClaims",
                schema: "Seguridad",
                table: "UsuariosClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "Seguridad",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolesClaims",
                schema: "Seguridad",
                table: "RolesClaims",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clubes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resumen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagenEscudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubes_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JugadoresClubes",
                columns: table => new
                {
                    JugadorId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JugadoresClubes", x => new { x.JugadorId, x.ClubId });
                    table.ForeignKey(
                        name: "FK_JugadoresClubes_Clubes_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JugadoresClubes_Jugadores_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Jugadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clubes_CategoriaId",
                table: "Clubes",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_JugadoresClubes_ClubId",
                table: "JugadoresClubes",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesClaims_Roles_RoleId",
                schema: "Seguridad",
                table: "RolesClaims",
                column: "RoleId",
                principalSchema: "Seguridad",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosClaims_Usuarios_UserId",
                schema: "Seguridad",
                table: "UsuariosClaims",
                column: "UserId",
                principalSchema: "Seguridad",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosLogin_Usuarios_UserId",
                schema: "Seguridad",
                table: "UsuariosLogin",
                column: "UserId",
                principalSchema: "Seguridad",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRoles_Roles_RoleId",
                schema: "Seguridad",
                table: "UsuariosRoles",
                column: "RoleId",
                principalSchema: "Seguridad",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRoles_Usuarios_UserId",
                schema: "Seguridad",
                table: "UsuariosRoles",
                column: "UserId",
                principalSchema: "Seguridad",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosToken_Usuarios_UserId",
                schema: "Seguridad",
                table: "UsuariosToken",
                column: "UserId",
                principalSchema: "Seguridad",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesClaims_Roles_RoleId",
                schema: "Seguridad",
                table: "RolesClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosClaims_Usuarios_UserId",
                schema: "Seguridad",
                table: "UsuariosClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosLogin_Usuarios_UserId",
                schema: "Seguridad",
                table: "UsuariosLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRoles_Roles_RoleId",
                schema: "Seguridad",
                table: "UsuariosRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRoles_Usuarios_UserId",
                schema: "Seguridad",
                table: "UsuariosRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosToken_Usuarios_UserId",
                schema: "Seguridad",
                table: "UsuariosToken");

            migrationBuilder.DropTable(
                name: "JugadoresClubes");

            migrationBuilder.DropTable(
                name: "Clubes");

            migrationBuilder.DropTable(
                name: "Jugadores");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosToken",
                schema: "Seguridad",
                table: "UsuariosToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosRoles",
                schema: "Seguridad",
                table: "UsuariosRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosLogin",
                schema: "Seguridad",
                table: "UsuariosLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosClaims",
                schema: "Seguridad",
                table: "UsuariosClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                schema: "Seguridad",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolesClaims",
                schema: "Seguridad",
                table: "RolesClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "Seguridad",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "UsuariosToken",
                schema: "Seguridad",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "UsuariosRoles",
                schema: "Seguridad",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "UsuariosLogin",
                schema: "Seguridad",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "UsuariosClaims",
                schema: "Seguridad",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                schema: "Seguridad",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "RolesClaims",
                schema: "Seguridad",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Seguridad",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosLogin_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RolesClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
