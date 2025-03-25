using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Models.Banners
{
    public class Banner:BaseEntity<int>
    {
        public string BannerUrl { get; set; }
        public string BannerName { get; set; }
    }
}
