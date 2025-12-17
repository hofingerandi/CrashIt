using System.Diagnostics;

namespace CrashIt
{
    public partial class MainForm : Form
    {
        private bool isDragging;
        private Process? _process;
        public Process? Process
        {
            get { return _process; }
            set { _process = value; UpdateButtons(); }
        }

        public MainForm()
        {
            InitializeComponent();
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            if (_process != null)
            {
                btnCrash.Enabled = true;
                txtInfo.Text = $"Selected Process: {_process.ProcessName}\r\nPID: {_process.Id}";
            }
            else
            {
                btnCrash.Enabled = false;
                txtInfo.Text = "Selected Process: None";
            }
        }

        private void btnSelectWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                Cursor = Cursors.Cross;
                btnSelectWindow.Capture = true;
                _process = null;
            }
        }

        private void btnSelectWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                this.Cursor = Cursors.Default;
                btnSelectWindow.Capture = false;
            }
        }

        private void btnSelectWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging)
                return;

            var pos = Cursor.Position;
            IntPtr hWnd = NativeMethods.WindowFromPoint(pos);
            if (hWnd != IntPtr.Zero)
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out var processId);
                // Don't allow selecting our own process.
                if (processId == Process.GetCurrentProcess().Id)
                    return;

                Process = Process.GetProcessById((int)processId);
            }
        }

        private void btnCrash_Click(object sender, EventArgs e)
        {
            if (_process == null)
                return;

            IntPtr hProcess = _process?.Handle ?? IntPtr.Zero;
            if (hProcess == IntPtr.Zero)
                return;

            var choice = MessageBox.Show(
                $"Trying to crash process {_process?.ProcessName} (PID: {_process?.Id})!\r\nProceed?",
                "Crashing Process",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (choice != DialogResult.OK)
                return;

            // We just use some dummy address, that definitely won't work.
            IntPtr fpProc = new IntPtr(1234);

            uint dwThreadId;
            // Create a thread in the other process, and execute our function there.
            IntPtr hThread = NativeMethods.CreateRemoteThread(
                hProcess,
                IntPtr.Zero /* threadAttributes */,
                0 /* stackSize */,
                fpProc /* startAddress */,
                IntPtr.Zero /* parameters */,
                0 /* creationFlags */,
                out dwThreadId);

            string msg;
            if (dwThreadId != 0)
            {
                msg = $"\r\nSuccessfully created remote thread in process {_process?.ProcessName} (PID: {_process?.Id})";
            }
            else
            {
                msg = $"\r\nFailed to create remote thread in process {_process?.ProcessName} (PID: {_process?.Id})";
            }
            MessageBox.Show(msg);
        }
    }
}
