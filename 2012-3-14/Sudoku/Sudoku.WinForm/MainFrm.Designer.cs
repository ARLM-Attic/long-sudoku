namespace Sudoku.WinForm
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labTime = new System.Windows.Forms.Label();
            this.picBtnReset = new Sudoku.WinForm.PicButton();
            this.picBtnClose = new Sudoku.WinForm.PicButton();
            this.picBtnAnswer = new Sudoku.WinForm.PicButton();
            this.picBtnNewGame = new Sudoku.WinForm.PicButton();
            this.picBtnSave = new Sudoku.WinForm.PicButton();
            this.picBtnOpen = new Sudoku.WinForm.PicButton();
            this.picBtnRedo = new Sudoku.WinForm.PicButton();
            this.picBtnUndo = new Sudoku.WinForm.PicButton();
            this.sudokuLabel = new Sudoku.WinForm.SudokuLabel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnReset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnAnswer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnNewGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnRedo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnUndo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.picBtnReset);
            this.panel2.Controls.Add(this.picBtnClose);
            this.panel2.Controls.Add(this.picBtnAnswer);
            this.panel2.Controls.Add(this.picBtnNewGame);
            this.panel2.Controls.Add(this.picBtnSave);
            this.panel2.Controls.Add(this.picBtnOpen);
            this.panel2.Controls.Add(this.picBtnRedo);
            this.panel2.Controls.Add(this.picBtnUndo);
            this.panel2.Location = new System.Drawing.Point(4, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(430, 53);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 21.75F);
            this.label1.Location = new System.Drawing.Point(0, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sudoku";
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.BackColor = System.Drawing.Color.Transparent;
            this.labTime.Font = new System.Drawing.Font("微软雅黑", 21.75F);
            this.labTime.Location = new System.Drawing.Point(339, 2);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(92, 38);
            this.labTime.TabIndex = 3;
            this.labTime.Text = "00:00";
            // 
            // picBtnReset
            // 
            this.picBtnReset.BackColor = System.Drawing.Color.Transparent;
            this.picBtnReset.Image = global::Sudoku.WinForm.Properties.Resources.appbar_home;
            this.picBtnReset.Location = new System.Drawing.Point(315, 3);
            this.picBtnReset.Name = "picBtnReset";
            this.picBtnReset.Size = new System.Drawing.Size(48, 48);
            this.picBtnReset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBtnReset.TabIndex = 7;
            this.picBtnReset.TabStop = false;
            // 
            // picBtnClose
            // 
            this.picBtnClose.BackColor = System.Drawing.Color.Transparent;
            this.picBtnClose.Image = global::Sudoku.WinForm.Properties.Resources.appbar_close;
            this.picBtnClose.Location = new System.Drawing.Point(379, 3);
            this.picBtnClose.Name = "picBtnClose";
            this.picBtnClose.Size = new System.Drawing.Size(48, 48);
            this.picBtnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBtnClose.TabIndex = 6;
            this.picBtnClose.TabStop = false;
            // 
            // picBtnAnswer
            // 
            this.picBtnAnswer.BackColor = System.Drawing.Color.Transparent;
            this.picBtnAnswer.Image = global::Sudoku.WinForm.Properties.Resources.appbar_eye;
            this.picBtnAnswer.Location = new System.Drawing.Point(263, 3);
            this.picBtnAnswer.Name = "picBtnAnswer";
            this.picBtnAnswer.Size = new System.Drawing.Size(48, 48);
            this.picBtnAnswer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBtnAnswer.TabIndex = 5;
            this.picBtnAnswer.TabStop = false;
            // 
            // picBtnNewGame
            // 
            this.picBtnNewGame.BackColor = System.Drawing.Color.Transparent;
            this.picBtnNewGame.Image = global::Sudoku.WinForm.Properties.Resources.appbar_clipboard;
            this.picBtnNewGame.Location = new System.Drawing.Point(211, 3);
            this.picBtnNewGame.Name = "picBtnNewGame";
            this.picBtnNewGame.Size = new System.Drawing.Size(48, 48);
            this.picBtnNewGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBtnNewGame.TabIndex = 4;
            this.picBtnNewGame.TabStop = false;
            // 
            // picBtnSave
            // 
            this.picBtnSave.BackColor = System.Drawing.Color.Transparent;
            this.picBtnSave.Image = global::Sudoku.WinForm.Properties.Resources.appbar_save_rest;
            this.picBtnSave.Location = new System.Drawing.Point(159, 3);
            this.picBtnSave.Name = "picBtnSave";
            this.picBtnSave.Size = new System.Drawing.Size(48, 48);
            this.picBtnSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBtnSave.TabIndex = 3;
            this.picBtnSave.TabStop = false;
            // 
            // picBtnOpen
            // 
            this.picBtnOpen.BackColor = System.Drawing.Color.Transparent;
            this.picBtnOpen.Image = global::Sudoku.WinForm.Properties.Resources.appbar_base;
            this.picBtnOpen.Location = new System.Drawing.Point(107, 3);
            this.picBtnOpen.Name = "picBtnOpen";
            this.picBtnOpen.Size = new System.Drawing.Size(48, 48);
            this.picBtnOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBtnOpen.TabIndex = 2;
            this.picBtnOpen.TabStop = false;
            // 
            // picBtnRedo
            // 
            this.picBtnRedo.BackColor = System.Drawing.Color.Transparent;
            this.picBtnRedo.Image = global::Sudoku.WinForm.Properties.Resources.appbar_redo;
            this.picBtnRedo.Location = new System.Drawing.Point(55, 3);
            this.picBtnRedo.Name = "picBtnRedo";
            this.picBtnRedo.Size = new System.Drawing.Size(48, 48);
            this.picBtnRedo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBtnRedo.TabIndex = 1;
            this.picBtnRedo.TabStop = false;
            // 
            // picBtnUndo
            // 
            this.picBtnUndo.BackColor = System.Drawing.Color.Transparent;
            this.picBtnUndo.Image = global::Sudoku.WinForm.Properties.Resources.appbar_undo;
            this.picBtnUndo.Location = new System.Drawing.Point(3, 3);
            this.picBtnUndo.Name = "picBtnUndo";
            this.picBtnUndo.Size = new System.Drawing.Size(48, 48);
            this.picBtnUndo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBtnUndo.TabIndex = 0;
            this.picBtnUndo.TabStop = false;
            // 
            // sudokuLabel
            // 
            this.sudokuLabel.BackColor = System.Drawing.Color.Transparent;
            this.sudokuLabel.Location = new System.Drawing.Point(0, 95);
            this.sudokuLabel.Name = "sudokuLabel";
            this.sudokuLabel.Size = new System.Drawing.Size(434, 369);
            this.sudokuLabel.Sudoku = null;
            this.sudokuLabel.TabIndex = 0;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BackgroundImage = global::Sudoku.WinForm.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(434, 464);
            this.Controls.Add(this.labTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.sudokuLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数独";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBtnReset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnAnswer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnNewGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnRedo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtnUndo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private int t;
        private SudokuLabel sudokuLabel;
        private System.Windows.Forms.Panel panel2;
        private PicButton picBtnClose;
        private PicButton picBtnAnswer;
        private PicButton picBtnNewGame;
        private PicButton picBtnSave;
        private PicButton picBtnOpen;
        private PicButton picBtnRedo;
        private PicButton picBtnUndo;
        private PicButton picBtnReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labTime;
    }
}

