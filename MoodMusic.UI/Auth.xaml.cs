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
using System.Windows.Shapes;

namespace MoodMusic.UI
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        bool firstAuth = false;

        public Auth()
        {
            InitializeComponent();
        }
        private void Loaded_Window(object sender, RoutedEventArgs e)
        {
            webBrowser.Navigate("https://oauth.vk.com/authorize?client_id=5756309&scope=audio&redirect_uri=https://oauth.vk.com/blank.html&display=popup&response_type=token&v=5.60");

        }

        private void webBrowser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            webBrowser.ToolTip = "Загрузка";
        }

        private void webBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                if (!Settings1.Default.auth)
                {
                    string url = webBrowser.Source.ToString();
                    MessageBox.Show(url);
                    string l = url.Split(new char[] { '#' })[1];
                    if (l[0].Equals('a'))
                    {
                        Settings1.Default.auth = true;
                        Settings1.Default.token = l.Split('&')[0].Split('=')[1];
                        Settings1.Default.id = l.Split('=')[3];
                        MessageBox.Show("OK");

                       // webBrowser.Navigate("https://vk.com/id" + Settings1.Default.id);
                    }
                }
                else
                {

                }
                Close();

            }
            catch (Exception e1)
            {

                MessageBox.Show(e1 + " Проверьте соединение с интернетом");
            }

            // this.Close();
        }
    }
}
