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

        public MediaElement Active { get; private set; }
        public MediaElement InActive { get; private set; }

        public DoublePlayer(MediaElement player1, ProgressBar progress1, TextBlock remaining1, MediaElement p2, ProgressBar progress2, TextBlock remaining2)
        {
            _player1 = player1;
            _player1.MediaOpened += _player1_MediaOpened;
            _progress1 = progress1;
            _remaining1 = remaining1;
            _player2 = p2;
            _player2.MediaOpened += _player2_MediaOpened;
            _progress2 = progress2;
            _remaining2 = remaining2;

            // set the update timer for the progress bars
            _timerPlayer1.Interval = TimeSpan.FromMilliseconds(1000);
            _timerPlayer1.Tick += new EventHandler(TriggerUIRefresh);
            _timerPlayer1.Start();

            _player1.Volume = 1;
            _player2.Volume = 0;
            Active = _player1;
            InActive = _player2;
        }

        public void Play()
        {
        }

        public void Pause()
        {
        }

        public void Stop()
        {
        }

        public void TriggerUIRefresh(object sender, EventArgs e)
        {
            _progress1.Value = _player1.Position.TotalSeconds;
            if (_player1.HasAudio)
                _remaining1.Text = _player1.NaturalDuration.TimeSpan.Subtract(_player1.Position).ToString(_remainingTimeSpanFormat);

            _progress2.Value = _player2.Position.TotalSeconds;
            if (_player2.HasAudio)
                _remaining2.Text = _player2.NaturalDuration.TimeSpan.Subtract(_player2.Position).ToString(_remainingTimeSpanFormat);
        }

        private void _player1_MediaOpened(object sender, RoutedEventArgs e)
        {
            _positionPlayer1 = _player1.NaturalDuration.TimeSpan;
            _progress1.Minimum = 0;
            _progress1.Maximum = _positionPlayer1.TotalSeconds;

            _remaining1.Text = _player1.NaturalDuration.TimeSpan.ToString(_remainingTimeSpanFormat);
        }

        private void _player2_MediaOpened(object sender, RoutedEventArgs e)
        {
            _positionPlayer2 = _player2.NaturalDuration.TimeSpan;
            _progress2.Minimum = 0;
            _progress2.Maximum = _positionPlayer2.TotalSeconds;

            _remaining2.Text = _player2.NaturalDuration.TimeSpan.ToString(_remainingTimeSpanFormat);
        }
    }
}
