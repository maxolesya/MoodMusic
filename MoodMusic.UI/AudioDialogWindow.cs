using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms = System.Windows.Forms;

namespace MoodMusic.UI
{
    public class AudioDialogWindow : IDialogWindow
    {
        public string GetPath()
        {
            WinForms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;

            if (dialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                if (Directory.Exists(dialog.SelectedPath))
                    return dialog.SelectedPath;
                else
                    return Environment.CurrentDirectory + "\\DefaultMusic";
            }
            else
                return Environment.CurrentDirectory + "\\DefaultMusic";
        }
    }
}
