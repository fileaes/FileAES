using FAES_GUI.CustomControls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FAES_GUI
{
    public partial class DevForm : Form
    {
        public DevForm()
        {
            InitializeComponent();

            titleLabel.Text += Program.GetVersion();
            Console.SetOut(new RichTextBoxWriter(consoleTextBox));
        }

        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void quitButton_MouseEnter(object sender, EventArgs e)
        {
            quitButton.BackColor = Color.Red;
            quitButton.ForeColor = Color.Black;
        }

        private void quitButton_MouseLeave(object sender, EventArgs e)
        {
            quitButton.BackColor = Color.Transparent;
            quitButton.ForeColor = Color.LightGray;
        }

        private void quitButton_MouseHover(object sender, EventArgs e)
        {
            slowToolTip.SetToolTip(quitButton, "Close");
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void ClearConsole_Click(object sender, EventArgs e)
        {
            consoleTextBox.Clear();
        }

        private void ExportLog_Click(object sender, EventArgs e)
        {
            string logPath = "FileAES-" + DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString() + ".log";
            File.WriteAllText(logPath, consoleTextBox.Text);
            consoleTextBox.SelectionColor = Color.LightBlue;
            consoleTextBox.AppendText("Log Exported! (" + logPath + ")" + Environment.NewLine);

            consoleTextBox.SelectionStart = consoleTextBox.TextLength;
            consoleTextBox.SelectionLength = 0;
            consoleTextBox.ScrollToCaret();
        }
    }
}
