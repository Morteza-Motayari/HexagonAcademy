using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Essay_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EssayId",
                table: "KeyWords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EssayCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EssayCategoryParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EssayCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EssayCategories_EssayCategories_EssayCategoryParentId",
                        column: x => x.EssayCategoryParentId,
                        principalTable: "EssayCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Essays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EssayCagtegoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Essays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Essays_EssayCategories_EssayCagtegoryId",
                        column: x => x.EssayCagtegoryId,
                        principalTable: "EssayCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyWords_EssayId",
                table: "KeyWords",
                column: "EssayId");

            migrationBuilder.CreateIndex(
                name: "IX_EssayCategories_EssayCategoryParentId",
                table: "EssayCategories",
                column: "EssayCategoryParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Essays_EssayCagtegoryId",
                table: "Essays",
                column: "EssayCagtegoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyWords_Essays_EssayId",
                table: "KeyWords",
                column: "EssayId",
                principalTable: "Essays",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyWords_Essays_EssayId",
                table: "KeyWords");

            migrationBuilder.DropTable(
                name: "Essays");

            migrationBuilder.DropTable(
                name: "EssayCategories");

            migrationBuilder.DropIndex(
                name: "IX_KeyWords_EssayId",
                table: "KeyWords");

            migrationBuilder.DropColumn(
                name: "EssayId",
                table: "KeyWords");
        }
    }
}
