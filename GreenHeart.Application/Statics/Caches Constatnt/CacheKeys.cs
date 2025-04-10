using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Statics.Caches_Constatnt
{
    public static class CacheKeys
    {
        #region Banner
        public const string AllBanners = "AllBanners";
        #endregion

        #region Sport
        public const string ActiveSports = "ActiveSports";
        public const string SportsExisted = "SportsExisted";

        public static readonly string[] SportRelatedKeys =
        {
        ActiveSports,
        SportsExisted
        };
        #endregion

        #region Sport Class
        public const string SportClass = "SportClass_";
        public const string SportClassesHomePage = "SportClassHomePage";
        public const string SportClassPattern = "SportClassPage_*";
        public const string SportClassPage = "SportClassPage_";
        public static readonly string[] SportClassRelatedKeys =
        {
        SportClassPattern,
        SportClassesHomePage
        };
        #endregion

        #region Class Comment
        public const string ClassComment = "ClassComments_";
        #endregion

        #region Users

        #region Trainer
        public const string Trainer = "Trainer_";
        public const string Trainers = "Trainer_*";
        public const string TrainerHomePage = "TrainerHomePage";
        public const string TrainerPage = "TrainerPage_";
        public const string TrainerPattern = "TrainerPage_*";
        public static readonly string[] TrainerRelatedKeys =
        {
        TrainerPattern,
        TrainerHomePage
        };
        #endregion

        #region Cader
        public const string CaderAboutUsPage = "CaderAboutUsPage";        
        #endregion

        #endregion
    }
}
