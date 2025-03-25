using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Enums.Filter
{
    public enum ExistingStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "حذف شده ها")]
        Deleted,
        [Display(Name = " موجود")]
        NotDeleted
    }
    public enum FilterUserGender
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "مذکر")]
        Male,
        [Display(Name = "مونث")]
        Female
    }
    public enum FilterUserStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فعال")]
        Active,
        [Display(Name = "غیرفعال")]
        NotActive,
        [Display(Name = "مسدود")]
        Ban
    }
    public enum FilterUserSituation
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "کادر")]
        Cadre,
        [Display(Name = "مربی")]
        Trainer,
        [Display(Name = "ورزشکار")]
        Athlete
    }
    public enum FilterSportClassStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فعال")]
        Active,
        [Display(Name = "غیرفعال")]
        NotActive,
        [Display(Name = "بسته شده")]
        Closed
    }
    public enum FilterContactUsStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "پاسخ داده شده")]
        Answered,
        [Display(Name = "منتظر پاسخ")]
        UnAnswered
    }
    public enum FilterOrderStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "نهایی شده")]
        Payed,
        [Display(Name = "منتظر پرداخت")]
        UnPayed
    }
    public enum FilterTransactionType
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "برداشت")]
        Creditor,
        [Display(Name = "واریز")]
        Deposit
    }
    public enum FilterTransactionCase
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "شارژ کیف پول")]
        ChargeWallet,
        [Display(Name = "پرداخت فاکتور")]
        PayOrder
    }
    public enum FilterPayementStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "پرداخت شده")]
        Payed,
        [Display(Name = "پرداخت نشده")]
        UnPayed
    }
    public enum FilterRegisteredAthletesStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "نیاز به تمدید")]
        NeedExtension,
        [Display(Name = "ثبت نام شده")]
        Registered
    }
}
