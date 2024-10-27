using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Shared
{
    public class SuccessMessages
    {
        #region Roles
        public static string RoleAddedSuccessfully = "نقش با موفقیت اضافه شده است.";
        public static string RoleUpdatedSuccessfully = "نقش با موفقیت ویرایش شده است.";
        public static string RoleDeletedSuccessfully = "نقش با موفقیت حذف شده است.";
        #endregion
    }
    public class ErrorMessages
    {
        #region Roles
        public static string RoleNotFound = "نقش مورد نظر پیدا نشده است.";
        public static string RoleTitleDuplicated = "نقشی با این عنوان پیدا شده است.";

        #endregion
    }
}
