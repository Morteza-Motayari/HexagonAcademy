using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Statics.WebSite_Texts
{
    public class HomeBoby
    {
        [JsonProperty("ServiceText")]
        public string ServiceText { get; set; }
        [JsonProperty("TrainersText")]
        public string TrainersText { get; set; }
    }
}
