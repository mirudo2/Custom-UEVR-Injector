using System;
using System.IO;
using System.Windows.Forms;

namespace Custom_UEVR_Injector
{
    public static class ExeFinder
    {

        public static string game_exe_finder(string folderPath, main__form form)
        {

            try
            {
                string[] files = Directory.GetFiles(folderPath, "*Win64-Shipping.exe", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    return Path.GetFileName(file);
                }

                if (files.Length == 0)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
				form.listResults.AppendText($"[ERROR] {ex.Message}{Environment.NewLine}");
            }
            return null;
        }

        public static string uevr_folder_finder(string folderPath, main__form form)
        {

            try
            {
                string[] files = Directory.GetFiles(folderPath, "UEVRBackend.dll", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    return Path.GetDirectoryName(file);
                }

                if (files.Length == 0)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                form.listResults.AppendText($"[ERROR] {ex.Message}{Environment.NewLine}");
            }
            return null;
        }

    }
}
