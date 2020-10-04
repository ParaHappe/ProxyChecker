using System;
using System.Collections.Generic;
namespace WpfApp2
{
    public class Proxy
    {
        public string ProxyIP { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string TimeOut { get; set; }

        public string Adress { get; set; }
        public string Port { get; set; }

        public Proxy(string proxyIP)
        {
            ProxyIP = proxyIP;

            string[] Data = ProxyIP.Split(new char[] { ':' });

            Adress = Data[0];
            Port = Data[1];
        }

    }
}
