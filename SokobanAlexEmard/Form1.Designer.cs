namespace SokobanAlexEmard
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.scoreToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.endScreenTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.mainMenuButton = new System.Windows.Forms.Button();
            this.restartButton = new System.Windows.Forms.Button();
            this.nextLevelButton = new System.Windows.Forms.Button();
            this.endScreenTitleLabel = new System.Windows.Forms.Label();
            this.endScreenLevelNoLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.endScreenTableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLevelToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openLevelToolStripMenuItem
            // 
            this.openLevelToolStripMenuItem.Name = "openLevelToolStripMenuItem";
            this.openLevelToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.openLevelToolStripMenuItem.Tag = "openLevel";
            this.openLevelToolStripMenuItem.Text = "Open Level";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.restartToolStripMenuItem.Tag = "restart";
            this.restartToolStripMenuItem.Text = "Restart";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.quitToolStripMenuItem.Tag = "quit";
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.undoToolStripMenuItem.Tag = "undo";
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.redoToolStripMenuItem.Tag = "redo";
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scoreToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // scoreToolStripStatusLabel
            // 
            this.scoreToolStripStatusLabel.Name = "scoreToolStripStatusLabel";
            this.scoreToolStripStatusLabel.Size = new System.Drawing.Size(227, 17);
            this.scoreToolStripStatusLabel.Text = "Load a level in File > Open Level > Level #";
            // 
            // endScreenTableLayoutPanel
            // 
            this.endScreenTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endScreenTableLayoutPanel.ColumnCount = 1;
            this.endScreenTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.endScreenTableLayoutPanel.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.endScreenTableLayoutPanel.Controls.Add(this.endScreenTitleLabel, 0, 0);
            this.endScreenTableLayoutPanel.Controls.Add(this.endScreenLevelNoLabel, 0, 1);
            this.endScreenTableLayoutPanel.Location = new System.Drawing.Point(300, 140);
            this.endScreenTableLayoutPanel.Name = "endScreenTableLayoutPanel";
            this.endScreenTableLayoutPanel.RowCount = 3;
            this.endScreenTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.endScreenTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.endScreenTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.endScreenTableLayoutPanel.Size = new System.Drawing.Size(200, 170);
            this.endScreenTableLayoutPanel.TabIndex = 2;
            this.endScreenTableLayoutPanel.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33555F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33223F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33223F));
            this.tableLayoutPanel2.Controls.Add(this.mainMenuButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.restartButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.nextLevelButton, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 121);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(194, 46);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // mainMenuButton
            // 
            this.mainMenuButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainMenuButton.Location = new System.Drawing.Point(0, 0);
            this.mainMenuButton.Margin = new System.Windows.Forms.Padding(0);
            this.mainMenuButton.Name = "mainMenuButton";
            this.mainMenuButton.Size = new System.Drawing.Size(64, 46);
            this.mainMenuButton.TabIndex = 0;
            this.mainMenuButton.Text = "mainMenuButton";
            this.mainMenuButton.UseVisualStyleBackColor = true;
            // 
            // restartButton
            // 
            this.restartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.restartButton.Location = new System.Drawing.Point(64, 0);
            this.restartButton.Margin = new System.Windows.Forms.Padding(0);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(64, 46);
            this.restartButton.TabIndex = 0;
            this.restartButton.Text = "restartButton";
            this.restartButton.UseVisualStyleBackColor = true;
            // 
            // nextLevelButton
            // 
            this.nextLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextLevelButton.Location = new System.Drawing.Point(128, 0);
            this.nextLevelButton.Margin = new System.Windows.Forms.Padding(0);
            this.nextLevelButton.Name = "nextLevelButton";
            this.nextLevelButton.Size = new System.Drawing.Size(66, 46);
            this.nextLevelButton.TabIndex = 0;
            this.nextLevelButton.Text = "nextLevelButton";
            this.nextLevelButton.UseVisualStyleBackColor = true;
            // 
            // endScreenTitleLabel
            // 
            this.endScreenTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endScreenTitleLabel.AutoSize = true;
            this.endScreenTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endScreenTitleLabel.Location = new System.Drawing.Point(3, 0);
            this.endScreenTitleLabel.Name = "endScreenTitleLabel";
            this.endScreenTitleLabel.Size = new System.Drawing.Size(194, 59);
            this.endScreenTitleLabel.TabIndex = 1;
            this.endScreenTitleLabel.Text = "You Win!";
            this.endScreenTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // endScreenLevelNoLabel
            // 
            this.endScreenLevelNoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endScreenLevelNoLabel.AutoSize = true;
            this.endScreenLevelNoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.endScreenLevelNoLabel.Location = new System.Drawing.Point(0, 59);
            this.endScreenLevelNoLabel.Margin = new System.Windows.Forms.Padding(0);
            this.endScreenLevelNoLabel.Name = "endScreenLevelNoLabel";
            this.endScreenLevelNoLabel.Size = new System.Drawing.Size(200, 59);
            this.endScreenLevelNoLabel.TabIndex = 1;
            this.endScreenLevelNoLabel.Text = "Level #";
            this.endScreenLevelNoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.endScreenTableLayoutPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.endScreenTableLayoutPanel.ResumeLayout(false);
            this.endScreenTableLayoutPanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLevelToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel scoreToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel endScreenTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button mainMenuButton;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Button nextLevelButton;
        private System.Windows.Forms.Label endScreenTitleLabel;
        private System.Windows.Forms.Label endScreenLevelNoLabel;
    }
}

