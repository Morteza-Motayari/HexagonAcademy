using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Statics.Caches_Constatnt
{
    public static class CacheDuration
    {
        public static TimeSpan NormalCahingTime = TimeSpan.FromMinutes(20);
        public static TimeSpan ClassHomeCahingTime = TimeSpan.FromDays(2);
        public static TimeSpan SportClassCahingTime = TimeSpan.FromDays(10);
    }
}
