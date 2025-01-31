using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;
using System;
using static Dject.api;
using System.Runtime.InteropServices;
using System.Text;

namespace Dject
{
    public class DJect
    {
        public static bool Dllinject(string process, string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Couldn't find dll. Please check path or privilege.", "Dject error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Process[] pr = Process.GetProcessesByName(process);
            if (pr.Length == 0)
            {
                MessageBox.Show($"Couldn't find process for {process}. typo?", "Dject error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            foreach (Process p in pr)
            {
                GetAccess(path);
                IntPtr hproc = OpenProcess(1082, false, p.Id);
                IntPtr Address = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                IntPtr vAlloc = VirtualAllocEx(hproc, IntPtr.Zero, (uint)((path.Length + 1) * Marshal.SizeOf(typeof(char))), 12288U, 4U);
                UIntPtr uintp;
                WriteProcessMemory(hproc, vAlloc, Encoding.Default.GetBytes(path), (uint)((path.Length + 1) * Marshal.SizeOf(typeof(char))), out uintp);
                CreateRemoteThread(hproc, IntPtr.Zero, 0u, Address, vAlloc, 0u, IntPtr.Zero); ;
            }
            return true;
        }
        private static void GetAccess(string path)
        {
            FileInfo f = new FileInfo(path);
            FileSecurity fs = f.GetAccessControl();
            fs.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, InheritanceFlags.None, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            f.SetAccessControl(fs);
        }
    }
}
