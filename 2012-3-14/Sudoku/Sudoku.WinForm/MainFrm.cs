using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Sudoku.Common;

namespace Sudoku.WinForm
{
    public partial class MainFrm : Form
    {
        #region 窗体可拖动代码

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;
        private void MainFrm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion
        private Timer timer = new Timer { Interval = 1000 };
        private static readonly string filter = "数独存储文件*.sxml|*.sxml";
        public MainFrm()
        {

            InitializeComponent();

            Image background = null;
            CommonFunc.SafetyRun(delegate { background = Image.FromFile("background.jpg"); }, false);
            if (background != null)
                BackgroundImage = background;
            t = 0;
            MouseDown += MainFrm_MouseDown;
            KeyUp += MainFrm_KeyUp;
            sudokuLabel.Sudoku = SudokuHelper.CreateSudoku(null);
            sudokuLabel.LiveIn = this;
            timer.Tick += delegate
            {
                t += 1;
                labTime.Text = string.Format("{0:D2}:{1:D2}", t / 60, t % 60);
            };

            #region 按钮事件
            picBtnNewGame.Click += picBtnNewGame_Click;
            picBtnAnswer.Click += picBtnAnswer_Click;
            picBtnUndo.Click += picBtnUndo_Click;
            picBtnRedo.Click += picBtnRedo_Click;
            picBtnReset.Click += picBtnReset_Click;
            picBtnSave.Click += picBtnSave_Click;
            picBtnOpen.Click += picBtnOpen_Click;
            picBtnClose.Click += delegate { Close(); };
            #endregion

        }

        private void picBtnOpen_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = filter;
                if (ofd.ShowDialog() != DialogResult.OK) return;
                timer.Start();
                sudokuLabel.Sudoku.Load(ofd.FileName);
                sudokuLabel.Invalidate();
            }
        }

        private void picBtnSave_Click(object sender, EventArgs e)
        {
            using (var ofd = new SaveFileDialog())
            {
                ofd.Filter = filter;
                if (ofd.ShowDialog() != DialogResult.OK) return;
                sudokuLabel.Sudoku.Save(ofd.FileName);
            }
        }

        private void picBtnReset_Click(object sender, EventArgs e)
        {
            if (!sudokuLabel.IsStart)
                return;
            if (MessageBox.Show(@"确定重置结果", @"是否重置", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sudokuLabel.Sudoku.Reset();
                sudokuLabel.Invalidate();
            }
        }

        private void picBtnRedo_Click(object sender, EventArgs e)
        {
            sudokuLabel.Sudoku.Redo();
            sudokuLabel.Invalidate();
        }

        private void picBtnUndo_Click(object sender, EventArgs e)
        {
            sudokuLabel.Sudoku.Undo();
            sudokuLabel.Invalidate();
        }

        private void picBtnAnswer_Click(object sender, EventArgs e)
        {
            sudokuLabel.Sudoku.Solve();
            timer.Stop();
            sudokuLabel.Invalidate();
        }

        private void picBtnNewGame_Click(object sender, EventArgs e)
        {
            sudokuLabel.Sudoku.Initialize();
            timer.Start();
            t = 0;
            sudokuLabel.Invalidate();
        }

        private void MainFrm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.O)
            {
                picBtnOpen_Click(this,null);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                picBtnSave_Click(this, null);
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                picBtnUndo_Click(this, null);
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                picBtnRedo_Click(this, null);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                picBtnSave_Click(this, null);
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                picBtnNewGame_Click(this, null);
            }
            else if (e.Control && e.KeyCode == Keys.H)
            {
                picBtnAnswer_Click(this, null);
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                picBtnReset_Click(this, null);
            }
        }
    }
}
