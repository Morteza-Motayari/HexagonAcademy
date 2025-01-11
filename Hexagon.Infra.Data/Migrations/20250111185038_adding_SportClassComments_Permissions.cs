using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hexagon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class adding_SportClassComments_Permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentId", "PermissionName", "PermissionTitle" },
                values: new object[,]
                {
                    { 86, null, "ManageClassComments", "مدیریت نظرات کلاس ها" },
                    { 87, 86, "GetComments", "مشاهده نظر" },
                    { 88, 86, "ChangeCommentStatus", "تغییر وضعیت نظر" },
                    { 89, 86, "DeleteComment", "حذف نظر" },
                    { 90, 86, "DetailComment", "جزئیات نظر" },
                    { 91, 86, "DeleteCommentForever", "حذف مطلق نظر" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 86);
        }
    }
}
