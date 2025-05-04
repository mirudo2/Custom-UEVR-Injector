using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Custom_UEVR_Injector
{
    public static class Functions
    {

        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        public static bool injector_config_update = false;
        public static bool config_txt_update = false;
        public static bool cvars_standard_update = false;
        public static bool user_script_update = false;

        public static string game_executable = null;
        public static string profile_folder = null;
        public static string profiles_path = null;
        public static string profile_path = null;
        public static string uevr_folder = null;

        public static Dictionary<string, object> injector_config_data { get; }
            = new Dictionary<string, object>();

        public static Dictionary<string, object> config_txt_data { get; }
            = new Dictionary<string, object>();

        public static Dictionary<string, object> cvars_standard_data { get; }
            = new Dictionary<string, object>();

        public static Dictionary<string, object> user_script_data { get; }
            = new Dictionary<string, object>();

        public static List<string> DLL_files = new List<string>();

        public static bool ProfileExists() {

            return game_executable != null && profile_folder != null && profiles_path != null && profile_path != null;
        }
		
        public static bool UEVRFolderExists() {

            return uevr_folder != null && game_executable != null;
        }

        public static void CloseApplication()
        {
            if (Application.OpenForms.Count > 0)
            {
                Application.Exit();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public static async void CloseApplicationDelayed()
        {
            await Task.Delay(2000);
            CloseApplication();
        }

        public static bool FocusOnGame(string processName)
        {
            if (string.IsNullOrWhiteSpace(processName))
                return false;

            processName = processName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
                ? processName.Substring(0, processName.Length - 4)
                : processName;

            var proc = Process.GetProcessesByName(processName).FirstOrDefault();
            if (proc == null || proc.HasExited)
                return false;

            IntPtr handle = proc.MainWindowHandle;
            if (handle == IntPtr.Zero)
                return false;

            ShowWindowAsync(handle, SW_RESTORE);
            return SetForegroundWindow(handle);
        }

        public static void CreateCheckProfile(string executable, main__form form)
        {
			
            if (string.IsNullOrWhiteSpace(executable))
            {
                form.listResults.AppendText("ERROR: Folder name cannot be null or empty." + Environment.NewLine);
                return;
            }

            profiles_path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "UnrealVRMod"
            );

			profile_folder = Path.GetFileNameWithoutExtension(executable);
            profile_path = Path.Combine(profiles_path, profile_folder);

            form.listResults.AppendText("[LOG] Profile Path: " + profile_path + Environment.NewLine);
            form.listResults.AppendText("[LOG] Profile Folder: " + profile_folder + Environment.NewLine);
			
			string configFilePath = null;

            try
            {
                if (!Directory.Exists(profiles_path))
                {
                    Directory.CreateDirectory(profiles_path);
                    form.listResults.AppendText("[LOG] UnrealVRMod Folder Created." + Environment.NewLine);
                }

                if (!Directory.Exists(profile_path))
                {
                    Directory.CreateDirectory(profile_path);
                    form.listResults.AppendText("[LOG] Profile Folder Created." + Environment.NewLine);
                }

                configFilePath = Path.Combine(profile_path, "injector_config.txt");

                if (!File.Exists(configFilePath))
                {
                    File.WriteAllText(configFilePath, "");
                    form.listResults.AppendText("[LOG] injector_config.txt Created." + Environment.NewLine);
                }

                configFilePath = Path.Combine(profile_path, "config.txt");

                if (!File.Exists(configFilePath))
                {
                    File.WriteAllText(configFilePath, "");
                    form.listResults.AppendText("[LOG] config.txt Created." + Environment.NewLine);
                }

                configFilePath = Path.Combine(profile_path, "cvars_standard.txt");

                if (!File.Exists(configFilePath))
                {
                    File.WriteAllText(configFilePath, "");
                    form.listResults.AppendText("[LOG] cvars_standard.txt Created." + Environment.NewLine);
                }

                configFilePath = Path.Combine(profile_path, "user_script.txt");

                if (!File.Exists(configFilePath))
                {
                    File.WriteAllText(configFilePath, "");
                    form.listResults.AppendText("[LOG] user_script.txt Created." + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                form.listResults.AppendText(
                        $"ERROR: Failed to set directory/file. {ex.Message}:{Environment.NewLine}"
                    );
            }
        }

        public static void GetAllConfig()
        {
            if (string.IsNullOrWhiteSpace(profile_path)) return;
			string configPath = null;

			/* injector_config.txt */
            configPath = Path.Combine(profile_path,"injector_config.txt");
			if (!File.Exists(configPath)) return;

                foreach (var line in File.ReadLines(configPath))
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        injector_config_data[parts[0].Trim()] = parts[1].Trim();
                    }
                }
				
			/* config.txt */
            configPath = Path.Combine(profile_path,"config.txt");
			if (!File.Exists(configPath)) return;

                foreach (var line in File.ReadLines(configPath))
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        config_txt_data[parts[0].Trim()] = parts[1].Trim();
                    }
                }
				
				
			/* cvars_standard.txt */
            configPath = Path.Combine(profile_path,"cvars_standard.txt");
			if (!File.Exists(configPath)) return;

                foreach (var line in File.ReadLines(configPath))
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        cvars_standard_data[parts[0].Trim()] = parts[1].Trim();
                    }
                }
				
			/* user_script.txt */
            configPath = Path.Combine(profile_path,"user_script.txt");
			if (!File.Exists(configPath)) return;

                foreach (var line in File.ReadLines(configPath))
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length == 2)
                    {
                        user_script_data[parts[0].Trim()] = parts[1].Trim();
                    }
                }
					
        }

        public static void Sort_DLLs(List<string> DLL_files)
        {
            if (DLL_files == null) return;

            const string nullifier = "UEVRPluginNullifier.dll";
            const string backend = "UEVRBackend.dll";

            bool hasNullifier = DLL_files.Remove(nullifier);
            bool hasBackend = DLL_files.Remove(backend);

            if (hasNullifier)
                DLL_files.Insert(0, nullifier);

            if (hasBackend)
                DLL_files.Add(backend);
        }

        public static void AddDLL(string dllName)
        {
            if (string.IsNullOrWhiteSpace(dllName))
                return;
            if (!DLL_files.Contains(dllName))
                DLL_files.Add(dllName);
        }

        public static bool TryGetString(IDictionary<string, object> dict, string key, out string value)
        {
            if (dict.TryGetValue(key, out var obj) && obj != null)
            {
                value = obj.ToString();
                return true;
            }
            value = null;
            return false;
        }

        public static void UpdateInterfaceOptions(main__form form)
        {
			
            /* injector_config.txt */

            if (TryGetString(injector_config_data, "custom_var_runtime", out var v_runtime))
            {
                switch (v_runtime)
                {
                    case "0": form.radioButtonXR.Checked = true; break;
                    case "1": form.radioButtonVR.Checked = true; break;
                }
            }

            if (TryGetString(injector_config_data, "custom_var_nullify", out var v_nullify))
            {
                form.checkBoxNullify.Checked = (v_nullify == "1");
            }

            if (TryGetString(injector_config_data, "custom_var_auto_inject", out var v_auto_inject))
            {
                form.checkBoxAutoInject.Checked = (v_auto_inject == "1");
            }

            if (TryGetString(injector_config_data, "custom_var_auto_focus", out var v_auto_focus))
            {
                form.checkBoxFocusGame.Checked = (v_auto_focus == "1");
            }
			
            if (TryGetString(injector_config_data, "custom_var_auto_close", out var v_auto_close))
            {
                form.checkBox_auto_close.Checked = (v_auto_close == "1");
            }

            if (TryGetString(injector_config_data, "custom_var_urvr_folder", out var v_urvr_folder))
            {
                if (v_urvr_folder != "null")
                {
                    form.button_uevr_folder.Text = v_urvr_folder;
                    uevr_folder = v_urvr_folder;
                } else
                {
                    uevr_folder = null;
                    form.button_uevr_folder.Text = "Select the UEVR folder";
                }
            }

            /* config.txt */

            if (TryGetString(config_txt_data, "VR_RenderingMethod", out var v_RenderingMethod))
            {
                switch (v_RenderingMethod)
                {
                    case "0": form.radioButtonNative.Checked = true; break;
                    case "1": form.radioButtonSync.Checked = true; break;
                    case "2": form.radioButtonAlt.Checked = true; break;
                }
            }

            if (TryGetString(config_txt_data, "VR_ShowFPSOverlay", out var v_show_fps))
            {
                form.checkBoxFPS.Checked = (v_show_fps.Equals("true", StringComparison.OrdinalIgnoreCase));
            }

            /* cvars_standard.txt */

            if (TryGetString(cvars_standard_data, "Core_r.ScreenPercentage", out var slide_ScreenPercentage)
             && float.TryParse(slide_ScreenPercentage, NumberStyles.Float, CultureInfo.InvariantCulture, out var spValue))
            {
                Slides.ScreenPercentage.Slider.Value = spValue;
            }

            /* user_script.txt */

            if (TryGetString(user_script_data, "sg.ResolutionQuality", out var slide_ResolutionQuality)
             && float.TryParse(slide_ResolutionQuality, NumberStyles.Float, CultureInfo.InvariantCulture, out var rqValue))
            {
                Slides.ResolutionQuality.Slider.Value = rqValue;
            }

            if (TryGetString(user_script_data, "sg.ViewDistanceQuality", out var slide_ViewDistanceQuality)
             && int.TryParse(slide_ViewDistanceQuality, out var vdqValue))
            {
                Slides.ViewDistanceQuality.Slider.Value = vdqValue;
            }

            if (TryGetString(user_script_data, "sg.AntiAliasingQuality", out var slide_AntiAliasingQuality)
                && int.TryParse(slide_AntiAliasingQuality, out var aaValue))
            {
                Slides.AntiAliasingQuality.Slider.Value = aaValue;
            }

            if (TryGetString(user_script_data, "sg.PostProcessQuality", out var slide_PostProcessQuality)
                && int.TryParse(slide_PostProcessQuality, out var ppValue))
            {
                Slides.PostProcessQuality.Slider.Value = ppValue;
            }

            if (TryGetString(user_script_data, "sg.ShadowQuality", out var slide_ShadowQuality)
                && int.TryParse(slide_ShadowQuality, out var shValue))
            {
                Slides.ShadowQuality.Slider.Value = shValue;
            }

            if (TryGetString(user_script_data, "sg.TextureQuality", out var slide_TextureQuality)
                && int.TryParse(slide_TextureQuality, out var txValue))
            {
                Slides.TextureQuality.Slider.Value = txValue;
            }

            if (TryGetString(user_script_data, "sg.EffectsQuality", out var slide_EffectsQuality)
                && int.TryParse(slide_EffectsQuality, out var efValue))
            {
                Slides.EffectsQuality.Slider.Value = efValue;
            }

            if (TryGetString(user_script_data, "sg.FoliageQuality", out var slide_FoliageQuality)
                && int.TryParse(slide_FoliageQuality, out var flValue))
            {
                Slides.FoliageQuality.Slider.Value = flValue;
            }

            if (TryGetString(user_script_data, "sg.ShadingQuality", out var slide_ShadingQuality)
                && int.TryParse(slide_ShadingQuality, out var sdValue))
            {
                Slides.ShadingQuality.Slider.Value = sdValue;
            }

            if (TryGetString(user_script_data, "sg.ReflectionQuality", out var slide_ReflectionQuality)
                && int.TryParse(slide_ReflectionQuality, out var rqValue2))
            {
                Slides.ReflectionQuality.Slider.Value = rqValue2;
            }

            if (TryGetString(user_script_data, "r.VSync", out var slide_VSync)
                && int.TryParse(slide_VSync, out var vsValue))
            {
                Slides.VSync.Slider.Value = vsValue;
            }

            if (TryGetString(user_script_data, "r.VolumetricCloud", out var slide_VolumetricCloud)
                && int.TryParse(slide_VolumetricCloud, out var vcValue))
            {
                Slides.VolumetricCloud.Slider.Value = vcValue;
            }

            if (TryGetString(user_script_data, "sg.GlobalIlluminationQuality", out var slide_GlobalIlluminationQuality)
                && int.TryParse(slide_GlobalIlluminationQuality, out var giValue))
            {
                Slides.GlobalIlluminationQuality.Slider.Value = giValue;
            }

            if (TryGetString(user_script_data, "r.ReflectionMethod", out var slide_ReflectionMethod)
                && int.TryParse(slide_ReflectionMethod, out var rmValue))
            {
                Slides.ReflectionMethod.Slider.Value = rmValue;
            }

        }

        public static string GetValidArguments()
        {
            string[] args = Environment.GetCommandLineArgs().Skip(1).ToArray();

            if (args.Length != 1) return null;

            string argument = args[0];

            if (!argument.EndsWith("Win64-Shipping.exe", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return argument.Trim();
        }

        public static void ResetConfigArrays()
        {
            injector_config_data.Clear();
            config_txt_data.Clear();
            user_script_data.Clear();
            cvars_standard_data.Clear();
            DLL_files.Clear();

            injector_config_data["custom_var_runtime"] = "0";
            injector_config_data["custom_var_nullify"] = "0";
            injector_config_data["custom_var_auto_inject"] = "0";
            injector_config_data["custom_var_auto_focus"] = "1";
            injector_config_data["custom_var_auto_close"] = "0";
            injector_config_data["custom_var_urvr_folder"] = "null";
            injector_config_data["custom_var_last_pid"] = "null";

            DLL_files.Add("openxr_loader.dll");
            DLL_files.Add("LuaVR.dll");
            DLL_files.Add("UEVRBackend.dll");

            config_txt_data["VR_RenderingMethod"] = "0";
            config_txt_data["VR_ShowFPSOverlay"] = "true";

            cvars_standard_data["Core_r.ScreenPercentage"] = "99.99";

            user_script_data["sg.ResolutionQuality"] = "99.99";
            user_script_data["sg.ViewDistanceQuality"] = "2";
            user_script_data["sg.AntiAliasingQuality"] = "2";
            user_script_data["sg.PostProcessQuality"] = "2";
            user_script_data["sg.ShadowQuality"] = "2";
            user_script_data["sg.TextureQuality"] = "2";
            user_script_data["sg.EffectsQuality"] = "2";
            user_script_data["sg.FoliageQuality"] = "2";
            user_script_data["sg.ShadingQuality"] = "2";
            user_script_data["sg.ReflectionQuality"] = "2";
            user_script_data["r.VSync"] = "1";
            user_script_data["r.VolumetricCloud"] = "0";
            user_script_data["sg.GlobalIlluminationQuality"] = "2";
            user_script_data["r.ReflectionMethod"] = "0";

        }
		
    }
}