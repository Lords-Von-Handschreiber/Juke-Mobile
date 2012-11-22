using Juke_Mobile_Core;
using Juke_Mobile_Gui.Helper;
using Juke_Mobile_Model;
using Juke_Mobile_Model.Database;
using Raven.Client.Embedded;
using System;
using System.IO;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _player = new DoublePlayer(Player1, Player1Progress, Player1Remaining, Player2, Player2Progress, Player2Remaining);

            //QueueList.DataContext = Db.Instance.Query<dynamic>()

            Application.Current.Exit += CloseServer;
        }

        /// <summary>
        /// Handles the Click event of the MenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        /// <summary>
        /// Handles the Click event of the Start control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (_server != null)
                return;

            HttpSelfHostConfiguration cfg = HttpSelfHostConfigurationFactory.CreateInstance();
            cfg.Filters.Add(new RavenDbApiAttribute(DbDocumentStore.Instance));

            _server = new HttpSelfHostServer(cfg);
            _server.OpenAsync().Wait();
            txtServerStatus.Text = "running";
        }

        /// <summary>
        /// Handles the Click event of the End control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void End_Click(object sender, RoutedEventArgs e)
        {
            CloseServer(sender, null);
        }

        /// <summary>
        /// Closes the server.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ExitEventArgs" /> instance containing the event data.</param>
        private void CloseServer(object sender, ExitEventArgs e)
        {
            if (_server == null)
                return;
            _server.CloseAsync().Wait();
            _server.Dispose();
            _server = null;
            txtServerStatus.Text = "stopped";
        }

        /// <summary>
        /// Handles the Click event of the Player1Play control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Player1Play_Click(object sender, RoutedEventArgs e)
        {
            _player.Play();
        }

        /// <summary>
        /// Handles the Click event of the Player1Pause control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Player1Pause_Click(object sender, RoutedEventArgs e)
        {
            _player.Pause();
        }

        /// <summary>
        /// Handles the Click event of the Player1Stop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Player1Stop_Click(object sender, RoutedEventArgs e)
        {
            _player.Stop();
        }

        /// <summary>
        /// Balance_s the value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Balance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Balance != null && Player1 != null && Player2 != null)
            {
                SetCalculatedVolume(Player1, VolumePlayer1.Value * (1 - ((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum))));
                SetCalculatedVolume(Player2, VolumePlayer2.Value * ((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum)));
            }
        }

        /// <summary>
        /// Volumes the player1_ value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void VolumePlayer1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Balance != null)
                SetCalculatedVolume(Player1, e.NewValue * (1 - ((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum))));
        }

        /// <summary>
        /// Volumes the player2_ value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void VolumePlayer2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Balance != null)
                SetCalculatedVolume(Player2, e.NewValue * ((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum)));
        }

        /// <summary>
        /// Sets the calculated volume.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="value">The value.</param>
        private void SetCalculatedVolume(MediaElement player, double value)
        {
            if (player != null)
                player.Volume = value;
        }

        /// <summary>
        /// Handles the MouseLeftButtonUp event of the Player1Progress control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs" /> instance containing the event data.</param>
        private void Player1Progress_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Player1.Position = new TimeSpan(0);
        }
    }
}
