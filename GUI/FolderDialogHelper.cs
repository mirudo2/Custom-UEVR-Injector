using System;
using System.Runtime.InteropServices;

namespace Custom_UEVR_Injector
{
    public static class FolderDialogHelper
    {
        [ComImport, Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
        private class FileOpenDialog { }

        [ComImport, Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IFileOpenDialog
        {
            [PreserveSig]
            int Show(IntPtr hwndParent);

            void SetFileTypes();
            void SetFileTypeIndex();
            void GetFileTypeIndex();
            void Advise();
            void Unadvise();

            [PreserveSig]
            int SetOptions(FOS fos);

            [PreserveSig]
            int GetOptions(out FOS pfos);

            void SetDefaultFolder();
            void SetFolder();
            void GetFolder();
            void GetCurrentSelection();

            [PreserveSig]
            int SetFileName(string pszName);

            [PreserveSig]
            int GetFileName();

            void SetTitle(string pszTitle);

            [PreserveSig]
            int SetOkButtonLabel(string pszText);

            [PreserveSig]
            int SetFileNameLabel(string pszLabel);

            [PreserveSig]
            int GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

            void AddPlace();
            void SetDefaultExtension();
            void Close();
            void SetClientGuid();
            void ClearClientData();
            void SetFilter();
        }

        [ComImport, Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellItem
        {
            [PreserveSig]
            int BindToHandler();

            [PreserveSig]
            int GetParent();

            [PreserveSig]
            int GetDisplayName(SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

            [PreserveSig]
            int GetAttributes();

            [PreserveSig]
            int Compare();
        }

        private enum SIGDN : uint
        {
            SIGDN_FILESYSPATH = 0x80058000
        }

        [Flags]
        private enum FOS : uint
        {
            FOS_PICKFOLDERS = 0x20,
            FOS_FORCEFILESYSTEM = 0x40
        }

        public static string ShowFolderDialog(IntPtr handle)
        {
            try
            {
                var dialog = (IFileOpenDialog)Activator.CreateInstance(
                    Type.GetTypeFromCLSID(new Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")));

                int hr = dialog.SetOptions(FOS.FOS_PICKFOLDERS | FOS.FOS_FORCEFILESYSTEM);
                if (hr != 0) Marshal.ThrowExceptionForHR(hr);

                dialog.SetTitle("Select a folder");

                hr = dialog.Show(handle);
                if (hr == 0) // S_OK
                {
                    hr = dialog.GetResult(out IShellItem item);
                    if (hr == 0 && item != null)
                    {
                        hr = item.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out string path);
                        if (hr == 0 && !string.IsNullOrEmpty(path))
                        {
                            return path;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}