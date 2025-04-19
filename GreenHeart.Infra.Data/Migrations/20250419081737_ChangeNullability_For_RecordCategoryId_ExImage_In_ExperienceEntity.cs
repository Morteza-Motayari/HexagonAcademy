using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNullability_For_RecordCategoryId_ExImage_In_ExperienceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExImage",
                table: "Experiences",
                type: "nvarchar(300)",
                nullable: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecordCategoryId",
                table: "Experiences",
                type: "int",
                nullable: false,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExImage",
                table: "Experiences",
                type: "nvarchar(300)",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "RecordCategoryId",
                table: "Experiences",
                type: "int",
                nullable: true);
        }
    }
}
