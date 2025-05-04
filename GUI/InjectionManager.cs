using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Custom_UEVR_Injector
{
    public static class InjectionManager
    {

        private static readonly HashSet<int> _injectedPids = new HashSet<int>();

        public static Process GetTargetProcess()
        {
            if (string.IsNullOrWhiteSpace(Functions.game_executable))
                return null;

            var name = Path.GetFileNameWithoutExtension(Functions.game_executable);
            return Process.GetProcessesByName(name).FirstOrDefault();
        }

        public static bool IsProcessRunning()
            => GetTargetProcess() != null;

        public static bool IsAlreadyInjected()
        {
            var p = GetTargetProcess();
            return p != null && _injectedPids.Contains(p.Id);
        }

        public static void MonitorProcess(main__form form)
        {

			if (!Functions.ProfileExists()) return;
            var p = GetTargetProcess();

            if (p == null || p.HasExited)
            {
				
                form.button_inject.Text = "Waiting for the game...";
                form.button_inject.Enabled = false;

                form.button_game_folder.Enabled = true;
                form.button_uevr_folder.Enabled = true;
                return;
            }

            if ($"{Functions.injector_config_data["custom_var_last_pid"]}" == $"{p.Id}")
            {
                _injectedPids.Add(p.Id);
            }

            bool injected = IsAlreadyInjected();
            if (injected)
            {
				
                form.button_inject.Text = "Done!";
                form.button_inject.Enabled = false;

                form.button_game_folder.Enabled = false;
                form.button_uevr_folder.Enabled = false;
            }
            else
            {
				
                form.button_inject.Text = "[Ready] Inject VR now?";
                form.button_inject.Enabled = true;

                form.button_game_folder.Enabled = true;
                form.button_uevr_folder.Enabled = true;
            }
        }

        public static void InjectAll(main__form form)
        {
            if (string.IsNullOrWhiteSpace(Functions.game_executable))
            {
                form.listResults.AppendText("ERROR Executable not defined." + Environment.NewLine);
                return;
            }
            if (string.IsNullOrWhiteSpace(Functions.uevr_folder) || !Directory.Exists(Functions.uevr_folder))
            {
                form.listResults.AppendText("ERROR Functions.uevr_folder invalid or does not exist." + Environment.NewLine);
                return;
            }
            if (Functions.DLL_files == null || Functions.DLL_files.Count == 0)
            {
                form.listResults.AppendText("ERROR No DLL configured for injection." + Environment.NewLine);
                return;
            }

            var proc = GetTargetProcess();
            if (proc == null)
            {
                form.listResults.AppendText($"ERROR Process not found: {Functions.game_executable}{Environment.NewLine}");
                return;
            }
            if (IsAlreadyInjected())
            {
                form.listResults.AppendText($"[LOG] Already injected into PID {proc.Id}.{Environment.NewLine}");
                return;
            }

            if ($"{Functions.injector_config_data["custom_var_last_pid"]}" == $"{proc.Id}")
            {
                _injectedPids.Add(proc.Id);
                form.listResults.AppendText($"[LOG] Already injected into PID {proc.Id}.{Environment.NewLine}");
                return;
            }

            try
            {
                foreach (var dllName in Functions.DLL_files)
                {
                    if (string.IsNullOrWhiteSpace(dllName))
                        continue;

                    string fullPath = Path.Combine(Functions.uevr_folder, dllName);
                    if (!File.Exists(fullPath))
                    {
                        form.listResults.AppendText($"[LOG] Skipping Missing Dll: {dllName}{Environment.NewLine}");
                        continue;
                    }

                    bool ok = InjectDll(proc, fullPath, form);
                    form.listResults.AppendText(
                        ok
                          ? $"[LOG] DLL Injected Successfully: {dllName}{Environment.NewLine}"
                          : $"ERROR Failed to inject: {dllName}{Environment.NewLine}"
                    );
                }

                _injectedPids.Add(proc.Id);

                LogSyncManager.SyncLog(form.listResults);

                Functions.injector_config_data["custom_var_last_pid"] = $"{proc.Id}";
                Functions.injector_config_update = true;
				
                form.button_inject.Text = "Done!";
                form.button_inject.Enabled = false;

                form.button_game_folder.Enabled = false;
                form.button_uevr_folder.Enabled = false;

                if ($"{Functions.injector_config_data["custom_var_auto_focus"]}" == "1")
                    Functions.FocusOnGame(Functions.game_executable);

                if ($"{Functions.injector_config_data["custom_var_auto_close"]}" == "1")
                    Functions.CloseApplicationDelayed();

            }
            catch (Exception ex)
            {
                form.listResults.AppendText($"[InjectAll ERROR] {ex.GetType().Name}: {ex.Message}{Environment.NewLine}");
            }
        }

        private static bool InjectDll(Process target, string dllPath, main__form form)
        {
            form.listResults.AppendText($"[InjectDll] PID={target.Id} DLL={Path.GetFileName(dllPath)}{Environment.NewLine}");
            const int PROCESS_ALL = 0x001F0FFF;
            IntPtr hProc = OpenProcess(PROCESS_ALL, false, target.Id);
            if (hProc == IntPtr.Zero)
            {
                form.listResults.AppendText("[InjectDll] OpenProcess failed" + Environment.NewLine);
                return false;
            }

            IntPtr addrLoad = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            if (addrLoad == IntPtr.Zero)
            {
                form.listResults.AppendText("[InjectDll] GetProcAddress failed" + Environment.NewLine);
                CloseHandle(hProc);
                return false;
            }

            byte[] data = Encoding.ASCII.GetBytes(dllPath + "\0");
            IntPtr mem = VirtualAllocEx(hProc, IntPtr.Zero, (uint)data.Length,
                                        AllocationType.Commit | AllocationType.Reserve,
                                        MemoryProtection.ReadWrite);
            if (mem == IntPtr.Zero)
            {
                form.listResults.AppendText("[InjectDll] VirtualAllocEx failed" + Environment.NewLine);
                CloseHandle(hProc);
                return false;
            }

            bool wrote = WriteProcessMemory(hProc, mem, data, data.Length, out _);
            if (!wrote)
            {
                form.listResults.AppendText("[InjectDll] WriteProcessMemory failed" + Environment.NewLine);
                CloseHandle(hProc);
                return false;
            }

            IntPtr hThread = CreateRemoteThread(hProc, IntPtr.Zero, 0, addrLoad, mem, 0, IntPtr.Zero);
            if (hThread == IntPtr.Zero)
            {
                form.listResults.AppendText("[InjectDll] CreateRemoteThread failed" + Environment.NewLine);
                CloseHandle(hProc);
                return false;
            }

            CloseHandle(hThread);
            CloseHandle(hProc);
            form.listResults.AppendText("[InjectDll] Injection successful" + Environment.NewLine);
            return true;
        }

        #region PInvoke

        [Flags]
        private enum AllocationType : uint
        {
            Commit = 0x1000,
            Reserve = 0x2000
        }

        [Flags]
        private enum MemoryProtection : uint
        {
            ReadWrite = 0x04
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr VirtualAllocEx(
            IntPtr hProcess, IntPtr lpAddress, uint dwSize,
            AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(
            IntPtr hProcess, IntPtr lpBaseAddress,
            byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateRemoteThread(
            IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize,
            IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags,
            IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        #endregion
    }
}
