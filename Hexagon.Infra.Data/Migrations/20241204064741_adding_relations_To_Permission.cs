using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexagon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class adding_relations_To_Permission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Permissions_PermissionId1",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PermissionId1",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PermissionId1",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId",
                principalTable: "Permissions",
                principalColumn: "PermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Permissions_ParentId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId1",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionId1",
                table: "Permissions",
                column: "PermissionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Permissions_PermissionId1",
                table: "Permissions",
                column: "PermissionId1",
                principalTable: "Permissions",
                principalColumn: "PermissionId");
        }
    }
}
