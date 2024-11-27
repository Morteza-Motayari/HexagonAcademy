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

        #region Account
        public static string RigesterDoneSuccessfully = "ثبت نام شما با موفقیت انجام شد.";
        public static string SignInDoneSuccessfully = " عزیز خوش آمدید.";
        public static string SignInDoneUnkownuUserSuccessfully = " کاربر عزیز خوش آمدید.";
        public static string ForgotPasswordSentSuccessfully = " کد تایید فراموشی رمز عبور برای شماره شما ارسال شد.";
        public static string ResetPasswordDoneSuccessfully = " رمز عبور شما با موفقیت تغییر یافت.";
        public static string PersonalUserInfoUpdatedSuccessfully = " اطلاعات شما با موفقیت بروز شد.";

        #endregion

        #region User
        public static string UserAddedSuccessfully = "کاربر جدید با موفقیت اضافه شده است.";
        public static string UserUpdatedSuccessfully = "کاربر مدنظر با موفقیت ویرایش شده است.";
        public static string UserStatusChangedSuccessfully = "کاربر جدید با موفقیت اضافه شده است.";
        public static string UserDeletedSuccessfully = "کاربر مدنظر با موفقیت حذف شده است.";
        public static string UserPasswordChangedSuccessfully = "رمز کاربر مدنظر با موفقیت تغییر کرده است.";
        #endregion

        #region Gym
        public static string GymAddedSuccessfully = "باشگاه جدید با موفقیت اضافه شده است.";
        public static string GymUpdatedSuccessfully = "باشگاه مدنظر  با موفقیت ویرایش شده است.";
        public static string GymDeletedSuccessfully = "باشگاه مدنظر  با موفقیت حذف شده است.";
        #endregion

        #region GymGallery
        public static string GymGalleryAddedSuccessfully = "تصویر جدید برای باشگاه با موفقیت اضافه شده است.";
        public static string GymGalleryDeletedSuccessfully = "تصویر باشگاه مدنظر  با موفقیت حذف شده است.";
        #endregion
    }
    public class ErrorMessages
    {
        #region Public Message
        public static string InsufficintInputs = "لطفا فیلدهای ضروری را پر بکنید.";
        public static string ErrorOccured = "خطایی در هنگام اجرای عملیات رخ داده است لطفا دوباره سعی بکنید.";
        #endregion

        #region Roles
        public static string RoleNotFound = "نقش مورد نظر پیدا نشده است.";
        public static string RoleTitleDuplicated = "نقشی با این عنوان پیدا شده است.";
        #endregion

        #region Account
        public static string PhoneNumberExisted = "شماره موبایلی که وارد کرده اید قبلا با آن ثبت نام شده است.";
        public static string UserNotExisted = "همچین کاربری پیدا نشده است.";
        public static string UserNotActive = "حساب کاربری مدنظر هنوز فعال نشده است.";
        public static string UserIsBanned = "حساب کاربری مدنظر مسدود شده است.";
        public static string ErrorOccuredInSms = "هنگام ارسال مد تایید به حساب شما حطایی رخ داده است لطفا بعدا سعی بکنید.";
        public static string WrongCodeEntered = "کد تایید وارد شده درست نمی باشد.";
        public static string ErrorInUpdateUserOccured = " در هنگام بروز اطلاعات شما مشکلی رخ داده است.";
        public static string InvalidDateTimeInput = " تاریخ تولد وارد شده معتبر نمی باشد.";
        #endregion

        #region User
        public static string UserNotFound = "کاربر مدنظر پیدا نشده است.";
        public static string UserPhoneNumberDuplicated = "شماره مدنظر قبلا ثبت شده است.";
        public static string UserAlreadyDeleted = "کاربر مدنظر قبلا حذف شده است.";
        #endregion

        #region Gym
        public static string GymNotFound = "باشگاه مدنظر پیدا نشده است.";
        public static string GymConstatntPhoneNumberDuplicated = "شماره ثابت مدنظر قبلا ثبت شده است.";
        public static string GymAlreadyDeleted = "باشگاه مدنظر قبلا حذف شده است.";
        #endregion

        #region GymGallery
        public static string GymGalleryNull = "تصویری برای باشگاه انتخاب نشده است.";
        public static string MaxImagesForGym = "حداکثر تصاویر موجود برای یک باشگاه 8 تا می تواند باشد.";
        #endregion
    }

    public class WarningMessages
    {
        #region User
        public static string UserCantbeEdited = "این کاربر حذف شده است و امکان ویرایش آن وجود ندارد.";
        #endregion

        #region Gym
        public static string GymCantbeEdited = "این باشگاه حذف شده است و امکان ویرایش آن وجود ندارد.";
        #endregion

        #region GymGallery
        public static string GymGalleryIdZero = "عکسی انتخاب نشده است.";

        #endregion
    }
}
