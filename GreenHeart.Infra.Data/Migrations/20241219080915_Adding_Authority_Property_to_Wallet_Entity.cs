using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Authority_Property_to_Wallet_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authority",
                table: "Wallets",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authority",
                table: "Wallets");
        }
    }
}
