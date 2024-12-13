using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexagon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Slug_To_Trainer_Fullname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Users");
        }
    }
}
