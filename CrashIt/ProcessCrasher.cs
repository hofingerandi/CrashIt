using System.Diagnostics;

namespace CrashIt
{
    public static class ProcessCrasher
    {
        public static bool CrashProcess(IntPtr hProcess, ProcessModule? module)
        {
            // We use some dummy address, that definitely won't work.
            // If a module is given, pick something relative to that module
            // The index is small enough, to point at non-executable memory, so that the thread will crash immediately.
            IntPtr crashAddress = (module?.BaseAddress ?? 0x00) + 0x0123;

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

        public static List<ProcessModule> GetCandidateModules(Process process)
        {
            var result = new List<ProcessModule>();

            var mainName = process.MainModule?.FileName;
            if (mainName == null)
            {
                return result;
            }

            var basePath = new FileInfo(mainName).Directory?.FullName;
            if (basePath == null)
            {
                return result;
            }
            result.Add(process.MainModule!);

            foreach (ProcessModule module in process.Modules)
            {
                if (module.FileName.StartsWith(basePath, StringComparison.OrdinalIgnoreCase)
                    && module.FileName != mainName)
                {
                    result.Add(module);
                }
            }
            return result;
        }
    }
}
