using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.Sports
{
    public enum DeleteForeverSportResult
    {
        Success,
        CantDeletedNow,
        FirstDeleteSimple,
        NotFound
    }
}
