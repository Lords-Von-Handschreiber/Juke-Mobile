using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Juke_Mobile_Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpSelfHostServer server;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            CloseServer();
            Application.Current.Shutdown(0);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (server != null)
                return;

            HttpSelfHostConfigurationFactory fact = new HttpSelfHostConfigurationFactory();
            HttpSelfHostConfiguration cfg = fact.CreateInstance();
            server = new HttpSelfHostServer(cfg);                       
            server.OpenAsync().Wait();
            txtServerStatus.Text = "running"; 

        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            CloseServer();
        }

        private void CloseServer()
        {
            if (server == null)
                return;
            server.CloseAsync().Wait();
            server.Dispose();
            server = null;
            txtServerStatus.Text = "stopped"; 
        }
    }
}
