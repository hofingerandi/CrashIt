using System.Runtime.InteropServices;

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


        public static int GetPidFromPoint(Point pos)
        {
            int processId = 0;
            IntPtr hWnd = NativeMethods.WindowFromPoint(pos);
            if (hWnd != IntPtr.Zero)
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint uProcessId);
                processId = (int)uProcessId;
            }
            return processId;
        }
    }
}
