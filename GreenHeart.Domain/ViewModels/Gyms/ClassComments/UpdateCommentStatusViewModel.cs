using GreenHeart.Domain.Enums.SportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.ClassComments
{
    public class UpdateCommentStatusViewModel
    {
        public int Id { get; set; }
    }
    public enum UpdateCommentStatusResult
    {
        Success,
        ClassCommentNotFound
    }
}
