using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Migrations.BookShopIdentity
{
    public partial class UpdateAspRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AppRoless_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AppRoless_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRoless",
                table: "AppRoless");

            migrationBuilder.RenameTable(
                name: "AppRoless",
                newName: "AppRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRoles",
                table: "AppRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AppRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AppRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AppRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AppRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRoles",
                table: "AppRoles");

            migrationBuilder.RenameTable(
                name: "AppRoles",
                newName: "AppRoless");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRoless",
                table: "AppRoless",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AppRoless_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AppRoless",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AppRoless_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AppRoless",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
