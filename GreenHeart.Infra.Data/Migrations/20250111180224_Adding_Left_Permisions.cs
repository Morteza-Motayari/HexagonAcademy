using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Left_Permisions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentId", "PermissionName", "PermissionTitle" },
                values: new object[,]
                {
                    { 59, null, "ManageBanners", "مدیریت بنرها" },
                    { 62, null, "ManageContactUses", "مدیریت ارتباط با ما" },
                    { 67, null, "ManageKeyWords", "مدیریت کلمات کلیدی" },
                    { 73, null, "ManageTickets", "مدیریت تیکت ها" },
                    { 81, null, "ManageWallets", "مدیریت کیف پول ها" },
                    { 84, null, "ManageOrders", "مدیریت فاکتور ها" },
                    { 60, 59, "AddBanner", "افزودن بنر" },
                    { 61, 59, "DeleteBanner", "افزودن بنر" },
                    { 63, 62, "AnswerContactUs", "ویرایش ارتباط با ما" },
                    { 64, 62, "DeleteContactUs", "حذف ارتباط با ما" },
                    { 65, 62, "DetailContactUs", "جزئیات ارتباط با ما" },
                    { 66, 62, "DeleteContactUsForever", "حذف مطلق ارتباط با ما" },
                    { 68, 67, "AddKeyWord", "افزودن کلمه کلیدی" },
                    { 69, 67, "EditKeyWord", "ویرایش کلمه کلیدی" },
                    { 70, 67, "DeleteKeyWord", "حذف کلمه کلیدی" },
                    { 71, 67, "DetailKeyWord", "جزئیات کلمه کلیدی" },
                    { 72, 67, "DeleteKeyWordForever", "حذف مطلق کلمه کلیدی" },
                    { 74, 73, "AnswerTicket", "پاسخ تیکت" },
                    { 75, 73, "ChangeTicketStatus", "تغییر وضعیت تیکت" },
                    { 76, 73, "DeleteTicket", "حذف تیکت" },
                    { 77, 73, "DetailTicket", "جزئیات تیکت" },
                    { 78, 73, "DeleteTicketForever", "حذف مطلق تیکت" },
                    { 79, 73, "DeleteTicketMessage", "حذف پیام تیکت" },
                    { 80, 73, "DeleteTicketMessageForever", "حذف مطلق پیام تیکت" },
                    { 82, 81, "ChargeUserWallet", "شارژ کیف پول کاربر" },
                    { 83, 81, "DetailWallet", "جزئیات کیف پول" },
                    { 85, 84, "DetailOrder", "جزئیات فاکتور" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 84);
        }
    }
}
