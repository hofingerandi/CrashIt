using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CrashIt
{
    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);


        // https://learn.microsoft.com/en-us/archive/blogs/jmstall/using-createremotethread-from-c
        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(
  IntPtr hProcess,
  IntPtr lpThreadAttributes,
  uint dwStackSize,
  IntPtr lpStartAddress, // raw Pointer into remote process
  IntPtr lpParameter,
  uint dwCreationFlags,
  out uint lpThreadId
);

        public static int MyThreadProc(IntPtr param)
        {
            int pid = Process.GetCurrentProcess().Id;
            Console.WriteLine("Pid {0}: Inside my new thread!. Param={1}", pid, param.ToInt32());
            return 1;
        }
    }
}
