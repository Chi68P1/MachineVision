namespace MP10_RGB2YCrCb
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
            this.picBox_Hinhgoc = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.picBox_Y = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.picBox_Cr = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.picBox_Cb = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.picBox_YCrCb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hinhgoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Cr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Cb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_YCrCb)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox_Hinhgoc
            // 
            this.picBox_Hinhgoc.Location = new System.Drawing.Point(42, 46);
            this.picBox_Hinhgoc.Name = "picBox_Hinhgoc";
            this.picBox_Hinhgoc.Size = new System.Drawing.Size(256, 256);
            this.picBox_Hinhgoc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Hinhgoc.TabIndex = 0;
            this.picBox_Hinhgoc.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hình gốc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kênh Y";
            // 
            // picBox_Y
            // 
            this.picBox_Y.Location = new System.Drawing.Point(42, 347);
            this.picBox_Y.Name = "picBox_Y";
            this.picBox_Y.Size = new System.Drawing.Size(256, 256);
            this.picBox_Y.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Y.TabIndex = 2;
            this.picBox_Y.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(337, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Kênh Cr";
            // 
            // picBox_Cr
            // 
            this.picBox_Cr.Location = new System.Drawing.Point(340, 347);
            this.picBox_Cr.Name = "picBox_Cr";
            this.picBox_Cr.Size = new System.Drawing.Size(256, 256);
            this.picBox_Cr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Cr.TabIndex = 4;
            this.picBox_Cr.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(639, 315);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Kênh Cb";
            // 
            // picBox_Cb
            // 
            this.picBox_Cb.Location = new System.Drawing.Point(642, 347);
            this.picBox_Cb.Name = "picBox_Cb";
            this.picBox_Cb.Size = new System.Drawing.Size(256, 256);
            this.picBox_Cb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Cb.TabIndex = 6;
            this.picBox_Cb.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(943, 315);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Hình YCrCb";
            // 
            // picBox_YCrCb
            // 
            this.picBox_YCrCb.Location = new System.Drawing.Point(946, 347);
            this.picBox_YCrCb.Name = "picBox_YCrCb";
            this.picBox_YCrCb.Size = new System.Drawing.Size(256, 256);
            this.picBox_YCrCb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_YCrCb.TabIndex = 8;
            this.picBox_YCrCb.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 644);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.picBox_YCrCb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.picBox_Cb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.picBox_Cr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picBox_Y);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picBox_Hinhgoc);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hinhgoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Cr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Cb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_YCrCb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox_Hinhgoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picBox_Y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picBox_Cr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picBox_Cb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picBox_YCrCb;
    }
}

