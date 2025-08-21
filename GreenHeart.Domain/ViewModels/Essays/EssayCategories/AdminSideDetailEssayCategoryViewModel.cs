using GreenHeart.Domain.Models.Essays;
using GreenHeart.Domain.ViewModels.Common;
using GreenHeart.Domain.ViewModels.Essays.Essays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Essays.EssayCategories
{
    public class AdminSideDetailEssayCategoryViewModel: BaseAdminDetail
    {
        public string Title { get; set; }
        public EssayCategoryViewModel? EssayCategoryParent { get; set; }
        public ICollection<EssayCategoryViewModel> EssayCategoryChilds { get; set; }
        public ICollection<EssayViewModel>? Essays { get; set; }
    }
}
