using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Models.Banners
{
    public class Banner:BaseEntity<int>
    {
        public string BannerUrl { get; set; }
        public string BannerName { get; set; }
    }
}
