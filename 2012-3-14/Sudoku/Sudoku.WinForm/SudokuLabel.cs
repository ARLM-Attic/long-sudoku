using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sudoku.Common;

namespace Sudoku.WinForm
{
    public class SudokuLabel : Label
    {
        private readonly static object sync = new object();
        private static readonly int SoildWidth = 360;
        private static readonly int SamllWidth = SoildWidth / 9;
        private static readonly byte Offset = 3;
        private static readonly byte Count = 9;
        private readonly Pen LinePen;
        private readonly Font NumberFont;
        private readonly Brush CellBrush;
        private readonly Brush FontBrush;
        private readonly Brush EmptyBrush;
        private readonly Brush ErrorBrush;
        private readonly Brush EmptyCellBrush;
        private readonly Brush RedBrush;
        private byte[,] original = new byte[9, 9];
        private List<byte> canSetValues = new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private int xpos = -1, ypos = -1;
        public bool IsStart { get; set; }

        private Form liveIn;
        public Form LiveIn
        {
            get { return liveIn; }
            set
            {
                liveIn = value;
                liveIn.KeyUp += SudokuLabel_KeyUp;
            }
        }

        public SudokuLabel()
        {
            Location = new Point();
            //NumberFont = new Font("微软雅黑", 12);
            NumberFont = new Font("微软雅黑", 26);
            FontBrush = new SolidBrush(Color.Black);
            EmptyBrush = new SolidBrush(Color.White);
            ErrorBrush = new SolidBrush(Color.Transparent);
            RedBrush = new SolidBrush(Color.Red);
            Width = SoildWidth;
            Height = SoildWidth;
            LinePen = new Pen(Color.Black, 3);
            CellBrush = new SolidBrush(Color.FromArgb(100, Color.Gray));
            AutoSize = false;
            EmptyCellBrush = new SolidBrush(Color.FromArgb(150, Color.DarkBlue));
            MouseClick += SudokuLabel_MouseClick;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        public void SudokuLabel_KeyUp(object sender, KeyEventArgs e)
        {
            if(xpos<0)
                return;
            if (HandlerKey(e.KeyCode))
            {
                canSetValues.Clear();
                var d = Sudoku.CanSetValues((byte) xpos, (byte) ypos);
                if (d != null)
                    canSetValues.AddRange(d);
                Invalidate();
                return;
            }
            int value = e.KeyValue;
            bool isSet = false;
            if(value<106&&value>96)
            {
                sudoku[(byte) xpos, (byte) ypos] = (byte) (value - 96);
                Invalidate();
                isSet = true;
            }
            else if (value < 58 && value > 48)
            {
                sudoku[(byte)xpos, (byte)ypos] = (byte)(value - 48);
                Invalidate();
                isSet = true;
            }
            bool finish = Sudoku.IsFinish;
            if (isSet && finish)
            {
                if (MessageBox.Show(@"恭喜过关，是否继续！", @"过关了", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    liveIn.Close();
                }
                else
                {
                    sudoku.Initialize();
                    Invalidate();
                }
            }
            if (finish)
                return;
            if(value==96||value==48||value==32)
            {
                sudoku[(byte)xpos, (byte)ypos] = 0;
                Invalidate();
            }
        }

        private void SudokuLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (!IsStart)
                return;
            if (Sudoku.IsFinish)
                return;
            byte x = (byte)((e.X - 3) / SamllWidth);
            byte y = (byte)((e.Y - 3) / SamllWidth);
            if (x > 8 || x < 0 || y > 8 || y < 0)
                return;
            if (x == xpos && y == ypos)
                return;
            if (original[x, y] != 0)
                return;
            xpos = x;
            ypos = y;
            canSetValues.Clear();
            var d = Sudoku.CanSetValues(x, y);
            if (d != null)
                canSetValues.AddRange(d);
            Invalidate();
        }

        private ISudoku sudoku;

        public ISudoku Sudoku
        {
            get { return sudoku; }
            set
            {
                if (value == null) return;
                sudoku = value;
                sudoku.OnInitializeFinish += delegate
                {
                    original = sudoku.Original;
                    IsStart = true;
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (original[i, j] != 0) continue;
                            xpos = i;
                            ypos = j;
                            canSetValues.Clear();
                            var d = Sudoku.CanSetValues((byte)xpos, (byte)ypos);
                            if (d != null)
                                canSetValues.AddRange(d);
                            Invalidate();
                            return;
                        }
                    }
                };
                sudoku.OnLoadFinish += delegate
                {
                    original = sudoku.Original;
                    IsStart = true;
                };
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            #region 准备数据，绘画小方块

            Graphics g = e.Graphics;
            byte[,] data = new byte[Count, Count];
            lock (sync)
            {
                if (Sudoku != null)
                    data = Sudoku.Current;
            }
            int x = 3 * SamllWidth + 3;
            int width = x - 3;

            g.FillRectangle(CellBrush, x, Offset, width, width);
            g.FillRectangle(CellBrush, Offset, x, width, width);

            int y = 6 * SamllWidth + 3;



            g.FillRectangle(CellBrush, x, y, width, width);
            g.FillRectangle(CellBrush, y, x, width, width);

            #endregion

            #region 绘画主要部分

            int i = 0, j = 0;
            for (; i < Count; i++)
            {
                for (j = 0; j < Count; j++)
                {
                    byte d = data[i, j];
                    if (d != 0)
                        g.DrawString(d.ToString(), NumberFont, original[i, j] > 0 ? FontBrush : (sudoku.CanSet((byte) i,(byte) j,d)?EmptyBrush:RedBrush), i * SamllWidth + Offset * 2, j * SamllWidth + Offset);
                    //g.DrawString(string.Format("{0},{1}", i, j), new Font("微软雅黑", 12), original[i, j] > 0 ? FontBrush : EmptyBrush, i * SamllWidth + Offset * 2, j * SamllWidth + Offset);
                    g.DrawLine(LinePen, Offset, j * SamllWidth + Offset, SoildWidth + Offset + 1, j * SamllWidth + Offset);
                    g.DrawLine(LinePen, i * SamllWidth + Offset, Offset, i * SamllWidth + Offset, SoildWidth + Offset + 1);
                }
            }
            g.DrawLine(LinePen, Offset, j * SamllWidth + Offset, SoildWidth + Offset + 1, j * SamllWidth + Offset);
            g.DrawLine(LinePen, i * SamllWidth + Offset, Offset, i * SamllWidth + Offset, SoildWidth + Offset + 1);

            #endregion

            #region 绘画数字及辅助块

            int off = Offset * 8;

            g.DrawLine(LinePen, i * SamllWidth + off, Offset, i++ * SamllWidth + off, SoildWidth + Offset + 1);
            g.DrawLine(LinePen, i * SamllWidth + off, Offset, i * SamllWidth + off, SoildWidth + Offset + 1);


            off = SoildWidth + off;
            lock (canSetValues)
            {
                for (i = 0; i < 9; i++)
                {
                    g.DrawLine(LinePen, off, i * SamllWidth + Offset, off + SamllWidth + 1, i * SamllWidth + Offset);
                    g.DrawString((i + 1).ToString(), NumberFont, canSetValues.Contains((byte)(i + 1)) ? FontBrush : ErrorBrush, off + Offset, i * SamllWidth + Offset);
                }
            }
            g.DrawLine(LinePen, off, i * SamllWidth + Offset, off + SamllWidth + 1, i * SamllWidth + Offset);
            if (xpos >= 0 && xpos >= 0 && !Sudoku.IsFinish)
                g.FillRectangle(EmptyCellBrush, xpos * SamllWidth + Offset, ypos * SamllWidth + Offset, SamllWidth, SamllWidth);

            #endregion
        }

        #region 键盘处理
        private bool HandlerKey(Keys code)
        {
            switch (code)
            {
                case Keys.Up:
                    ypos = UpKey((byte)xpos, (byte)ypos);
                    return true;
                case Keys.Down:
                    ypos = DownKey((byte)xpos, (byte)ypos);
                    return true;
                case Keys.Left:
                    xpos = LeftKey((byte)xpos, (byte)ypos);
                    return true;
                case Keys.Right:
                    xpos = RightKey((byte)xpos, (byte)ypos);
                    return true;
            }
            return false;
        }
        private byte UpKey(byte x, byte? y)
        {
            byte? p = null;
            for (int i = y.HasValue ? y.Value - 1 : 8; i >= 0; i--)
            {
                if (original[x, i] == 0)
                {
                    p = (byte?)i;
                    break;
                }
            }
            if (p.HasValue)
                return p.Value;
            byte t = (byte)((x - 1) < 0 ? 8 : (x - 1));
            xpos = t;
            return UpKey(t, null);
        }
        private byte DownKey(byte x, byte? y)
        {
            byte? p = null;
            for (byte i = (byte)(y.HasValue ? y + 1 : 0); i < 9; i++)
            {
                if (original[x, i] == 0)
                {
                    p = i;
                    break;
                }
            }
            if (p.HasValue)
                return p.Value;
            byte t = (byte)(x + 1 > 8 ? 0 : x + 1);
            xpos = t;
            return DownKey(t, null);
        }
        private byte RightKey(byte? x, byte y)
        {
            byte? p = null;
            for (byte i = (byte)(x.HasValue ? x.Value + 1 : 0); i < 9; i++)
            {
                if (original[i, y] == 0)
                {
                    p = i;
                    break;
                }
            }
            if (p.HasValue)
                return p.Value;
            byte t = (byte)(y + 1 > 8 ? 0 : y + 1);
            ypos = t;
            return RightKey(null, t);
        }
        private byte LeftKey(byte? x, byte y)
        {
            byte? p = null;
            for (int i = x.HasValue ? x.Value - 1 : 8; i >= 0; i--)
            {
                if (original[i, y] == 0)
                {
                    p = (byte?)i;
                    break;
                }
            }
            if (p.HasValue)
                return p.Value;
            byte t = (byte)((y - 1) < 0 ? 8 : (y - 1));
            ypos = t;
            return LeftKey(null, t);
        }
        #endregion
    }
}
