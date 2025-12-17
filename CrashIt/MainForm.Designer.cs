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
            txtInfo.Location = new Point(12, 100);
            txtInfo.Multiline = true;
            txtInfo.Name = "txtInfo";
            txtInfo.Size = new Size(100, 73);
            txtInfo.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtInfo);
            Controls.Add(btnSelectWindow);
            Name = "MainForm";
            Text = "Crash It!";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectWindow;
        private TextBox txtInfo;
    }
}
