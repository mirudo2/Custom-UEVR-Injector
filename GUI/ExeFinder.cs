using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Custom_UEVR_Injector
{
    public static class ExeFinder
    {

        public static string game_exe_finder(string folderPath, main__form form)
        {
            try
            {
                string[] files = Directory.GetFiles(folderPath, "*Win64-Shipping.exe", SearchOption.AllDirectories);
                if (files.Length > 0)
                    return Path.GetFileName(files[0]);

                var win64Dirs = Directory
                    .EnumerateDirectories(folderPath, "Win64", SearchOption.AllDirectories)
                    .Where(d =>
                        string.Equals(Path.GetFileName(Path.GetDirectoryName(d)),
                                      "Binaries",
                                      StringComparison.OrdinalIgnoreCase));

                var blacklist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "UnrealCEFSubProcess.exe",
            "CrashReportClient.exe",
            "UE4PrereqSetup_x64.exe",
            "DXSETUP.exe",
            "VC_redist.x64.exe",
            "VC_redist.x86.exe",
            "uninst.exe",
            "binary_patch.exe",
            "CrasheyeReport64.exe"
        };

                string bestExeName = null;
                long bestSize = 0;

                foreach (var dir in win64Dirs)
                {
                    var exes = Directory.GetFiles(dir, "*.exe", SearchOption.TopDirectoryOnly);
                    foreach (var fullPath in exes)
                    {
                        var name = Path.GetFileName(fullPath);
                        if (blacklist.Contains(name))
                            continue;

                        long size = new FileInfo(fullPath).Length;
                        if (size > bestSize)
                        {
                            bestSize = size;
                            bestExeName = name;
                        }
                    }
                }

                return bestExeName;
            }
            catch (Exception ex)
            {
                form.listResults.AppendText($"[ERROR] {ex.Message}{Environment.NewLine}");
                return null;
            }
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
