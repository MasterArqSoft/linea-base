using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Sockets;

namespace microservice.domain.Helpers;

public static class UtilitiesHelper
{
    public static string GetClientIPAddress(HttpContext context)
    {
        string ipAddress = string.Empty;
        IPAddress ip = context.Connection.RemoteIpAddress!;
        if (ip != null)
        {
            if (ip.AddressFamily == AddressFamily.InterNetworkV6)
            {
                ip = Dns.GetHostEntry(ip).AddressList
                        .First(x => x.AddressFamily == AddressFamily.InterNetwork);
            }
            ipAddress = ip.ToString();
        }
        return ipAddress;
    }
}
