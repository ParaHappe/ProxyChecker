using System.Collections.Concurrent;

namespace WpfApp2
{
    static class ProxyQueue
    {
        public static ConcurrentQueue<Proxy> proxyQueue = new ConcurrentQueue<Proxy>();
    }
}
