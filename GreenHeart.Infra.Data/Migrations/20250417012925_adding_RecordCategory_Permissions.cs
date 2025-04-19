using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class adding_RecordCategory_Permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentId", "PermissionName", "PermissionTitle" },
                values: new object[,]
                {
                    { 92, null, "ManageRecordCategories", "مدیریت مجموعه های سوابق" },
                    { 93, 92, "AddRecordCategory", "افزودن مجموعه سوابق" },
                    { 94, 92, "EditRecordCategory", "ویرایش مجموعه سوابق" },
                    { 95, 92, "DeleteRecordCategory", "حذف مجموعه سوابق" },
                    { 96, 92, "DetailRecordCategory", "جزئیات مجموعه سوابق" },
                    { 97, 92, "DeleteRecordCategoryForever", "حذف مطلق مجموعه سوابق" }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 92);

        }
    }
}
