namespace CrashIt
{
    public partial class MainForm : Form
    {
        private bool isDragging;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                Cursor = Cursors.Cross;
                btnSelectWindow.Capture = true;
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
                txtInfo.Text = hWnd.ToString("X");
            }
        }
    }
}
