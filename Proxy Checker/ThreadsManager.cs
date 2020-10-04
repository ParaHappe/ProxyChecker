using System.Collections.Generic;
using System.Threading;

namespace WpfApp2
{
    static class ThreadsManager
    {
        static List<Thread> threads = new List<Thread>();

        public static void StartCheckingProcess(int threadsNumber, string checkingURL)
        {
            Checker.URL = checkingURL;
            threads = new List<Thread>();
            for(int i = 0; i < threadsNumber; i++)
            {
                Thread thread = new Thread(CheckingThread);
                thread.Start();
                threads.Add(thread);
            }
        }

        private static void CheckingThread()
        {
            while (!ProxyQueue.proxyQueue.IsEmpty)
            {
                Proxy proxy;
                if(ProxyQueue.proxyQueue.TryDequeue(out proxy))
                {
                    Checker.CheckOne(proxy);
                }
            }
        }

        public static void StopThreads()
        {
            foreach(var thr in threads)
            {
                thr.Abort();
            }
            
        }
    }
}
