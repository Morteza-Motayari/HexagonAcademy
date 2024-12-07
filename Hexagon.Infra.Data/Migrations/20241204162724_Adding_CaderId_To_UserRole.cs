using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexagon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_CaderId_To_UserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaderId",
                table: "userRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userRoles_CaderId",
                table: "userRoles",
                column: "CaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_Staffs_CaderId",
                table: "userRoles",
                column: "CaderId",
                principalTable: "Staffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_Staffs_CaderId",
                table: "userRoles");

            migrationBuilder.DropIndex(
                name: "IX_userRoles_CaderId",
                table: "userRoles");

            migrationBuilder.DropColumn(
                name: "CaderId",
                table: "userRoles");
        }
    }
}
