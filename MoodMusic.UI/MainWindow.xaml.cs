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
        VKService vk;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            mediaPlayer.Volume = 1;


        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mediaPlayer.Source!=null) && mediaPlayer.NaturalDuration.HasTimeSpan &&!sliderIsCaptured)
            {
                sliderDurationProgress.Minimum = 0;
                sliderDurationProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliderDurationProgress.Value = mediaPlayer.Position.TotalSeconds;

            }
            if ((mediaPlayer.Source != null) && (sliderDurationProgress.Value == sliderDurationProgress.Maximum))
            {
                button_next_Click(this,null);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vk = new VKService();
            vk.onAudioListDownloaded += a => a.ForEach(item => Dispatcher.Invoke(()=>listBox.Items.Add(item)) );
            new Auth().Show();
            Task t = new Task(BackgroundWorker);
            t.Start();
        }
        private void BackgroundWorker()
        {
            while (!Settings1.Default.auth)
            {
                Thread.Sleep(30);
            }         
            vk.GetAudioList( Settings1.Default.id, Settings1.Default.token);
        }
        List<EmotionsTypes> emotionlist = new List<EmotionsTypes>();
        private async void LoadPicture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Image"; // Название файла по умолчанию
            dlg.DefaultExt = ".jpg"; // Расширение файла по умолчанию
            dlg.Filter = "Images (.jpg)|*.jpg"; // Фильтер по умолчанию
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                FileStream s = new FileStream(dlg.FileName, FileMode.Open);
                EmotionsTypes emotion = await PictureAnalyse.Compare(s);
                emotionlist.Add(emotion);
                dataGrid.ItemsSource = emotionlist;              
                MessageBox.Show(PictureAnalyse.GetEmotion(emotion).ToString());
                List<int> list = PictureAnalyse.genres_dictionary[PictureAnalyse.GetEmotion(emotion)];

                List<Audio> newlist = vk.AudioList.Where(mus => list.Contains(mus.genre)).ToList();
                listBox.Items.Clear();
                newlist.ForEach(mus => listBox.Items.Add(mus));

                s.Close();

            }
            var player = new MediaPlayer();
            player.MediaFailed += (s, e1) => MessageBox.Show("Error");
            player.Open(new Uri(vk.AudioList[1].url, UriKind.RelativeOrAbsolute));
            player.Play();
           

            //require - design - implem - verific - maintenance
            //test driven development
            //red green refactor

            //необходима фабрика и интерфейсы для анализа фотографии
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Stop();
            if (listBox.SelectedItem!=null )
            {
                mediaPlayer.Source = new Uri((listBox.SelectedItem as Audio).url);

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
            if (listBox.SelectedIndex!=listBox.Items.Count-1)
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
                    mediaPlayer.Play();
                    mediaPlayerIsPlaying = true;
                    mediaPlayerIsPaused = false;
                }
                else
                {
                    mediaPlayer.Pause();
                    mediaPlayerIsPaused = true;
                }
            }
        }

        private void button_prev_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex == 0)
            {
                listBox.SelectedIndex = listBox.Items.Count-1;
            }
            else
            {
                listBox.SelectedIndex = currentIndex-1;
            }
            listBox_MouseDoubleClick(this, null);
        }
    }
}
