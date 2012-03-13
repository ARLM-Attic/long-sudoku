using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Sudoku.WinForm
{
    public class PicButton : PictureBox
    {

        public PicButton()
        {
            AutoSize = true;
            SizeMode = PictureBoxSizeMode.CenterImage;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            Timer timer=new Timer {Interval = 50};
            float w = 0;
            float step = 8;
            
            bool isBig = false;
            Image ori = null;
            timer.Tick += delegate
                              {
                                  if(isBig)
                                  {
                                      w += step;
                                      if(w>ori.Width)
                                      {
                                          w = ori.Width;
                                          isBig = false;
                                      }
                                  }
                                  else
                                  {
                                      w -= step;
                                      if (w <step)
                                      {
                                          w = step;
                                          isBig = true;
                                      }
                                  }
                                  Image = KiRotate(ori , w, BackColor);
                              };
            MouseLeave += delegate
                              {
                                  timer.Stop();

                                  if (ori != null)
                                      Image = ori;
                              };
            MouseEnter += delegate
                              {
                                  if (Image!=null)
                                  {
                                      ori = Image;
                                      w = Image.Width;
                                      timer.Start();
                                  }
                              };
        }


        /// <summary>
        /// 任意角度旋转
        /// </summary>
        /// <param name="bmp">原始图Bitmap</param>
        /// <param name="angle">旋转角度</param>
        /// <param name="bkColor">背景色</param>
        /// <returns>输出Bitmap</returns>
        private static Bitmap KiRotate(Image bmp, float angle, Color bkColor)
        {
            int w = bmp.Width + 2;
            int h = bmp.Height + 2;

            PixelFormat pf = bkColor == Color.Transparent ? PixelFormat.Format32bppArgb : bmp.PixelFormat;

            Bitmap tmp = new Bitmap(w, h, pf);
            Graphics g = Graphics.FromImage(tmp);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 1, 1);
            g.Dispose();

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, w, h));
            Matrix mtrx = new Matrix();
            mtrx.Rotate(angle);
            RectangleF rct = path.GetBounds(mtrx);

            Bitmap dst = new Bitmap((int)rct.Width, (int)rct.Height, pf);
            g = Graphics.FromImage(dst);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tmp, 0, 0);
            g.Dispose();

            tmp.Dispose();

            return dst;
        }
    }
}
