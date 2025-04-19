using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class adding_RecordCategory_Entity_And_ExperienceImage_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Experiences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExImage",
                table: "Experiences",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RecordCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordCategories_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CategoryId",
                table: "Experiences",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordCategories_StaffId",
                table: "RecordCategories",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_RecordCategories_CategoryId",
                table: "Experiences",
                column: "CategoryId",
                principalTable: "RecordCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_RecordCategories_CategoryId",
                table: "Experiences");

            migrationBuilder.DropTable(
                name: "RecordCategories");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_CategoryId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "ExImage",
                table: "Experiences");
        }
    }
}
