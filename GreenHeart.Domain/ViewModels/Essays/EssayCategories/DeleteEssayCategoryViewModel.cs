using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Essays.EssayCategories
{
    public enum DeleteEssayCategoryResult
    {
        Success,
        EssayCategoryAlreadyDeleted,
        EssayCategoryNotFound
    }
}
