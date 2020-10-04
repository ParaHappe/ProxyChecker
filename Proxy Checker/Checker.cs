using System;
using System.Windows;
using System.Diagnostics;
using xNet;

namespace WpfApp2
{
    static class Checker
    {
        public static string URL { get; set; }
        private static int timeout = 8000;

        public static void CheckOne(Proxy proxy)
        {
            (bool valid, string timeout, string country) = IsHTTP(proxy.ProxyIP, proxy.Adress);
            if (valid)
            {
                Application.Current.Dispatcher.Invoke(() => Statistics.IncrementValue((Application.Current.MainWindow as MainWindow).ValidTextBlock));
                proxy.TimeOut = timeout + "ms";
                proxy.Type = "HTTP";
                proxy.Country = country;
                Application.Current.Dispatcher.Invoke(() => (Application.Current.MainWindow as MainWindow).validProxyList.Add(proxy));
                Application.Current.Dispatcher.Invoke(() => Statistics.IncrementValue((Application.Current.MainWindow as MainWindow).Checked));

                return;
            }

            (valid, timeout, country) = IsSOCKS4(proxy.ProxyIP, proxy.Adress);
            if (valid)
            {
                Application.Current.Dispatcher.Invoke(() => Statistics.IncrementValue((Application.Current.MainWindow as MainWindow).ValidTextBlock));
                proxy.TimeOut = timeout + "ms";
                proxy.Type = "SOCKS4";
                proxy.Country = country;
                Application.Current.Dispatcher.Invoke(() => (Application.Current.MainWindow as MainWindow).validProxyList.Add(proxy));
                Application.Current.Dispatcher.Invoke(() => Statistics.IncrementValue((Application.Current.MainWindow as MainWindow).Checked));
                return;
            }

            (valid, timeout, country) = IsSOCKS5(proxy.ProxyIP, proxy.Adress);
            if (valid)
            {
                Application.Current.Dispatcher.Invoke(() => Statistics.IncrementValue((Application.Current.MainWindow as MainWindow).ValidTextBlock));
                proxy.TimeOut = timeout + "ms";
                proxy.Type = "SOCKS5";
                proxy.Country = country;
                Application.Current.Dispatcher.Invoke(() => (Application.Current.MainWindow as MainWindow).validProxyList.Add(proxy));
                Application.Current.Dispatcher.Invoke(() => Statistics.IncrementValue((Application.Current.MainWindow as MainWindow).Checked));
                return;
            }

            Application.Current.Dispatcher.Invoke(() => Statistics.IncrementValue((Application.Current.MainWindow as MainWindow).InvalidTextBlock));
            Application.Current.Dispatcher.Invoke(() => Statistics.IncrementValue((Application.Current.MainWindow as MainWindow).Checked));
        }

        private static (bool, string, string) IsHTTP(string proxyIP, string Adress)
        {
            Stopwatch stopWatch = new Stopwatch();
            string content;
            try
            {
                using (var request = new HttpRequest())
                {
                    request.Proxy = HttpProxyClient.Parse(proxyIP);
                    request.Proxy.ConnectTimeout = timeout;
                    request.UserAgent = Http.ChromeUserAgent();
                    stopWatch.Start();
                    request.Get(URL);
                    stopWatch.Stop();
                    HttpResponse response = request.Get($"http://ip-api.com/json/{Adress}?fields=countryCode");
                    string temp = response.ToString();
                    content = temp.Substring(16, temp.Length-16).Substring(0, temp.Length-18);
                    
                }
            } catch(HttpException ex)
            {
                return (false, null, null);
            }

            return (true, Convert.ToInt32(stopWatch.Elapsed.TotalMilliseconds).ToString(), content);
        }

        private static (bool, string, string) IsSOCKS4(string proxyIP, string Adress)
        {
            Stopwatch stopWatch = new Stopwatch();
            string content;
            try
            {
                using (var request = new HttpRequest())
                {
                    request.Proxy = Socks4ProxyClient.Parse(proxyIP);
                    request.Proxy.ConnectTimeout = timeout;
                    request.UserAgent = Http.ChromeUserAgent();
                    stopWatch.Start();
                    request.Get(URL);
                    stopWatch.Stop();
                    HttpResponse response = request.Get($"http://ip-api.com/json/{Adress}?fields=countryCode");
                    string temp = response.ToString();
                    content = temp.Substring(16, temp.Length - 16).Substring(0, temp.Length - 18);
                }
            }
            catch (HttpException  ex)
            {
                return (false, null, null);
            }
            return (true, Convert.ToInt32(stopWatch.Elapsed.TotalMilliseconds).ToString(), content);
        }

        private static (bool, string, string) IsSOCKS5(string proxyIP, string Adress)
        {
            Stopwatch stopWatch = new Stopwatch();
            string content;
            try
            {
                using (var request = new HttpRequest())
                {
                    request.Proxy = Socks5ProxyClient.Parse(proxyIP);
                    request.Proxy.ConnectTimeout = timeout;
                    request.UserAgent = Http.ChromeUserAgent();
                    stopWatch.Start();
                    request.Get(URL);
                    stopWatch.Stop();
                    HttpResponse response = request.Get($"http://ip-api.com/json/{Adress}?fields=countryCode");
                    string temp = response.ToString();
                    content = temp.Substring(16, temp.Length - 16).Substring(0, temp.Length - 18);
                }
            }
            catch (HttpException ex)
            {
                return (false, null, null);
            }
            return (true, Convert.ToInt32(stopWatch.Elapsed.TotalMilliseconds).ToString(), content);
        }
        
        private static string GetCountryCode(Proxy proxy)
        {

            return null;
        }
    }
}
