using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenHeart.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_GenderAndClassStatus_To_SportClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassUsers_SportClasses_SportClassId",
                table: "ClassUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassUsers_Users_UserId",
                table: "ClassUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Users_UserId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_GymGalleries_Gyms_GymId",
                table: "GymGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_GymStaffs_Gyms_GymId",
                table: "GymStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_GymStaffs_Staffs_StaffId",
                table: "GymStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_GymUsers_Gyms_GymId",
                table: "GymUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_GymUsers_Users_UserId",
                table: "GymUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_SportClasses_Gyms_GymId",
                table: "SportClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SportClasses_Sports_SportId",
                table: "SportClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SportClasses_Staffs_TrainerId",
                table: "SportClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sports_Certificates_CertificateId",
                table: "Sports");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Users_UserId",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_Certificates_CertificateId",
                table: "UserCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_Users_UserId",
                table: "UserCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_Roles_RoleId",
                table: "userRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_Users_UserId",
                table: "userRoles");

            migrationBuilder.AlterColumn<int>(
                name: "TrainerId",
                table: "SportClasses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SportId",
                table: "SportClasses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "SportClasses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassStatus",
                table: "SportClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "SportClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassUsers_SportClasses_SportClassId",
                table: "ClassUsers",
                column: "SportClassId",
                principalTable: "SportClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassUsers_Users_UserId",
                table: "ClassUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Users_UserId",
                table: "Experiences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GymGalleries_Gyms_GymId",
                table: "GymGalleries",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GymStaffs_Gyms_GymId",
                table: "GymStaffs",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GymStaffs_Staffs_StaffId",
                table: "GymStaffs",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GymUsers_Gyms_GymId",
                table: "GymUsers",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GymUsers_Users_UserId",
                table: "GymUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SportClasses_Gyms_GymId",
                table: "SportClasses",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SportClasses_Sports_SportId",
                table: "SportClasses",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SportClasses_Staffs_TrainerId",
                table: "SportClasses",
                column: "TrainerId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sports_Certificates_CertificateId",
                table: "Sports",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Users_UserId",
                table: "Staffs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_Certificates_CertificateId",
                table: "UserCertificates",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_Users_UserId",
                table: "UserCertificates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_Roles_RoleId",
                table: "userRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_Users_UserId",
                table: "userRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassUsers_SportClasses_SportClassId",
                table: "ClassUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassUsers_Users_UserId",
                table: "ClassUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Users_UserId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_GymGalleries_Gyms_GymId",
                table: "GymGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_GymStaffs_Gyms_GymId",
                table: "GymStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_GymStaffs_Staffs_StaffId",
                table: "GymStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_GymUsers_Gyms_GymId",
                table: "GymUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_GymUsers_Users_UserId",
                table: "GymUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_SportClasses_Gyms_GymId",
                table: "SportClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SportClasses_Sports_SportId",
                table: "SportClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SportClasses_Staffs_TrainerId",
                table: "SportClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sports_Certificates_CertificateId",
                table: "Sports");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Users_UserId",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_Certificates_CertificateId",
                table: "UserCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_Users_UserId",
                table: "UserCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_Roles_RoleId",
                table: "userRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_Users_UserId",
                table: "userRoles");

            migrationBuilder.DropColumn(
                name: "ClassStatus",
                table: "SportClasses");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "SportClasses");

            migrationBuilder.AlterColumn<int>(
                name: "TrainerId",
                table: "SportClasses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SportId",
                table: "SportClasses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "SportClasses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassUsers_SportClasses_SportClassId",
                table: "ClassUsers",
                column: "SportClassId",
                principalTable: "SportClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassUsers_Users_UserId",
                table: "ClassUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Users_UserId",
                table: "Experiences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GymGalleries_Gyms_GymId",
                table: "GymGalleries",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GymStaffs_Gyms_GymId",
                table: "GymStaffs",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GymStaffs_Staffs_StaffId",
                table: "GymStaffs",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GymUsers_Gyms_GymId",
                table: "GymUsers",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GymUsers_Users_UserId",
                table: "GymUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportClasses_Gyms_GymId",
                table: "SportClasses",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SportClasses_Sports_SportId",
                table: "SportClasses",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SportClasses_Staffs_TrainerId",
                table: "SportClasses",
                column: "TrainerId",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sports_Certificates_CertificateId",
                table: "Sports",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Users_UserId",
                table: "Staffs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_Certificates_CertificateId",
                table: "UserCertificates",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_Users_UserId",
                table: "UserCertificates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_Roles_RoleId",
                table: "userRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_Users_UserId",
                table: "userRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
