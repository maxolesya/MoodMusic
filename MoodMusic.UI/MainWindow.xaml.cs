using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoodMusic.Data;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Reflection;
using System.Media;
using System.Windows.Threading;
using System.Windows.Interactivity;
using MoodMusic.UI.ViewModel;

namespace MoodMusic.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int currentIndex = 0;
        bool sliderIsCaptured = false;
        bool mediaPlayerIsPlaying = false;
        bool mediaPlayerIsPaused = false;
        ImageBrush content = new ImageBrush();
        IAudioService vk;
        EmotionGenreRhythmCombination combination;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            mediaPlayer.Volume = 1;
            combination = new EmotionGenreRhythmCombination();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mediaPlayer.Source != null) && mediaPlayer.NaturalDuration.HasTimeSpan && !sliderIsCaptured)
            {
                sliderDurationProgress.Minimum = 0;
                sliderDurationProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliderDurationProgress.Value = mediaPlayer.Position.TotalSeconds;

            }
            if ((mediaPlayer.Source != null) && (sliderDurationProgress.Value == sliderDurationProgress.Maximum))
            {
                button_next_Click(this, null);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e1)
        {

            MainViewModel m = new MainViewModel(new DialogWindow());
            m.onAudioListDownloaded += a => a.ForEach(item => Dispatcher.Invoke(() => listBox.Items.Add(item)));
            m.WindowLoading();
        }
        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Stop();

            if (listBox.SelectedItem != null)
            {

                mediaPlayer.Source = new Uri((listBox.SelectedItem as Audio).url);
                content.ImageSource = new BitmapImage(new Uri(@"C:\Олеся\Visual Studio\MoodMusic\MoodMusic.UI\Icons\pause.png"));
                button_play_pause.Background = content;
                mediaPlayer.Play();
                mediaPlayerIsPlaying = true;
                currentIndex = listBox.SelectedIndex;
            }

        }

        private void sliderDurationProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            durationProgressStatus.Text = TimeSpan.FromSeconds(sliderDurationProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void sliderDurationProgress_DragStarted(object sender, RoutedEventArgs e)
        {
            sliderIsCaptured = true;
        }

        private void sliderDurationProgress_DragCompleted(object sender, RoutedEventArgs e)
        {
            sliderIsCaptured = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(sliderDurationProgress.Value);
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != listBox.Items.Count - 1)
            {
                listBox.SelectedIndex = currentIndex + 1;
            }
            else
            {
                listBox.SelectedIndex = 0;
            }
            listBox_MouseDoubleClick(this, null);
        }

        private void button_play_pause_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayerIsPlaying)
            {
                if (mediaPlayerIsPaused)
                {

                    content.ImageSource = new BitmapImage(new Uri(@"C:\Олеся\Visual Studio\MoodMusic\MoodMusic.UI\Icons\pause.png"));
                    mediaPlayer.Play();
                    button_play_pause.Background = content;
                    mediaPlayerIsPlaying = true;
                    mediaPlayerIsPaused = false;
                }
                else
                {
                    content.ImageSource = new BitmapImage(new Uri(@"C:\Олеся\Visual Studio\MoodMusic\MoodMusic.UI\Icons\play.png"));
                    button_play_pause.Background = content;
                    mediaPlayer.Pause();
                    mediaPlayerIsPaused = true;
                }
            }
        }

        private void button_prev_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex == 0)
            {
                listBox.SelectedIndex = listBox.Items.Count - 1;
            }
            else
            {
                listBox.SelectedIndex = currentIndex - 1;
            }
            listBox_MouseDoubleClick(this, null);
        }


    }
}
