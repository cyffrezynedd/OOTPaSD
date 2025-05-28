namespace Editor{
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
            menuStrip = new MenuStrip();
            tsFile = new ToolStripMenuItem();
            tsFileOpen = new ToolStripMenuItem();
            tsFileSave = new ToolStripMenuItem();
            tsCanvas = new ToolStripMenuItem();
            tsClear = new ToolStripMenuItem();
            tsPlugin = new ToolStripMenuItem();
            toolStrip = new ToolStrip();
            tsTool = new ToolStripSplitButton();
            tsToolPencil = new ToolStripMenuItem();
            tsToolEraser = new ToolStripMenuItem();
            tsToolFigure = new ToolStripMenuItem();
            tsToolLine = new ToolStripMenuItem();
            tsStrokeLabel = new ToolStripLabel();
            tsStrokeValue = new ToolStripDropDownButton();
            tsBtnRedo = new ToolStripButton();
            tsBtnUndo = new ToolStripButton();
            panelColor = new Panel();
            colorDialog = new ColorDialog();
            pictureBox = new PictureBox();
            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { tsFile, tsCanvas, tsPlugin });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1000, 24);
            menuStrip.TabIndex = 1;
            // 
            // tsFile
            // 
            tsFile.DropDownItems.AddRange(new ToolStripItem[] { tsFileOpen, tsFileSave });
            tsFile.Name = "tsFile";
            tsFile.Size = new Size(48, 20);
            tsFile.Text = "Файл";
            // 
            // tsFileOpen
            // 
            tsFileOpen.Name = "tsFileOpen";
            tsFileOpen.ShortcutKeys = Keys.Control | Keys.O;
            tsFileOpen.Size = new Size(180, 22);
            tsFileOpen.Text = "Открыть";
            tsFileOpen.Click += FileOpenClick;
            // 
            // tsFileSave
            // 
            tsFileSave.Name = "tsFileSave";
            tsFileSave.ShortcutKeys = Keys.Control | Keys.S;
            tsFileSave.Size = new Size(180, 22);
            tsFileSave.Text = "Сохранить";
            tsFileSave.Click += FileSaveClick;
            // 
            // tsCanvas
            // 
            tsCanvas.DropDownItems.AddRange(new ToolStripItem[] { tsClear });
            tsCanvas.Name = "tsCanvas";
            tsCanvas.ShortcutKeys = Keys.Control | Keys.C;
            tsCanvas.Size = new Size(68, 20);
            tsCanvas.Text = "Полотно";
            // 
            // tsClear
            // 
            tsClear.Name = "tsClear";
            tsClear.Size = new Size(126, 22);
            tsClear.Text = "Очистить";
            // 
            // tsPlugin
            // 
            tsPlugin.Name = "tsPlugin";
            tsPlugin.Size = new Size(69, 20);
            tsPlugin.Text = "Плагины";
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { tsTool, tsStrokeLabel, tsStrokeValue, tsBtnRedo, tsBtnUndo });
            toolStrip.Location = new Point(0, 24);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(1000, 25);
            toolStrip.TabIndex = 2;
            // 
            // tsTool
            // 
            tsTool.DropDownItems.AddRange(new ToolStripItem[] { tsToolPencil, tsToolEraser, tsToolFigure, tsToolLine });
            tsTool.ImageTransparentColor = Color.Magenta;
            tsTool.Name = "tsTool";
            tsTool.Size = new Size(96, 22);
            tsTool.Text = "Инструмент :";
            tsTool.TextImageRelation = TextImageRelation.TextBeforeImage;
            tsTool.ToolTipText = "Инструмент";
            // 
            // tsToolPencil
            // 
            tsToolPencil.Image = Properties.Resources.pen;
            tsToolPencil.Name = "tsToolPencil";
            tsToolPencil.Size = new Size(130, 22);
            tsToolPencil.Tag = "Pencil";
            tsToolPencil.Text = "Карандаш";
            // 
            // tsToolEraser
            // 
            tsToolEraser.Image = Properties.Resources.eraser;
            tsToolEraser.Name = "tsToolEraser";
            tsToolEraser.Size = new Size(130, 22);
            tsToolEraser.Tag = "Eraser";
            tsToolEraser.Text = "Ластик";
            // 
            // tsToolFigure
            // 
            tsToolFigure.Image = Properties.Resources.figures;
            tsToolFigure.Name = "tsToolFigure";
            tsToolFigure.Size = new Size(130, 22);
            tsToolFigure.Tag = "Figures";
            tsToolFigure.Text = "Фигуры";
            // 
            // tsToolLine
            // 
            tsToolLine.Image = Properties.Resources.lines;
            tsToolLine.Name = "tsToolLine";
            tsToolLine.Size = new Size(130, 22);
            tsToolLine.Tag = "Lines";
            tsToolLine.Text = "Линии";
            // 
            // tsStrokeLabel
            // 
            tsStrokeLabel.Name = "tsStrokeLabel";
            tsStrokeLabel.Size = new Size(58, 22);
            tsStrokeLabel.Text = "Ширина :";
            tsStrokeLabel.ToolTipText = "Ширина линии";
            // 
            // tsStrokeValue
            // 
            tsStrokeValue.AutoSize = false;
            tsStrokeValue.AutoToolTip = false;
            tsStrokeValue.BackColor = SystemColors.GradientActiveCaption;
            tsStrokeValue.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsStrokeValue.ImageTransparentColor = Color.Magenta;
            tsStrokeValue.Name = "tsStrokeValue";
            tsStrokeValue.Size = new Size(47, 22);
            // 
            // tsBtnRedo
            // 
            tsBtnRedo.Alignment = ToolStripItemAlignment.Right;
            tsBtnRedo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnRedo.Image = Properties.Resources.redo;
            tsBtnRedo.ImageTransparentColor = Color.Magenta;
            tsBtnRedo.Margin = new Padding(2, 1, 5, 2);
            tsBtnRedo.Name = "tsBtnRedo";
            tsBtnRedo.Size = new Size(23, 22);
            tsBtnRedo.Click += BtnRedoClick;
            // 
            // tsBtnUndo
            // 
            tsBtnUndo.Alignment = ToolStripItemAlignment.Right;
            tsBtnUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBtnUndo.Image = Properties.Resources.undo;
            tsBtnUndo.ImageTransparentColor = Color.Magenta;
            tsBtnUndo.Margin = new Padding(3, 1, 5, 2);
            tsBtnUndo.Name = "tsBtnUndo";
            tsBtnUndo.RightToLeft = RightToLeft.No;
            tsBtnUndo.Size = new Size(23, 22);
            tsBtnUndo.Click += BtnUndoClick;
            // 
            // panelColor
            // 
            panelColor.BorderStyle = BorderStyle.FixedSingle;
            panelColor.Location = new Point(0, 48);
            panelColor.Name = "panelColor";
            panelColor.Size = new Size(1000, 26);
            panelColor.TabIndex = 3;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(0, 75);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1000, 675);
            pictureBox.TabIndex = 4;
            pictureBox.TabStop = false;
            pictureBox.Paint += PictureBoxPaint;
            pictureBox.MouseDoubleClick += PictureBoxMouseDoubleClick;
            pictureBox.MouseDown += PictureBoxMouseDown;
            pictureBox.MouseMove += PictureBoxMouseMove;
            pictureBox.MouseUp += PictureBoxMouseUp;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 736);
            Controls.Add(pictureBox);
            Controls.Add(panelColor);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);
            Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "MainForm";
            ShowIcon = false;
            Text = " Editor";
            Load += MainFormLoad;
            KeyDown += MainFormKeyDown;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip;
        private ToolStrip toolStrip;
        private ToolStripMenuItem tsFile;
        private ToolStripMenuItem tsFileOpen;
        private ToolStripMenuItem tsFileSave;
        private ToolStripMenuItem tsCanvas;
        private ToolStripMenuItem tsClear;
        private ToolStripSplitButton tsTool;
        private ToolStripMenuItem tsToolPencil;
        private ToolStripMenuItem tsToolFigure;
        private ToolStripMenuItem tsToolLine;
        private ToolStripDropDownButton tsStrokeValue;
        private Panel panelColor;
        private ColorDialog colorDialog;
        private ToolStripMenuItem tsPlugin;
        private ToolStripLabel tsStrokeLabel;
        private PictureBox pictureBox;
        private ToolStripMenuItem tsToolEraser;
        private ToolStripButton tsBtnRedo;
        private ToolStripButton tsBtnUndo;
    }
}
