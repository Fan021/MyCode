namespace LaserEzd
{
    partial class Debug
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
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.redline_btn = new System.Windows.Forms.Button();
            this.mark_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonInit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Font = new System.Drawing.Font("SimHei", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOpenFile.Location = new System.Drawing.Point(566, 29);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(32, 21);
            this.buttonOpenFile.TabIndex = 3;
            this.buttonOpenFile.Text = "...";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(12, 29);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(548, 21);
            this.textBoxFileName.TabIndex = 2;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox.Location = new System.Drawing.Point(12, 86);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(548, 323);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 34;
            this.pictureBox.TabStop = false;
            // 
            // buttonPreview
            // 
            this.buttonPreview.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonPreview.Location = new System.Drawing.Point(580, 141);
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(96, 36);
            this.buttonPreview.TabIndex = 227;
            this.buttonPreview.Tag = "0";
            this.buttonPreview.Text = "预览";
            this.buttonPreview.UseVisualStyleBackColor = true;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // redline_btn
            // 
            this.redline_btn.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.redline_btn.Location = new System.Drawing.Point(580, 196);
            this.redline_btn.Name = "redline_btn";
            this.redline_btn.Size = new System.Drawing.Size(96, 36);
            this.redline_btn.TabIndex = 228;
            this.redline_btn.Tag = "0";
            this.redline_btn.Text = "红光对标";
            this.redline_btn.UseVisualStyleBackColor = true;
            this.redline_btn.Click += new System.EventHandler(this.redline_btn_Click);
            // 
            // mark_btn
            // 
            this.mark_btn.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mark_btn.Location = new System.Drawing.Point(580, 251);
            this.mark_btn.Name = "mark_btn";
            this.mark_btn.Size = new System.Drawing.Size(96, 36);
            this.mark_btn.TabIndex = 229;
            this.mark_btn.Tag = "0";
            this.mark_btn.Text = "启动光刻";
            this.mark_btn.UseVisualStyleBackColor = true;
            this.mark_btn.Click += new System.EventHandler(this.mark_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 230;
            this.label1.Text = "选择光刻模板文件：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 230;
            this.label2.Text = "模板预览：";
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonClose.Location = new System.Drawing.Point(580, 306);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(96, 36);
            this.buttonClose.TabIndex = 229;
            this.buttonClose.Tag = "0";
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // buttonInit
            // 
            this.buttonInit.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonInit.Location = new System.Drawing.Point(580, 86);
            this.buttonInit.Name = "buttonInit";
            this.buttonInit.Size = new System.Drawing.Size(96, 36);
            this.buttonInit.TabIndex = 231;
            this.buttonInit.Text = "初始化";
            this.buttonInit.UseVisualStyleBackColor = true;
            this.buttonInit.Click += new System.EventHandler(this.buttonInit_Click);
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 433);
            this.Controls.Add(this.buttonInit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.mark_btn);
            this.Controls.Add(this.redline_btn);
            this.Controls.Add(this.buttonPreview);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.textBoxFileName);
            this.Name = "Debug";
            this.Text = "Debug";
            this.Load += new System.EventHandler(this.Debug_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.TextBox textBoxFileName;
        public System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.Button redline_btn;
        private System.Windows.Forms.Button mark_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonInit;
    }
}