using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.UI
{
    public interface IDialogWindow
    {
        string GetPath();
    }
    public class DialogWindow : IDialogWindow
    {
        public string GetPath()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Image"; // Название файла по умолчанию
            dlg.DefaultExt = ".jpg"; // Расширение файла по умолчанию
            dlg.Filter = "Images (.jpg)|*.jpg"; // Фильтер по умолчанию
            bool? result = dlg.ShowDialog();

            if (result == true) { return dlg.FileName; }
            else
            {
                return null;
            }
        }


    }
}
