using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Enums.Users
{
    public enum UserGender
    {
        Male,
        Female
    }
    public enum UserStatus
    {
        Active,
        NotActive,
        Ban
    }
    public enum UserSituation
    {
        Cadre,
        Trainer,
        Athlete
    }
}
