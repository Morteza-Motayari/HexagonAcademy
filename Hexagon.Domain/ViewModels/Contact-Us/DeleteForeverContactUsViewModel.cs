using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Contact_Us
{
    public enum DeleteForeverContactUsResult
    {
        Success,
        CantDeletedNow,
        FirstDeleteSimple,
        NotFound
    }
}
