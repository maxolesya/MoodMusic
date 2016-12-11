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

namespace MoodMusic.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

            WebRequest webRequest = WebRequest.Create("https://api.vk.com/method/audio.get?owner_id=" + Settings1.Default.id + "&need_user=0&access_token=" + Settings1.Default.token);
            WebResponse response = webRequest.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(dataStream);
            string serverResponce = sr.ReadToEnd();
            // Dispatcher.Invoke(() => listBox.Items.Add(serverResponce));
            sr.Close();
            response.Close();
            serverResponce = HttpUtility.HtmlDecode(serverResponce);
            JToken token = JToken.Parse(serverResponce);
            List<Audio> audiolist = token["response"].Children().Skip(1).Select(c => c.ToObject<Audio>()).ToList();
            Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < audiolist.Count; i++)
                {
                    listBox.Items.Add(audiolist[i].ToString());
                }
            });

        }

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
                MessageBox.Show(emotion.ToString());
                MessageBox.Show(PictureAnalyse.GetEmotion(emotion).ToString());
                s.Close();

            }//require - design - implem - verific - maintenance
            //test driven development
            //red green refactor

            //необходима фабрика и интерфейсы для анализа фотографии
        }
    }
}
