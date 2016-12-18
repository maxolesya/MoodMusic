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
    /// Логика взаимодействия для OpenWindow.xaml
    /// </summary>
    public partial class OpenWindow : Window
    {
        IDialogWindow dialog;

        public OpenWindow()
        {
            InitializeComponent();
        }

        private void button_open_window_ok_Click(object sender, RoutedEventArgs e)
        {
            if (checkbox_memory.IsChecked == true)
            {
                Settings1.Default.auth = true;
                Settings1.Default.datamusic = true;
                dialog = new AudioDialogWindow();
                Settings1.Default.token = dialog.GetPath();
                MainWindow m = new MainWindow();
                m.Show();
            }
            if (checkbox_vk.IsChecked == true)
            {
                Settings1.Default.vkmusic = true;
                HintWindow h = new HintWindow();
                h.Show();
            }
            Close();
        }

        private void checkbox_vk_Checked(object sender, RoutedEventArgs e)
        {
            if (checkbox_memory.IsChecked == false)
            {
                button_open_window_ok.IsEnabled = true;
            }
            else
            {
                button_open_window_ok.IsEnabled = false;
            }
        }

        private void checkbox_vk_Unchecked(object sender, RoutedEventArgs e)
        {
            if (checkbox_memory.IsChecked == false)
            {
                button_open_window_ok.IsEnabled = false;
            }
            else
            {
                button_open_window_ok.IsEnabled = true;
            }
        }

        private void checkbox_memory_Unchecked(object sender, RoutedEventArgs e)
        {
            if (checkbox_vk.IsChecked == false)
            {
                button_open_window_ok.IsEnabled = false;
            }
            else
            {
                button_open_window_ok.IsEnabled = true;
            }
        }

        private void checkbox_memory_Checked(object sender, RoutedEventArgs e)
        {
            if (checkbox_vk.IsChecked == false)
            {
                button_open_window_ok.IsEnabled = true;
            }
            else
            {
                button_open_window_ok.IsEnabled = false;
            }
        }
    }
}
