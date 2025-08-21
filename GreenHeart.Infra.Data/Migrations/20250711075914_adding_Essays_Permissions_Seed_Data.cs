using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class adding_Essays_Permissions_Seed_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentId", "PermissionName", "PermissionTitle" },
                values: new object[,]
                {
                    { 98, null, "ManageEssayCategories", "مدیریت گروه های مقالات" },
                    { 104, null, "ManageEssays", "مدیریت مقالات" },
                    { 99, 98, "AddEssayCategory", "افزودن گروه مقاله" },
                    { 100, 98, "EditEssayCategory", "ویرایش گروه مقاله" },
                    { 101, 98, "DeleteEssayCategory", "حذف گروه مقاله" },
                    { 102, 98, "DetailEssayCategory", "جزئیات گروه مقاله" },
                    { 103, 98, "DeleteEssayCategoryForever", "حذف مطلق گروه مقاله" },
                    { 105, 104, "AddEssay", "افزودن مقاله" },
                    { 106, 104, "EditEssay", "ویرایش مقاله" },
                    { 107, 104, "DeleteEssay", "حذف مقاله" },
                    { 108, 104, "DetailEssay", "جزئیات مقاله" },
                    { 109, 104, "DeleteEssayForever", "حذف مطلق مقاله" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 104);
        }
    }
}
