using System;
using System.IO;
using System.Windows.Forms;

namespace Custom_UEVR_Injector
{
    public static class LogSyncManager
    {
        private static readonly string _logFilePath = Path.Combine(
            Path.GetDirectoryName(Application.ExecutablePath),
            "Custom_UEVR_Injector.txt"
        );
        private static string _lastText;

        public static void SyncLog(TextBox listResults)
        {
            var current = listResults.Text;
            if (current != _lastText)
            {
                File.WriteAllText(_logFilePath, current);
                _lastText = current;
            }
        }
    }
}
