using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Statics
{
    public class MelipayamakStatics
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [JsonProperty("PassWord")]
        public string PassWord { get; set; }
    }
}
