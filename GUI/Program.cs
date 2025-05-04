using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Custom_UEVR_Injector
{
    static class Program
    {
        private const string MutexName = "Custom_UEVR_Mutex";

        [STAThread]
        static void Main()
        {
            bool createdNew;
            using (var mutex = new Mutex(true, MutexName, out createdNew))
            {
                if (!createdNew)
                {
                    FocusExistingInstance();
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new main__form());
            }
        }

        private static void FocusExistingInstance()
        {
            var me = Process.GetCurrentProcess();
            var other = Process
                .GetProcessesByName(me.ProcessName)
                .FirstOrDefault(p => p.Id != me.Id);

            if (other == null)
                return;

            IntPtr handle = other.MainWindowHandle;
            if (handle == IntPtr.Zero)
                return;

            ShowWindowAsync(handle, SW_RESTORE);
            SetForegroundWindow(handle);
        }

        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
    }
}
