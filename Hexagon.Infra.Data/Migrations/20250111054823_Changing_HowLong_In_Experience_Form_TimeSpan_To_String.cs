using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexagon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Changing_HowLong_In_Experience_Form_TimeSpan_To_String : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HowLong",
                table: "Experiences",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HowLong",
                table: "Experiences",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);
        }
    }
}
