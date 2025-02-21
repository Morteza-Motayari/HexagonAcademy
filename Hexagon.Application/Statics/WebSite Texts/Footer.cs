using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Statics.WebSite_Texts
{
    public class Footer
    {
        [JsonProperty("PhoneNumberDisplay")]
        public string PhoneNumberDisplay { get; set; }
        [JsonProperty("PhoneNumberCall")]
        public string PhoneNumberCall { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("CopyRight")]
        public string CopyRight { get; set; }
        [JsonProperty("CreatorEmail")]
        public string CreatorEmail { get; set; }
        [JsonProperty("InstaName")]
        public string InstaName { get; set; }
        [JsonProperty("InstaLink")]
        public string InstaLink { get; set; }
        [JsonProperty("TelegramName")]
        public string TelegramName { get; set; }
        [JsonProperty("TelegramLink")]
        public string TelegramLink { get; set; }
        [JsonProperty("WhatsappName")]
        public string WhatsappName { get; set; }
        [JsonProperty("WhatsappLink")]
        public string WhatsappLink { get; set; }
        [JsonProperty("Location")]
        public string Location { get; set; }
    }
}
