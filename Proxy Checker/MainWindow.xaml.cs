using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        bool isRunning = false;
        bool isThreadsRunning = false;
        public ObservableCollection<Proxy> validProxyList = new ObservableCollection<Proxy>();


        public MainWindow()
        {
            InitializeComponent();
            ProxyDataTable.ItemsSource = validProxyList;
        }

        private void LoadProxy_Click(object sender, RoutedEventArgs e)
        {
            string path = FileManager.GetFilePath();
            if(path != null)
            {
                FileManager.LoadProxyFromFileToCollection(path, ref ProxyQueue.proxyQueue);
            }
            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {


            if (isRunning)
            {
                StartButton.Content = "Запустить";
                isRunning = false;
                ThreadsManager.StopThreads();
                isThreadsRunning = false;
            }
            else
            {
                StartButton.Content = "Стоп";
                isRunning = true;
                ThreadsManager.StartCheckingProcess(Convert.ToInt32(ThreadsNum.Text), URL.Text);
                isThreadsRunning = true;
            }

        }

        private void Window_Closing(object sender, EventArgs e)
        {
            if (isThreadsRunning)
            {
                ThreadsManager.StopThreads();
            }
        }
    }

}
