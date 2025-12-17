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
            var processId = NativeMethods.GetPidFromPoint(pos);

            if (processId == Environment.ProcessId)
                return;

            Process = Process.GetProcessById((int)processId);
        }
        #endregion

        private void btnCrash_Click(object sender, EventArgs e)
        {
            ProcessCrasher.CrashProcess(_process);
        }
    }
}
