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

        #region Red Button
        private void btnCrash_EnabledChanged(object sender, EventArgs e)
        {
            btnCrash.ForeColor = btnCrash.Enabled ? System.Drawing.Color.White : System.Drawing.Color.Gray;
            btnCrash.BackColor = System.Drawing.Color.DarkRed;
        }

        private void btnCrash_Paint(object sender, PaintEventArgs e)
        {
            // https://stackoverflow.com/a/23416851/821134

            Button btn = (Button)sender;
            btn.Text = string.Empty;
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;   // center the text
            TextRenderer.DrawText(e.Graphics, "Crash It", btn.Font, e.ClipRectangle, btn.ForeColor, flags);
        }
        #endregion

        #region Select Window
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
        #endregion

        private void btnCrash_Click(object sender, EventArgs e)
        {
            if (_process == null)
                return;

            IntPtr hProcess = IntPtr.Zero;
            try
            {
                hProcess = _process?.Handle ?? IntPtr.Zero;
            }
            catch
            {
                MessageBox.Show(
                    $"Failed to get handle for process {_process?.ProcessName} (PID: {_process?.Id}).\r\n" +
                    "Make sure you have sufficient permissions.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

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

            string caption;
            string msg;
            MessageBoxIcon icon;
            if (dwThreadId != 0)
            {
                caption = "Success";
                msg = $"\r\nSuccessfully created remote thread in process {_process?.ProcessName} (PID: {_process?.Id})";
                icon = MessageBoxIcon.Information;
            }
            else
            {
                caption = "Failure";
                msg = $"\r\nFailed to create remote thread in process {_process?.ProcessName} (PID: {_process?.Id})";
                icon = MessageBoxIcon.Error;
            }
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, icon);
        }
    }
}
