using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexagon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Rename_wrong_PropertyName_Order_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFunally",
                table: "Orders",
                newName: "IsFainally");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFainally",
                table: "Orders",
                newName: "IsFunally");
        }
    }
}
