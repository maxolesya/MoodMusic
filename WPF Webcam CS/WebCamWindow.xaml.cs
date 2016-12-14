using System.Windows;
using System.Windows.Data;
using Microsoft.Expression.Encoder.Devices;
using WebcamControl;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace WPF_Webcam_CS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string imagePath;
        DirectoryInfo di;
        public MainWindow()
        {
            InitializeComponent();
            Binding binding_1 = new Binding("SelectedValue");
            binding_1.Source = VideoDevicesComboBox;
            WebcamCtrl.SetBinding(Webcam.VideoDeviceProperty, binding_1);

            // Create directory for saving image files
            string imagePath = @"C:\WebcamSnapshots";

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);   
            }
            di = new DirectoryInfo(imagePath);
            // Set some properties of the Webcam control
            WebcamCtrl.ImageDirectory = imagePath;
            WebcamCtrl.FrameRate = 30;
            WebcamCtrl.FrameSize = new System.Drawing.Size(640, 480);

            // Find available a/v devices
            var vidDevices = EncoderDevices.FindDevices(EncoderDeviceType.Video);
            VideoDevicesComboBox.ItemsSource = vidDevices;
            VideoDevicesComboBox.SelectedIndex = 0;
        }

        private void StartCaptureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Display webcam video
                WebcamCtrl.StartPreview();
            }
            catch (Microsoft.Expression.Encoder.SystemErrorException ex)
            {
                MessageBox.Show("Device is in use by another application");
            }
        }

        private void StopCaptureButton_Click(object sender, RoutedEventArgs e)
        {
            // Stop the display of webcam video.
            WebcamCtrl.StopPreview();
        }

        private void TakeSnapshotButton_Click(object sender, RoutedEventArgs e)
        {
            // Take snapshot of webcam video.
            Array.ForEach(Directory.GetFiles(@"C:\WebcamSnapshots\"), File.Delete);
            WebcamCtrl.TakeSnapshot();
            Image img = Image.FromFile(di.GetFiles()[0].ToString());
            WebcamCtrl.Content = img;
        }
    }
}
