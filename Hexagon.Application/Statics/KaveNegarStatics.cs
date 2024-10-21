using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Statics
{
    public class KaveNegarStatics
    {
        [JsonProperty("ApiKey")]
        public string ApiKey { get; set; }
        [JsonProperty("Sender")]
        public string Sender { get; set; }
    }
}
