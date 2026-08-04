using System.Diagnostics;

namespace CrashIt
{
    public static class ProcessCrasher
    {
        public static bool CrashProcess(IntPtr hProcess, IntPtr crashAddress)
        {
            // Create a thread in the other process, and execute our function there.
            IntPtr handle = NativeMethods.CreateRemoteThread(
                hProcess,
                IntPtr.Zero /* threadAttributes */,
                0 /* stackSize */,
                crashAddress /* startAddress */,
                IntPtr.Zero /* parameters */,
                0 /* creationFlags */,
                out uint dwThreadId);

            return handle != IntPtr.Zero;
        }
    }
}
