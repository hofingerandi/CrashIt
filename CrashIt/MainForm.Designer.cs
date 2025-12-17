namespace CrashIt
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelectWindow = new Button();
            txtInfo = new TextBox();
            btnCrash = new Button();
            lblInfo = new Label();
            SuspendLayout();
            // 
            // btnSelectWindow
            // 
            btnSelectWindow.Font = new Font("Segoe UI", 20F);
            btnSelectWindow.Location = new Point(12, 12);
            btnSelectWindow.Name = "btnSelectWindow";
            btnSelectWindow.Size = new Size(75, 66);
            btnSelectWindow.TabIndex = 0;
            btnSelectWindow.Text = "⌖";
            btnSelectWindow.UseVisualStyleBackColor = true;
            btnSelectWindow.MouseDown += btnSelectWindow_MouseDown;
            btnSelectWindow.MouseMove += btnSelectWindow_MouseMove;
            btnSelectWindow.MouseUp += btnSelectWindow_MouseUp;
            // 
            // txtInfo
            // 
            txtInfo.Location = new Point(12, 84);
            txtInfo.Multiline = true;
            txtInfo.Name = "txtInfo";
            txtInfo.Size = new Size(287, 131);
            txtInfo.TabIndex = 1;
            // 
            // btnCrash
            // 
            btnCrash.BackColor = Color.DarkRed;
            btnCrash.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCrash.ForeColor = Color.White;
            btnCrash.Location = new Point(12, 218);
            btnCrash.Name = "btnCrash";
            btnCrash.Size = new Size(287, 40);
            btnCrash.TabIndex = 2;
            btnCrash.Text = "Crash It!";
            btnCrash.UseVisualStyleBackColor = false;
            btnCrash.EnabledChanged += btnCrash_EnabledChanged;
            btnCrash.Click += btnCrash_Click;
            btnCrash.Paint += btnCrash_Paint;
            // 
            // lblInfo
            // 
            lblInfo.Location = new Point(93, 12);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(206, 66);
            lblInfo.TabIndex = 3;
            lblInfo.Text = "Drag the cursor and select a window.\r\nThe application will try to crash the corresponding process.";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(310, 267);
            Controls.Add(lblInfo);
            Controls.Add(btnCrash);
            Controls.Add(txtInfo);
            Controls.Add(btnSelectWindow);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Crash It!";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectWindow;
        private TextBox txtInfo;
        private Button btnCrash;
        private Label lblInfo;
    }
}
