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

        #region Certificate
        public static string CertificateAddedSuccessfully = "مدرک جدید با موفقیت اضافه شده است.";
        public static string CertificateUpdatedSuccessfully = "مدرک مدنظر  با موفقیت ویرایش شده است.";
        public static string CertificateDeletedSuccessfully = "مدرک مدنظر  با موفقیت حذف شده است.";
        #endregion

        #region Sport
        public static string SportAddedSuccessfully = "رشته ورزشی جدید با موفقیت اضافه شده است.";
        public static string SportUpdatedSuccessfully = "رشته ورزشی مدنظر  با موفقیت ویرایش شده است.";
        public static string SportDeletedSuccessfully = "رشته ورزشی مدنظر  با موفقیت حذف شده است.";
        #endregion

        #region Sport Class
        public static string SportClassAddedSuccessfully = "کلاس ورزشی جدید با موفقیت اضافه شده است.";
        public static string SportClassUpdatedSuccessfully = "کلاس ورزشی مدنظر  با موفقیت ویرایش شده است.";
        public static string SportClassDeletedSuccessfully = "کلاس ورزشی مدنظر  با موفقیت حذف شده است.";
        #endregion

        #region Staffs

        #region Trainer
        public static string TrainerAddedSuccessfully = "مربی رشته ورزشی جدید با موفقیت اضافه شده است.";
        public static string TrainerUpdatedSuccessfully = "مربی رشته ورزشی مدنظر  با موفقیت ویرایش شده است.";
        public static string TrainerDeletedSuccessfully = "مربی رشته ورزشی مدنظر  با موفقیت حذف شده است.";
        #endregion

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

        #region Certificate
        public static string CertificateNotFound = "مدرک مدنظر پیدا نشده است.";
        public static string CertificateDuplicated = "مدرک مدنظر قبلا ثبت شده است.";
        public static string CertificateAlreadyDeleted = "مدرک مدنظر قبلا حذف شده است.";
        #endregion

        #region Sport
        public static string SportNotFound = "رشته ورزشی مدنظر پیدا نشده است.";
        public static string SportDuplicated = "رشته ورزشی مدنظر قبلا ثبت شده است.";
        public static string SportAlreadyDeleted = "رشته ورزشی مدنظر قبلا حذف شده است.";
        #endregion

        #region Sport Class
        public static string SportClassNotFound = "کلاس ورزشی مدنظر پیدا نشده است.";
        public static string SportClassAlreadyDeleted = "کلاس ورزشی مدنظر قبلا حذف شده است.";
        public static string InvalidStartDateTimeInput = " تاریخ شروع وارد شده معتبر نمی باشد.";
        public static string InvalidEndTime = "ساعت انتهایی کلاس وارد شده باید بعد از ساعت شروع آن باشد.";

        #endregion

        #region Staffs

        #region Trainer
        public static string TrainerNotFound = "مربی رشته ورزشی مدنظر پیدا نشده است.";
        public static string TrainerDuplicated = "مربی رشته ورزشی مدنظر قبلا ثبت شده است.";
        public static string TrainerAlreadyDeleted = "مربی رشته ورزشی مدنظر قبلا حذف شده است.";
        public static string TrainerPositionDuplicated = "برای این مربی رشته ورزشی مدنظر قبلا ثبت شده است.";
        public static string InvalidSalaryInput = "دریافتی وارد شده نا معتبر است.";
        public static string ExistCertificateForUserTrainer = "این مدرک برای این کاربر به عنوان مربی رشته دیگر ثبت شده است.";
        #endregion

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

        #region Certificate
        public static string CertificateCantbeEdited = "این مدرک حذف شده است و امکان ویرایش آن وجود ندارد.";
        #endregion

        #region Sport
        public static string SportCantbeEdited = "این رشته ورزشی حذف شده است و امکان ویرایش آن وجود ندارد.";
        #endregion

        #region Sport Class
        public static string SportClassCantbeEdited = "این کلاس ورزشی حذف شده است و امکان ویرایش آن وجود ندارد.";
        #endregion

        #region Staff

        #region Trainer
        public static string TrainerCantbeEdited = "مربی برای این رشته ورزشی حذف شده است و امکان ویرایش آن وجود ندارد.";
        #endregion

        #endregion

    }

    public class InfoMessages
    {
        #region Certificate
        public static string CertificateDontExisted = "مدرک رشته ورزشی یافت نشد لطفا برای افزودن مربی مدرک آن را ساخت سپس مربی ایجاد بکنید.";
        #endregion
    }
}
