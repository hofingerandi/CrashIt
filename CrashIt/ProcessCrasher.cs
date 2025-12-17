using System.Diagnostics;

namespace CrashIt
{
    public static class ProcessCrasher
    {
        public static void CrashProcess(Process? process)
        {
            if (process == null)
                return;

            IntPtr hProcess = IntPtr.Zero;
            try
            {
                hProcess = process?.Handle ?? IntPtr.Zero;
            }
            catch
            {
                MessageBox.Show(
                    $"Failed to get handle for process {process?.ProcessName} (PID: {process?.Id}).\r\n" +
                    "Make sure you have sufficient permissions.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (hProcess == IntPtr.Zero)
                return;

            var choice = MessageBox.Show(
                $"Trying to crash process {process?.ProcessName} (PID: {process?.Id})!\r\nProceed?",
                "Crashing Process",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (choice != DialogResult.OK)
                return;

            // We just use some dummy address, that definitely won't work.
            IntPtr fpProc = new(1234);

            // Create a thread in the other process, and execute our function there.
            IntPtr _ = NativeMethods.CreateRemoteThread(
                hProcess,
                IntPtr.Zero /* threadAttributes */,
                0 /* stackSize */,
                fpProc /* startAddress */,
                IntPtr.Zero /* parameters */,
                0 /* creationFlags */,
                out uint dwThreadId);

            string caption;
            string msg;
            MessageBoxIcon icon;
            if (dwThreadId != 0)
            {
                caption = "Success";
                msg = $"\r\nSuccessfully created remote thread in process {process?.ProcessName} (PID: {process?.Id})";
                icon = MessageBoxIcon.Information;
            }
            else
            {
                caption = "Failure";
                msg = $"\r\nFailed to create remote thread in process {process?.ProcessName} (PID: {process?.Id})";
                icon = MessageBoxIcon.Error;
            }
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, icon);
        }
    }
}
