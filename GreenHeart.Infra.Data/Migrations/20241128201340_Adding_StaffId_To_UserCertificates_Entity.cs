using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_StaffId_To_UserCertificates_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "UserCertificates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCertificates_StaffId",
                table: "UserCertificates",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_Staffs_StaffId",
                table: "UserCertificates",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_Staffs_StaffId",
                table: "UserCertificates");

            migrationBuilder.DropIndex(
                name: "IX_UserCertificates_StaffId",
                table: "UserCertificates");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "UserCertificates");
        }
    }
}
