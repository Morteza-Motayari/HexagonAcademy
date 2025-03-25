using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.SportClasses
{
    
    public enum DeleteSportClassResult
    {
        Success,
        SportClassNotFound,
        SportClassAlreadyDeleted
    }
}
