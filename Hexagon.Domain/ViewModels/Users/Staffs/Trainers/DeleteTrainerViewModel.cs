using Hexagon.Domain.Models.Links;
using Hexagon.Domain.Models.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Users.Staffs.Trainers
{
    public enum DeleteTrainerResult
    {
        Success,
        TrainerNotFound,
        TrainerAlreadyDeleted
    }
}
