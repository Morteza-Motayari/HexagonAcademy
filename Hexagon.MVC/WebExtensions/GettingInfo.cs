namespace Hexagon.MVC.WebExtensions
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

    }
}
