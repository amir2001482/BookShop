using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Migrations.BookShopIdentity
{
    public partial class init04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AppRoles_ApplicationRolesId",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_ApplicationRole_RoleId",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AppRoles_RoleId",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_ApplicationRole_RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AppRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "ApplicationRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRoles",
                table: "AppRoles");

            migrationBuilder.RenameTable(
                name: "AppRoles",
                newName: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationRoles_Description",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_AspNetRoles_ApplicationRolesId",
                table: "AppUserRoles",
                column: "ApplicationRolesId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_AspNetRoles_RoleId",
                table: "AppUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_AspNetRoles_RoleId1",
                table: "AppUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AspNetRoles_ApplicationRolesId",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AspNetRoles_RoleId",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_AspNetRoles_RoleId1",
                table: "AppUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ApplicationRoles_Description",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AppRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRoles",
                table: "AppRoles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ApplicationRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRole", x => x.Id);
                });

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
                name: "FK_AppUserRoles_AppRoles_RoleId",
                table: "AppUserRoles",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_ApplicationRole_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1",
                principalTable: "ApplicationRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AppRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
