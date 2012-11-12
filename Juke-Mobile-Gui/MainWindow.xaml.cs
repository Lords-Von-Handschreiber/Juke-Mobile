using Juke_Mobile_Core;
using System;
using System.Web.Http.SelfHost;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Juke_Mobile_Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpSelfHostServer _server;

        DoublePlayer _player;

        public MainWindow()
        {
            InitializeComponent();

            _player = new DoublePlayer(Player1, Player1Progress, Player1Remaining, Player2, Player2Progress, Player2Remaining);
            _player.Load(new Uri(@"C:\Users\Thomas\Music\01 - Sonnentanz (Original Version).mp3"));

            Application.Current.Exit += CloseServer;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (_server != null)
                return;

            HttpSelfHostConfiguration cfg = HttpSelfHostConfigurationFactory.CreateInstance();
            _server = new HttpSelfHostServer(cfg);
            _server.OpenAsync().Wait();
            txtServerStatus.Text = "running";
        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            CloseServer(sender, null);
        }

        private void CloseServer(object sender, ExitEventArgs e)
        {
            if (_server == null)
                return;
            _server.CloseAsync().Wait();
            _server.Dispose();
            _server = null;
            txtServerStatus.Text = "stopped";
        }

        private void Player1Play_Click(object sender, RoutedEventArgs e)
        {
            _player.Play();
        }

        private void Player1Pause_Click(object sender, RoutedEventArgs e)
        {
            _player.Pause();
        }

        private void Player1Stop_Click(object sender, RoutedEventArgs e)
        {
            _player.Stop();
        }

        private void Balance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Balance != null && Player1 != null && Player2 != null)
            {
                SetCalculatedVolume(Player1, VolumePlayer1.Value * (1 - ((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum))));
                SetCalculatedVolume(Player2, VolumePlayer2.Value * ((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum)));
            }
        }

        private void VolumePlayer1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Balance != null)
                SetCalculatedVolume(Player1, e.NewValue * (1 - ((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum))));
        }

        private void VolumePlayer2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Balance != null)
                SetCalculatedVolume(Player2, e.NewValue * ((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum)));
        }

        private void SetCalculatedVolume(MediaElement player, double value)
        {
            if (player != null)
                player.Volume = value;
        }
    }
}
