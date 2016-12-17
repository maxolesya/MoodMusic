using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoodMusic.Data;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace MoodMusic.UI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        IAudioService _audioService;
        IDialogWindow _dialog;
        EmotionGenreRhythmCombination combination;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>

        private List<Audio> _audioList;

        public List<Audio> AudioList
        {
            get { return _audioList; }
            set { Set(() => AudioList, ref _audioList, value); }
        }
        private string _tracknumber;

        public string TrackNumber
        {
            get { return _tracknumber; }
            set { Set(() => TrackNumber, ref _tracknumber, value); }
        }

        public RelayCommand ProcessPhoto { get; set; }
        public RelayCommand Loading { get; set; }
        private void WindowLoading()
        {
            Task t = new Task(BackgroundWorker);
            t.Start();
        }
        private void BackgroundWorker()
        {
            while (!Settings1.Default.auth)
            {
                Thread.Sleep(30);
            }
            _audioService.GetAudioList(Settings1.Default.id, Settings1.Default.token);
            AudioList = _audioService.AudioList;
           
        }
        private async void Process()
        {
            try
            {
                string path = _dialog.GetPath();
                using (var s = new FileStream(path, FileMode.Open))
                {
                    EmotionsTypes emotion = await PictureAnalyse.Compare(s);
                    AudioList = _audioService.AudioList.Where(mus => combination.GenreEmotionDictionary[PictureAnalyse.GetEmotion(emotion)].Contains(mus.genre)).ToList();
                    TrackNumber = "Треки: " + AudioList.Count.ToString();
                }
            }

            catch
            {
                // If the program ends up here check that you
                // have api key and engine id assigned
                // MessageBox.Show("Error occured", "Google Search");
            }
        }
        public MainViewModel(IDialogWindow dialog)
        {
            _dialog = dialog;
            _audioService = Factory.Default.GetService(Settings1.Default.vkmusic);
            Loading = new RelayCommand(WindowLoading);
            ProcessPhoto = new RelayCommand(Process);
            combination = new EmotionGenreRhythmCombination();
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}