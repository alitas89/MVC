using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLayer.Tools
{
    public static class IpGenerator
    {
        public static string GetIpAddress()
        {
            string myip = "";
            IPHostEntry iph = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in iph.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myip = ip.ToString();
                }
            }

            return myip;
        }
    }
}
