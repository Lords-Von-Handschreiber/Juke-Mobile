using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Juke_Mobile_Core
{
    public class DoublePlayer
    {
        TimeSpan _positionPlayer1;
        TimeSpan _positionPlayer2;

        MediaElement _player1;
        ProgressBar _progress1;
        TextBlock _remaining1;

        MediaElement _player2;
        ProgressBar _progress2;
        TextBlock _remaining2;

        string _remainingTimeSpanFormat = @"\-m\:ss";

        DispatcherTimer _timerPlayer1 = new DispatcherTimer();

        /// <summary>
        /// Gets the active.
        /// </summary>
        /// <value>
        /// The active.
        /// </value>
        public MediaElement Active { get; private set; }
        /// <summary>
        /// Gets the inactive.
        /// </summary>
        /// <value>
        /// The inactive.
        /// </value>
        public MediaElement InActive { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoublePlayer" /> class.
        /// </summary>
        /// <param name="player1">The player1.</param>
        /// <param name="progress1">The progress1.</param>
        /// <param name="remaining1">The remaining1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="progress2">The progress2.</param>
        /// <param name="remaining2">The remaining2.</param>
        public DoublePlayer(MediaElement player1, ProgressBar progress1, TextBlock remaining1, MediaElement p2, ProgressBar progress2, TextBlock remaining2)
        {
            _player1 = player1;
            _player1.MediaOpened += _player1_MediaOpened;
            _player1.LoadedBehavior = MediaState.Manual;
            _player1.UnloadedBehavior = MediaState.Stop;
            _progress1 = progress1;
            _remaining1 = remaining1;
            _player2 = p2;
            _player2.MediaOpened += _player2_MediaOpened;
            _player2.LoadedBehavior = MediaState.Manual;
            _player2.UnloadedBehavior = MediaState.Stop;
            _progress2 = progress2;
            _remaining2 = remaining2;

            // set the update timer for the progress bars
            _timerPlayer1.Interval = TimeSpan.FromMilliseconds(1000);
            _timerPlayer1.Tick += new EventHandler(TriggerUIRefresh);
            _timerPlayer1.Start();

            Active = _player1;
            InActive = _player2;
        }

        /// <summary>
        /// Plays this instance.
        /// </summary>
        public void Play()
        {
            // faking 1st load from query
            Load(new Uri(@"C:\Users\Thomas\Music\01 - Sonnentanz (Original Version).mp3"));

            Active.Play();
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            Active.Pause();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            Active.Stop();
        }

        /// <summary>
        /// Triggers the UI refresh.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void TriggerUIRefresh(object sender, EventArgs e)
        {
            _progress1.Value = _player1.Position.TotalSeconds;
            if (_player1.HasAudio)
                _remaining1.Text = _player1.NaturalDuration.TimeSpan.Subtract(_player1.Position).ToString(_remainingTimeSpanFormat);

            _progress2.Value = _player2.Position.TotalSeconds;
            if (_player2.HasAudio)
                _remaining2.Text = _player2.NaturalDuration.TimeSpan.Subtract(_player2.Position).ToString(_remainingTimeSpanFormat);
        }

        /// <summary>
        /// Handles the MediaOpened event of the _player1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void _player1_MediaOpened(object sender, RoutedEventArgs e)
        {
            _positionPlayer1 = _player1.NaturalDuration.TimeSpan;
            _progress1.Minimum = 0;
            _progress1.Maximum = _positionPlayer1.TotalSeconds;

            _remaining1.Text = _positionPlayer1.ToString(_remainingTimeSpanFormat);
        }

        /// <summary>
        /// Handles the MediaOpened event of the _player2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void _player2_MediaOpened(object sender, RoutedEventArgs e)
        {
            _positionPlayer2 = _player2.NaturalDuration.TimeSpan;
            _progress2.Minimum = 0;
            _progress2.Maximum = _positionPlayer2.TotalSeconds;

            _remaining2.Text = _player2.NaturalDuration.TimeSpan.ToString(_remainingTimeSpanFormat);
        }

        /// <summary>
        /// Loads the specified track.
        /// </summary>
        /// <param name="track">The track.</param>
        public void Load(Uri track)
        {
            Active.Source = track;
        }
    }
}
