using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Gyms.GymGalleries
{
    public enum DeleteGymGalleryResult
    {
        Success,
        GymImageNotFound
    }
}
