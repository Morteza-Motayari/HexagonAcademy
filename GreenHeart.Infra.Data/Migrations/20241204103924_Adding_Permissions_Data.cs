using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Permissions_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentId", "PermissionName", "PermissionTitle" },
                values: new object[,]
                {
                    { 1, null, "ManageUsers", "مدیریت کاربران" },
                    { 7, null, "ManageRoles", "مدیریت نقش ها" },
                    { 13, null, "ManageTrainers", "مدیریت مربی ها" },
                    { 19, null, "ManageCadres", "مدیریت کادرها" },
                    { 25, null, "ManageCertificates", "مدیریت مدرک ها" },
                    { 31, null, "ManageExperiences", "مدیریت سابقه ها" },
                    { 37, null, "ManageGyms", "مدیریت باشگاه ها" },
                    { 45, null, "ManageSports", "مدیریت رشته های ورزشی" },
                    { 51, null, "ManageSportClasses", "مدیریت کلاس های ورزشی" },
                    { 2, 1, "AddUser", "افزودن کاربر" },
                    { 3, 1, "EditUser", "ویرایش کاربر" },
                    { 4, 1, "DeleteUser", "حذف کاربر" },
                    { 5, 1, "DetailUser", "جزئیات کاربر" },
                    { 6, 1, "DeleteUserForever", "حذف مطلق کاربر" },
                    { 8, 7, "AddRole", "افزودن نقش" },
                    { 9, 7, "EditRole", "ویرایش نقش" },
                    { 10, 7, "DeleteRole", "حذف نقش" },
                    { 11, 7, "DetailRole", "جزئیات نقش" },
                    { 12, 7, "DeleteRoleForever", "حذف مطلق نقش" },
                    { 14, 13, "AddTrainer", "افزودن مربی" },
                    { 15, 13, "EditTrainer", "ویرایش مربی" },
                    { 16, 13, "DeleteTrainer", "حذف مربی" },
                    { 17, 13, "DetailTrainer", "جزئیات مربی" },
                    { 18, 13, "DeleteTrainerForever", "حذف مطلق مربی" },
                    { 20, 19, "AddCadre", "افزودن کادر" },
                    { 21, 19, "EditCadre", "ویرایش کادر" },
                    { 22, 19, "DeleteCadre", "حذف کادر" },
                    { 23, 19, "DetailCadre", "جزئیات کادر" },
                    { 24, 19, "DeleteCadreForever", "حذف مطلق کادر" },
                    { 26, 25, "AddCertificate", "افزودن مدرک" },
                    { 27, 25, "EditCertificate", "ویرایش مدرک" },
                    { 28, 25, "DeleteCertificate", "حذف مدرک" },
                    { 29, 25, "DetailCertificate", "جزئیات مدرک" },
                    { 30, 25, "DeleteCertificateForever", "حذف مطلق مدرک" },
                    { 32, 31, "AddExperience", "افزودن سابقه" },
                    { 33, 31, "EditExperience", "ویرایش سابقه" },
                    { 34, 31, "DeleteExperience", "حذف سابقه" },
                    { 35, 31, "DetailExperience", "جزئیات سابقه" },
                    { 36, 31, "DeleteExperienceForever", "حذف مطلق سابقه" },
                    { 38, 37, "AddGym", "افزودن باشگاه" },
                    { 39, 37, "EditGym", "ویرایش باشگاه" },
                    { 40, 37, "DeleteGym", "حذف باشگاه" },
                    { 41, 37, "DeleteGymForever", "حذف مطلق باشگاه" },
                    { 42, 37, "GalleryGym", "گالری باشگاه" },
                    { 43, 37, "AddGalleryGym", "افزودن گالری باشگاه" },
                    { 44, 37, "DeleteGalleryGym", "حذف گالری باشگاه" },
                    { 46, 45, "AddSport", "افزودن رشته ورزشی" },
                    { 47, 45, "EditSport", "ویرایش رشته ورزشی" },
                    { 48, 45, "DeleteSport", "حذف رشته ورزشی" },
                    { 49, 45, "DetailSport", "جزئیات رشته ورزشی" },
                    { 50, 45, "DeleteSportForever", "حذف مطلق رشته ورزشی" },
                    { 52, 51, "AddSportClass", "افزودن کلاس ورزشی" },
                    { 53, 51, "EditSportClass", "ویرایش کلاس ورزشی" },
                    { 54, 51, "DeleteSportClass", "حذف کلاس ورزشی" },
                    { 55, 51, "DetailSportClass", "جزئیات کلاس ورزشی" },
                    { 56, 51, "DeleteSportClassForever", "حذف مطلق کلاس ورزشی" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 51);
        }
    }
}
