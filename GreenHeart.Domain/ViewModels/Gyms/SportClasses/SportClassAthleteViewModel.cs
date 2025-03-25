using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.SportClasses
{
    public class SportClassAthleteViewModel
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime ExtensionDate { get; set; }
    }
}
