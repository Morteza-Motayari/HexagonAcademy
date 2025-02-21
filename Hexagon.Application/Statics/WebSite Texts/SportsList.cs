using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Statics.WebSite_Texts
{
    public class SportsList
    {
        [JsonProperty("SportGoal")]
        public string SportGoal { get; set; }
    }
}
