using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Migrations.BookShopIdentity
{
    public partial class init03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Register",
                table: "AppUsers",
                newName: "RegisterDate");

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "AspNetRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoleClaims",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastVisitDateTime",
                table: "AppUsers",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AppUsers",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationRolesId",
                table: "AppUserRoles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_ApplicationRolesId",
                table: "AppUserRoles",
                column: "ApplicationRolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_AppRoles_ApplicationRolesId",
                table: "AppUserRoles",
                column: "ApplicationRolesId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_ApplicationRole_RoleId",
                table: "AppUserRoles",
                column: "RoleId",
                principalTable: "ApplicationRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_ApplicationRole_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1",
                principalTable: "ApplicationRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AppRoles_ApplicationRolesId",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_ApplicationRole_RoleId",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_ApplicationRole_RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "ApplicationRole");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropIndex(
                name: "IX_AppUserRoles_ApplicationRolesId",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "ApplicationRolesId",
                table: "AppUserRoles");

            migrationBuilder.RenameColumn(
                name: "RegisterDate",
                table: "AppUsers",
                newName: "Register");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastVisitDateTime",
                table: "AppUsers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AppUsers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
