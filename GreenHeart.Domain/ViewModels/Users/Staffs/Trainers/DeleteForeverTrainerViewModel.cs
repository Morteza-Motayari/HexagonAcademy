using GreenHeart.Domain.Models.Links;
using GreenHeart.Domain.Models.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Users.Staffs.Trainers
{
    public enum DeleteForeverTrainerResult
    {
        Success,
        CantDeletedNow,
        FirstDeleteSimple,
        NotFound
    }
}
