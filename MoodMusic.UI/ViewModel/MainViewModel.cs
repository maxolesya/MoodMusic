using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoodMusic.Data;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using System.Windows;

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
        public event Action<List<Audio>> onAudioListDownloaded;
        public static event Action onListBoxCleared;

        private List<Audio> _audioList;

        public List<Audio> AudioListMVVM
        {
            get { return _audioList; }
            set { Set(() => AudioListMVVM, ref _audioList, value); }
        }
        private string _tracknumber;

        public string TrackNumber
        {
            get { return _tracknumber; }
            set { Set(() => TrackNumber, ref _tracknumber, value); }
        }

        public RelayCommand ProcessPhoto { get; set; }
        public RelayCommand Loading { get; set; }
        public void WindowLoading()
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
            AudioListMVVM = _audioService.AudioList;
            onAudioListDownloaded?.Invoke(AudioListMVVM);

        }
        private async void Process()
        {
            try
            {
                string path = _dialog.GetPath();
                using (var s = new FileStream(path, FileMode.Open))
                {
                    EmotionsTypes emotion = await PictureAnalyse.Compare(s);
                    onListBoxCleared?.Invoke();
                    AudioListMVVM = _audioService.AudioList.Where(mus => combination.GenreEmotionDictionary[PictureAnalyse.GetEmotion(emotion)].Contains(mus.genre)).ToList();                   
                    TrackNumber =PictureAnalyse.GetEmotion(emotion).ToString()+" "+"Треки: " + AudioListMVVM.Count.ToString();
                }
            }

            catch
            {
                 MessageBox.Show("Something is wrong with your picture, try to download another one!");
            }
        }
        public MainViewModel(IDialogWindow dialog)
        {
            _dialog = dialog;
            _audioService = Factory.Default.GetService(Settings1.Default.vkmusic);
            Loading = new RelayCommand(WindowLoading);
            ProcessPhoto = new RelayCommand(Process);
            combination = new EmotionGenreRhythmCombination();
        }
    }
}