using System;
using System.IO;
using System.Windows;
using System.Collections.Concurrent;

namespace WpfApp2
{
    static class FileManager
    {
        public static string GetFilePath()
        {

            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Text documents (*.txt)|*.txt";

            if(dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;

        }

        public static void LoadProxyFromFileToCollection(string filePath, ref ConcurrentQueue<Proxy> collection)
        {
            collection = new ConcurrentQueue<Proxy>();
            Statistics.SetToZero((Application.Current.MainWindow as MainWindow).LoadedProxyTextBlock);
            Statistics.SetToZero((Application.Current.MainWindow as MainWindow).ValidTextBlock);
            Statistics.SetToZero((Application.Current.MainWindow as MainWindow).InvalidTextBlock);
            try
            {
                
                using(StreamReader sr = new StreamReader(filePath))
                {
                    string line;

                    while((line = sr.ReadLine()) != null)
                    {
                        Proxy proxy = new Proxy(line);
                        collection.Enqueue(proxy);
                        Statistics.IncrementValue((Application.Current.MainWindow as MainWindow).LoadedProxyTextBlock);
                    }
                }
            } catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }



        }

    }
}
