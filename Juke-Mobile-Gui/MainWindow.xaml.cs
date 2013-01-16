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
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Juke_Mobile_Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPlayRequestReceiver, IDisposable
    {
        HttpSelfHostServer _server;
        /// <summary>
        /// public static for threadsafety. List that updates UI
        /// </summary>
        public static ObservableCollection<PlayRequest> uploadedTracks = new ObservableCollection<PlayRequest>();
        DoublePlayer _player;
        private IMusicInfoImporter importer = new MusicInfoImporterNoDelete();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            QueueList.DataContext = uploadedTracks;

            PlayRequestManager.Attach(this);
            List<PlayRequest> requests = PlayRequestManager.GetPlayList(PlayRequest.PlayRequestTypeEnum.Queue);
            foreach (var request in requests)
            {
                uploadedTracks.Add(request);
            }

            if (uploadedTracks.Count != 0)
            {
                QueueList.SelectedIndex = 0;
            }

            _player = new DoublePlayer(Player1, Player1Progress, Player1Remaining, Player2, Player2Progress, Player2Remaining);
            _player.mediaEnded += _player_mediaEnded;            

            Application.Current.Exit += CloseServer;
        }

        //Event Handling if song stopped.
        void _player_mediaEnded(object sender, EventArgs e)
        {
            uploadedTracks.RemoveAt(0);
            QueueList.SelectedIndex = 0;
            PlayRequest info = PlayRequestManager.GetNextRequest(PlayRequest.PlayRequestTypeEnum.Queue);
            PlayRequestManager.MovePlayRequestToHistory(info);
            PlayRequest  req = PlayRequestManager.GetNextRequest(PlayRequest.PlayRequestTypeEnum.Queue);
            if (req != null)
            {
                _player.Load(new Uri(req.MusicInfo.PhysicalPath));
                _player.Play();
            }
            else
            {
                _player.Stop();
            }            
            
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
            PlayRequestManager.Detach(this);
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
            //MusicInfo mi = (MusicInfo)QueueList.SelectedItem;
            //_player.Load(new Uri(mi.PhysicalPath));
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
                SetCalculatedVolume(Player1, VolumePlayer1.Value * Math.Pow((1 - ((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum))), 2));
                SetCalculatedVolume(Player2, VolumePlayer2.Value * Math.Pow(((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum)), 2));
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
                SetCalculatedVolume(Player1, e.NewValue * Math.Pow((1 - ((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum))), 2));
        }

        /// <summary>
        /// Volumes the player2_ value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void VolumePlayer2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Balance != null)
                SetCalculatedVolume(Player2, e.NewValue * Math.Pow(((Balance.Value - Balance.Minimum) / (Balance.Maximum - Balance.Minimum)), 2));
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

        /// <summary>
        /// Implemented Observer Method uploadedTracks has to be set by the current applicationdispatcher.
        /// Because it cannot be set from another thread than the UI-Thread
        /// </summary>
        /// <param name="db"></param>
        public void Update(string id)
        {
            var dbSession = Db.Instance.OpenSession();
            PlayRequest info = dbSession.Load<PlayRequest>(id);
            dbSession.Dispose();
            System.Windows.Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Normal,
                (Action)delegate()
                {
                    bool isFirstTrack = uploadedTracks.Count == 0;
                    
                    uploadedTracks.Add(info);
                    if (isFirstTrack)
                    {
                        QueueList.SelectedIndex = 0;
                    }
            });

        }

        private void ImportFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fdialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult res = fdialog.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(fdialog.SelectedPath))
            {
                DirectoryInfo dinfo = new DirectoryInfo(fdialog.SelectedPath);
                if(dinfo.Exists){
                    importer.ImportFolder(dinfo);
                }
            }
        }

        private void ImportFile_Click(object sender, RoutedEventArgs e)
        {            
            System.Windows.Forms.OpenFileDialog fdialog = new System.Windows.Forms.OpenFileDialog();
            fdialog.Multiselect = true;
            System.Windows.Forms.DialogResult res = fdialog.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK && fdialog.FileNames.Length != 0)
            {
                foreach (string filename in fdialog.FileNames)
                {
                    FileInfo finfo = new FileInfo(filename);
                    if (finfo.Exists)
                    {
                        importer.ImportMusic(finfo);
                    }
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            return;
        }

        protected virtual void Dispose(bool b)
        {
            if (b)
            {
                _server.Dispose();
                _player = null;
            }
        }
    }
}
