using Hexagon.Domain.Enums.SportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Gyms.ClassComments
{
    public enum DeleteCommentStatusResult
    {
        Success,
        ClassCommentNotFound,
        ClassCommentAlreadyDeleted
    }
}
