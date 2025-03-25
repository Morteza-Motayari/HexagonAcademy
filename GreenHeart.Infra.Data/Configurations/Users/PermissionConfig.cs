using GreenHeart.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Configurations.Users
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            #region Users

            #region User
            builder.HasData(
                new Permission
                {
                    PermissionId = 1,
                    PermissionName = "ManageUsers",
                    PermissionTitle = "مدیریت کاربران"
                },
                new Permission
                {
                    PermissionId = 2,
                    PermissionName = "AddUser",
                    PermissionTitle = "افزودن کاربر",
                    ParentId = 1
                }, new Permission
                {
                    PermissionId = 3,
                    PermissionName = "EditUser",
                    PermissionTitle = "ویرایش کاربر",
                    ParentId = 1
                }, new Permission
                {
                    PermissionId = 4,
                    PermissionName = "DeleteUser",
                    PermissionTitle = "حذف کاربر",
                    ParentId = 1
                }, new Permission
                {
                    PermissionId = 5,
                    PermissionName = "DetailUser",
                    PermissionTitle = "جزئیات کاربر",
                    ParentId = 1
                }, new Permission
                {
                    PermissionId = 6,
                    PermissionName = "DeleteUserForever",
                    PermissionTitle = "حذف مطلق کاربر",
                    ParentId = 1
                }, new Permission
                {
                    PermissionId = 58,
                    PermissionName = "ChangeUserPassword",
                    PermissionTitle = "تغییر رمز کاربر",
                    ParentId = 1
                });
            #endregion

            #region Role
            builder.HasData(
                new Permission
                {
                    PermissionId = 7,
                    PermissionName = "ManageRoles",
                    PermissionTitle = "مدیریت نقش ها"
                },
                new Permission
                {
                    PermissionId = 8,
                    PermissionName = "AddRole",
                    PermissionTitle = "افزودن نقش",
                    ParentId = 7
                }, new Permission
                {
                    PermissionId = 9,
                    PermissionName = "EditRole",
                    PermissionTitle = "ویرایش نقش",
                    ParentId = 7
                }, new Permission
                {
                    PermissionId = 10,
                    PermissionName = "DeleteRole",
                    PermissionTitle = "حذف نقش",
                    ParentId = 7
                }, new Permission
                {
                    PermissionId = 11,
                    PermissionName = "DetailRole",
                    PermissionTitle = "جزئیات نقش",
                    ParentId = 7
                }, new Permission
                {
                    PermissionId = 12,
                    PermissionName = "DeleteRoleForever",
                    PermissionTitle = "حذف مطلق نقش",
                    ParentId = 7
                });
            #endregion

            #region Trainer
            builder.HasData(
                new Permission
                {
                    PermissionId = 13,
                    PermissionName = "ManageTrainers",
                    PermissionTitle = "مدیریت مربی ها"
                },
                new Permission
                {
                    PermissionId = 14,
                    PermissionName = "AddTrainer",
                    PermissionTitle = "افزودن مربی",
                    ParentId = 13
                }, new Permission
                {
                    PermissionId = 15,
                    PermissionName = "EditTrainer",
                    PermissionTitle = "ویرایش مربی",
                    ParentId = 13
                }, new Permission
                {
                    PermissionId = 16,
                    PermissionName = "DeleteTrainer",
                    PermissionTitle = "حذف مربی",
                    ParentId = 13
                }, new Permission
                {
                    PermissionId = 17,
                    PermissionName = "DetailTrainer",
                    PermissionTitle = "جزئیات مربی",
                    ParentId = 13
                }, new Permission
                {
                    PermissionId = 18,
                    PermissionName = "DeleteTrainerForever",
                    PermissionTitle = "حذف مطلق مربی",
                    ParentId = 13
                });
            #endregion

            #region Cadre
            builder.HasData(
                new Permission
                {
                    PermissionId = 19,
                    PermissionName = "ManageCaders",
                    PermissionTitle = "مدیریت کادرها"
                },
                new Permission
                {
                    PermissionId = 20,
                    PermissionName = "AddCader",
                    PermissionTitle = "افزودن کادر",
                    ParentId = 19
                }, new Permission
                {
                    PermissionId = 21,
                    PermissionName = "EditCader",
                    PermissionTitle = "ویرایش کادر",
                    ParentId = 19
                }, new Permission
                {
                    PermissionId = 22,
                    PermissionName = "DeleteCader",
                    PermissionTitle = "حذف کادر",
                    ParentId = 19
                }, new Permission
                {
                    PermissionId = 23,
                    PermissionName = "DetailCader",
                    PermissionTitle = "جزئیات کادر",
                    ParentId = 19
                }, new Permission
                {
                    PermissionId = 24,
                    PermissionName = "DeleteCaderForever",
                    PermissionTitle = "حذف مطلق کادر",
                    ParentId = 19
                });
            #endregion

            #endregion

            #region Records

            #region Certificate
            builder.HasData(
                new Permission
                {
                    PermissionId = 25,
                    PermissionName = "ManageCertificates",
                    PermissionTitle = "مدیریت مدرک ها"
                },
                new Permission
                {
                    PermissionId = 26,
                    PermissionName = "AddCertificate",
                    PermissionTitle = "افزودن مدرک",
                    ParentId = 25
                }, new Permission
                {
                    PermissionId = 27,
                    PermissionName = "EditCertificate",
                    PermissionTitle = "ویرایش مدرک",
                    ParentId = 25
                }, new Permission
                {
                    PermissionId = 28,
                    PermissionName = "DeleteCertificate",
                    PermissionTitle = "حذف مدرک",
                    ParentId = 25
                }, new Permission
                {
                    PermissionId = 29,
                    PermissionName = "DetailCertificate",
                    PermissionTitle = "جزئیات مدرک",
                    ParentId = 25
                }, new Permission
                {
                    PermissionId = 30,
                    PermissionName = "DeleteCertificateForever",
                    PermissionTitle = "حذف مطلق مدرک",
                    ParentId = 25
                });
            #endregion

            #region Experience
            builder.HasData(
                new Permission
                {
                    PermissionId = 31,
                    PermissionName = "ManageExperiences",
                    PermissionTitle = "مدیریت سابقه ها"
                },
                new Permission
                {
                    PermissionId = 32,
                    PermissionName = "AddExperience",
                    PermissionTitle = "افزودن سابقه",
                    ParentId = 31
                }, new Permission
                {
                    PermissionId = 33,
                    PermissionName = "EditExperience",
                    PermissionTitle = "ویرایش سابقه",
                    ParentId = 31
                }, new Permission
                {
                    PermissionId = 34,
                    PermissionName = "DeleteExperience",
                    PermissionTitle = "حذف سابقه",
                    ParentId = 31
                }, new Permission
                {
                    PermissionId = 35,
                    PermissionName = "DetailExperience",
                    PermissionTitle = "جزئیات سابقه",
                    ParentId = 31
                }, new Permission
                {
                    PermissionId = 36,
                    PermissionName = "DeleteExperienceForever",
                    PermissionTitle = "حذف مطلق سابقه",
                    ParentId = 31
                });
            #endregion

            #endregion

            #region Gyms

            #region Gym
            builder.HasData(
                new Permission
                {
                    PermissionId = 37,
                    PermissionName = "ManageGyms",
                    PermissionTitle = "مدیریت باشگاه ها"
                },
                new Permission
                {
                    PermissionId = 38,
                    PermissionName = "AddGym",
                    PermissionTitle = "افزودن باشگاه",
                    ParentId = 37
                }, new Permission
                {
                    PermissionId = 39,
                    PermissionName = "EditGym",
                    PermissionTitle = "ویرایش باشگاه",
                    ParentId = 37
                }, new Permission
                {
                    PermissionId = 40,
                    PermissionName = "DeleteGym",
                    PermissionTitle = "حذف باشگاه",
                    ParentId = 37
                }, new Permission
                {
                    PermissionId = 57,
                    PermissionName = "DetailGym",
                    PermissionTitle = "جزئیات باشگاه",
                    ParentId = 37
                }, new Permission
                {
                    PermissionId = 41,
                    PermissionName = "DeleteGymForever",
                    PermissionTitle = "حذف مطلق باشگاه",
                    ParentId = 37
                }, new Permission
                {
                    PermissionId = 42,
                    PermissionName = "GalleryGym",
                    PermissionTitle = "گالری باشگاه",
                    ParentId = 37
                }, new Permission
                {
                    PermissionId = 43,
                    PermissionName = "AddGalleryGym",
                    PermissionTitle = "افزودن گالری باشگاه",
                    ParentId = 37
                }, new Permission
                {
                    PermissionId = 44,
                    PermissionName = "DeleteGalleryGym",
                    PermissionTitle = "حذف گالری باشگاه",
                    ParentId = 37
                });
            #endregion

            #region Sport
            builder.HasData(
                new Permission
                {
                    PermissionId = 45,
                    PermissionName = "ManageSports",
                    PermissionTitle = "مدیریت رشته های ورزشی"
                },
                new Permission
                {
                    PermissionId = 46,
                    PermissionName = "AddSport",
                    PermissionTitle = "افزودن رشته ورزشی",
                    ParentId = 45
                }, new Permission
                {
                    PermissionId = 47,
                    PermissionName = "EditSport",
                    PermissionTitle = "ویرایش رشته ورزشی",
                    ParentId = 45
                }, new Permission
                {
                    PermissionId = 48,
                    PermissionName = "DeleteSport",
                    PermissionTitle = "حذف رشته ورزشی",
                    ParentId = 45
                }, new Permission
                {
                    PermissionId = 49,
                    PermissionName = "DetailSport",
                    PermissionTitle = "جزئیات رشته ورزشی",
                    ParentId = 45
                }, new Permission
                {
                    PermissionId = 50,
                    PermissionName = "DeleteSportForever",
                    PermissionTitle = "حذف مطلق رشته ورزشی",
                    ParentId = 45
                });
            #endregion

            #region SportClass
            builder.HasData(
                new Permission
                {
                    PermissionId = 51,
                    PermissionName = "ManageSportClasses",
                    PermissionTitle = "مدیریت کلاس های ورزشی"
                },
                new Permission
                {
                    PermissionId = 52,
                    PermissionName = "AddSportClass",
                    PermissionTitle = "افزودن کلاس ورزشی",
                    ParentId = 51
                }, new Permission
                {
                    PermissionId = 53,
                    PermissionName = "EditSportClass",
                    PermissionTitle = "ویرایش کلاس ورزشی",
                    ParentId = 51
                }, new Permission
                {
                    PermissionId = 54,
                    PermissionName = "DeleteSportClass",
                    PermissionTitle = "حذف کلاس ورزشی",
                    ParentId = 51
                }, new Permission
                {
                    PermissionId = 55,
                    PermissionName = "DetailSportClass",
                    PermissionTitle = "جزئیات کلاس ورزشی",
                    ParentId = 51
                }, new Permission
                {
                    PermissionId = 56,
                    PermissionName = "DeleteSportClassForever",
                    PermissionTitle = "حذف مطلق کلاس ورزشی",
                    ParentId = 51
                });
            #endregion

            #region Class Comment
            builder.HasData(
                new Permission
                {
                    PermissionId = 86,
                    PermissionName = "ManageClassComments",
                    PermissionTitle = "مدیریت نظرات کلاس ها"
                },
                new Permission
                {
                    PermissionId = 87,
                    PermissionName = "GetComments",
                    PermissionTitle = "مشاهده نظر",
                    ParentId = 86
                }, new Permission
                {
                    PermissionId = 88,
                    PermissionName = "ChangeCommentStatus",
                    PermissionTitle = "تغییر وضعیت نظر",
                    ParentId = 86
                }, new Permission
                {
                    PermissionId = 89,
                    PermissionName = "DeleteComment",
                    PermissionTitle = "حذف نظر",
                    ParentId = 86
                }, new Permission
                {
                    PermissionId = 90,
                    PermissionName = "DetailComment",
                    PermissionTitle = "جزئیات نظر",
                    ParentId = 86
                }, new Permission
                {
                    PermissionId = 91,
                    PermissionName = "DeleteCommentForever",
                    PermissionTitle = "حذف مطلق نظر",
                    ParentId = 86
                });
            #endregion

            #endregion

            #region Banners
            builder.HasData(
                new Permission
                {
                    PermissionId = 59,
                    PermissionName = "ManageBanners",
                    PermissionTitle = "مدیریت بنرها"
                },
                new Permission
                {
                    PermissionId = 60,
                    PermissionName = "AddBanner",
                    PermissionTitle = "افزودن بنر",
                    ParentId = 59
                }, new Permission
                {
                    PermissionId = 61,
                    PermissionName = "DeleteBanner",
                    PermissionTitle = "افزودن بنر",
                    ParentId = 59
                });
            #endregion

            #region Contact Us
            builder.HasData(
                new Permission
                {
                    PermissionId = 62,
                    PermissionName = "ManageContactUses",
                    PermissionTitle = "مدیریت ارتباط با ما"
                }, new Permission
                {
                    PermissionId = 63,
                    PermissionName = "AnswerContactUs",
                    PermissionTitle = "ویرایش ارتباط با ما",
                    ParentId = 62
                }, new Permission
                {
                    PermissionId = 64,
                    PermissionName = "DeleteContactUs",
                    PermissionTitle = "حذف ارتباط با ما",
                    ParentId = 62
                }, new Permission
                {
                    PermissionId = 65,
                    PermissionName = "DetailContactUs",
                    PermissionTitle = "جزئیات ارتباط با ما",
                    ParentId = 62
                }, new Permission
                {
                    PermissionId = 66,
                    PermissionName = "DeleteContactUsForever",
                    PermissionTitle = "حذف مطلق ارتباط با ما",
                    ParentId = 62
                });
            #endregion

            #region Key Words
            builder.HasData(
                new Permission
                {
                    PermissionId = 67,
                    PermissionName = "ManageKeyWords",
                    PermissionTitle = "مدیریت کلمات کلیدی"
                },
                new Permission
                {
                    PermissionId = 68,
                    PermissionName = "AddKeyWord",
                    PermissionTitle = "افزودن کلمه کلیدی",
                    ParentId = 67
                }, new Permission
                {
                    PermissionId = 69,
                    PermissionName = "EditKeyWord",
                    PermissionTitle = "ویرایش کلمه کلیدی",
                    ParentId = 67
                }, new Permission
                {
                    PermissionId = 70,
                    PermissionName = "DeleteKeyWord",
                    PermissionTitle = "حذف کلمه کلیدی",
                    ParentId = 67
                }, new Permission
                {
                    PermissionId = 71,
                    PermissionName = "DetailKeyWord",
                    PermissionTitle = "جزئیات کلمه کلیدی",
                    ParentId = 67
                }, new Permission
                {
                    PermissionId = 72,
                    PermissionName = "DeleteKeyWordForever",
                    PermissionTitle = "حذف مطلق کلمه کلیدی",
                    ParentId = 67
                });
            #endregion

            #region Tickets
            builder.HasData(
                new Permission
                {
                    PermissionId = 73,
                    PermissionName = "ManageTickets",
                    PermissionTitle = "مدیریت تیکت ها"
                },
                new Permission
                {
                    PermissionId = 74,
                    PermissionName = "AnswerTicket",
                    PermissionTitle = "پاسخ تیکت",
                    ParentId = 73
                },
                new Permission
                {
                    PermissionId = 75,
                    PermissionName = "ChangeTicketStatus",
                    PermissionTitle = "تغییر وضعیت تیکت",
                    ParentId = 73
                }, new Permission
                {
                    PermissionId = 76,
                    PermissionName = "DeleteTicket",
                    PermissionTitle = "حذف تیکت",
                    ParentId = 73
                }, new Permission
                {
                    PermissionId = 77,
                    PermissionName = "DetailTicket",
                    PermissionTitle = "جزئیات تیکت",
                    ParentId = 73
                }, new Permission
                {
                    PermissionId = 78,
                    PermissionName = "DeleteTicketForever",
                    PermissionTitle = "حذف مطلق تیکت",
                    ParentId = 73
                }, new Permission
                {
                    PermissionId = 79,
                    PermissionName = "DeleteTicketMessage",
                    PermissionTitle = "حذف پیام تیکت",
                    ParentId = 73
                }, new Permission
                {
                    PermissionId = 80,
                    PermissionName = "DeleteTicketMessageForever",
                    PermissionTitle = "حذف مطلق پیام تیکت",
                    ParentId = 73
                });
            #endregion

            #region Wallets
            builder.HasData(
                new Permission
                {
                    PermissionId = 81,
                    PermissionName = "ManageWallets",
                    PermissionTitle = "مدیریت کیف پول ها"
                },
                new Permission
                {
                    PermissionId = 82,
                    PermissionName = "ChargeUserWallet",
                    PermissionTitle = "شارژ کیف پول کاربر",
                    ParentId = 81
                }, new Permission
                {
                    PermissionId = 83,
                    PermissionName = "DetailWallet",
                    PermissionTitle = "جزئیات کیف پول",
                    ParentId = 81
                });
            #endregion

            #region Orders
                        builder.HasData(
                new Permission
                {
                    PermissionId = 84,
                    PermissionName = "ManageOrders",
                    PermissionTitle = "مدیریت فاکتور ها"
                }, new Permission
                {
                    PermissionId = 85,
                    PermissionName = "DetailOrder",
                    PermissionTitle = "جزئیات فاکتور",
                    ParentId = 84
                });
            #endregion
        }
    }
}
