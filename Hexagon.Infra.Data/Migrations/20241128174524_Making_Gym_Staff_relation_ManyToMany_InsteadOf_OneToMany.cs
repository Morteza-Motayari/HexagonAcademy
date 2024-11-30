using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexagon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Making_Gym_Staff_relation_ManyToMany_InsteadOf_OneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Gyms_GymId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_GymId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Staffs");

            migrationBuilder.CreateTable(
                name: "GymStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymStaffs_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GymStaffs_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymStaffs_GymId",
                table: "GymStaffs",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_GymStaffs_StaffId",
                table: "GymStaffs",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymStaffs");

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_GymId",
                table: "Staffs",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Gyms_GymId",
                table: "Staffs",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
