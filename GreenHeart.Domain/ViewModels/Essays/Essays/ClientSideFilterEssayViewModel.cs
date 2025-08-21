using GreenHeart.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Essays.Essays
{
    public class ClientSideFilterEssayViewModel:BasePaging<ClientSideEssayViewModel>
    {
        [Display(Name = "عنوان مقاله")]
        public string Title { get; set; }
        public string? KeyWord { get; set; }
        [Display(Name = "گروه مقاله")]
        public int? EssayCategoryId { get; set; }
        public string EssayCategorySlug { get; set; }

    }
}
