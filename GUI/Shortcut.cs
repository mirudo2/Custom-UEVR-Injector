using System;
using System.IO;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace Custom_UEVR_Injector
{
    public static class Shortcut
    {
        public static void create_Shortcut(string game_exe, main__form form)
        {
			if (!Functions.ProfileExists()) return;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Windows Shortcut (*.lnk)|*.lnk";
                sfd.Title = "Save an injector shortcut for this game...";
                sfd.FileName = Path.GetFileNameWithoutExtension(game_exe) + ".lnk";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    var shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(sfd.FileName);

                    shortcut.TargetPath = Application.ExecutablePath;
                    shortcut.Arguments = game_exe;
                    shortcut.WorkingDirectory = Application.StartupPath;
                    shortcut.WindowStyle = 1;
                    shortcut.Description = game_exe;
                    shortcut.IconLocation = Application.ExecutablePath + ",0";
                    shortcut.Save();

                    form.listResults.AppendText("[LOG] Shortcut created in: " + sfd.FileName + Environment.NewLine);

                }
                catch (Exception ex)
                {
                    form.listResults.AppendText("ERROR: Failed to create shortcut.");
                    string error_msg = ex.Message;
                }
            }
        }
    }
}
