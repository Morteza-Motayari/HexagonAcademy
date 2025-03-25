using Microsoft.Extensions.Primitives;
using UAParser;

namespace GreenHeart.MVC.WebExtensions
{
    public static class GettingInfo
    {
        public static string GettingIP(this HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress?.ToString()??string.Empty;
        }

        public static string GettingOStype(this OperatingSystem operatingSystem)
        {
            return operatingSystem.VersionString.ToString();
        }
        public static string GettingClientOSType(this StringValues request)
        {
            string userAgent = request.ToString();

            var parser = Parser.GetDefault();
            var clientInfo = parser.Parse(userAgent);
            // Get the operating system and version details
            string osFamily = clientInfo.OS.Family; // e.g., "Windows"
            string osVersion = $"{clientInfo.OS.Major}"; // e.g., "10.0"

            if (!string.IsNullOrEmpty(clientInfo.OS.Minor))
            {
                osVersion += $".{clientInfo.OS.Minor}"; // Append patch version if available
            }
            if (!string.IsNullOrEmpty(clientInfo.OS.Patch))
            {
                osVersion += $".{clientInfo.OS.Patch}"; // Append patch version if available
            }
            string fullOS = $" {osFamily} {osVersion}";

            // Extract device information
            string deviceBrand = clientInfo.Device.Brand; // e.g., "Samsung"
            string deviceModel = clientInfo.Device.Model; // e.g., "Galaxy S20"

            string deviceName = !string.IsNullOrEmpty(deviceBrand) && !string.IsNullOrEmpty(deviceModel)
                ? $"{deviceBrand} {deviceModel}"
                : clientInfo.Device.ToString();

            return deviceName + fullOS;
        }

    }
}
