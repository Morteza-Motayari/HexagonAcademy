using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_StaffId_To_experience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Experiences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_StaffId",
                table: "Experiences",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Staffs_StaffId",
                table: "Experiences",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Staffs_StaffId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_StaffId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Experiences");
        }
    }
}
