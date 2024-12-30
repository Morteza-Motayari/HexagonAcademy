using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.Gyms
{
    public enum DeleteForeverGymResult
    {
        Success,
        CantDeletedNow,
        FirstDeleteSimple,
        NotFound
    }
}
