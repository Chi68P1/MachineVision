namespace MP04_Nhiphan
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
            this.pictureHinhgoc = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureHinhBinary = new System.Windows.Forms.PictureBox();
            this.labelThreshold = new System.Windows.Forms.Label();
            this.labelNguong = new System.Windows.Forms.Label();
            this.hScrollBarHinhnhiphan = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHinhgoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHinhBinary)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureHinhgoc
            // 
            this.pictureHinhgoc.Location = new System.Drawing.Point(50, 32);
            this.pictureHinhgoc.Name = "pictureHinhgoc";
            this.pictureHinhgoc.Size = new System.Drawing.Size(256, 256);
            this.pictureHinhgoc.TabIndex = 0;
            this.pictureHinhgoc.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Hình gốc";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(376, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "Binary";
            // 
            // pictureHinhBinary
            // 
            this.pictureHinhBinary.Location = new System.Drawing.Point(379, 32);
            this.pictureHinhBinary.Name = "pictureHinhBinary";
            this.pictureHinhBinary.Size = new System.Drawing.Size(256, 256);
            this.pictureHinhBinary.TabIndex = 8;
            this.pictureHinhBinary.TabStop = false;
            // 
            // labelThreshold
            // 
            this.labelThreshold.AutoSize = true;
            this.labelThreshold.Location = new System.Drawing.Point(376, 302);
            this.labelThreshold.Name = "labelThreshold";
            this.labelThreshold.Size = new System.Drawing.Size(72, 17);
            this.labelThreshold.TabIndex = 11;
            this.labelThreshold.Text = "Threshold";
            // 
            // labelNguong
            // 
            this.labelNguong.AutoSize = true;
            this.labelNguong.Location = new System.Drawing.Point(935, 512);
            this.labelNguong.Name = "labelNguong";
            this.labelNguong.Size = new System.Drawing.Size(0, 17);
            this.labelNguong.TabIndex = 12;
            // 
            // hScrollBarHinhnhiphan
            // 
            this.hScrollBarHinhnhiphan.LargeChange = 1;
            this.hScrollBarHinhnhiphan.Location = new System.Drawing.Point(379, 346);
            this.hScrollBarHinhnhiphan.Maximum = 255;
            this.hScrollBarHinhnhiphan.Name = "hScrollBarHinhnhiphan";
            this.hScrollBarHinhnhiphan.Size = new System.Drawing.Size(256, 39);
            this.hScrollBarHinhnhiphan.TabIndex = 13;
            this.hScrollBarHinhnhiphan.ValueChanged += new System.EventHandler(this.hScrollBarHinhnhiphan_ValueChanged);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(823, 427);
            this.Controls.Add(this.hScrollBarHinhnhiphan);
            this.Controls.Add(this.labelNguong);
            this.Controls.Add(this.labelThreshold);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pictureHinhBinary);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureHinhgoc);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureHinhgoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHinhBinary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureHinhgoc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureHinhBinary;
        private System.Windows.Forms.Label labelThreshold;
        private System.Windows.Forms.Label labelNguong;
        private System.Windows.Forms.HScrollBar hScrollBarHinhnhiphan;
    }
}

