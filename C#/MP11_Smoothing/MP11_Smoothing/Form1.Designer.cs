namespace MP11_Smoothing
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
            this.picBox_Smoothed3 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.picBox_Smoothed5 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.picBox_Smoothed7 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.picBox_Smoothed9 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hinhgoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Smoothed3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Smoothed5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Smoothed7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Smoothed9)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox_Hinhgoc
            // 
            this.picBox_Hinhgoc.Location = new System.Drawing.Point(24, 42);
            this.picBox_Hinhgoc.Name = "picBox_Hinhgoc";
            this.picBox_Hinhgoc.Size = new System.Drawing.Size(256, 256);
            this.picBox_Hinhgoc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Hinhgoc.TabIndex = 0;
            this.picBox_Hinhgoc.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ảnh gốc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 342);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "3x3";
            // 
            // picBox_Smoothed3
            // 
            this.picBox_Smoothed3.Location = new System.Drawing.Point(24, 371);
            this.picBox_Smoothed3.Name = "picBox_Smoothed3";
            this.picBox_Smoothed3.Size = new System.Drawing.Size(256, 256);
            this.picBox_Smoothed3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Smoothed3.TabIndex = 2;
            this.picBox_Smoothed3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(327, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "5x5";
            // 
            // picBox_Smoothed5
            // 
            this.picBox_Smoothed5.Location = new System.Drawing.Point(330, 371);
            this.picBox_Smoothed5.Name = "picBox_Smoothed5";
            this.picBox_Smoothed5.Size = new System.Drawing.Size(256, 256);
            this.picBox_Smoothed5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Smoothed5.TabIndex = 4;
            this.picBox_Smoothed5.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(628, 342);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "7x7";
            // 
            // picBox_Smoothed7
            // 
            this.picBox_Smoothed7.Location = new System.Drawing.Point(631, 371);
            this.picBox_Smoothed7.Name = "picBox_Smoothed7";
            this.picBox_Smoothed7.Size = new System.Drawing.Size(256, 256);
            this.picBox_Smoothed7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Smoothed7.TabIndex = 6;
            this.picBox_Smoothed7.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(951, 342);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "9x9";
            // 
            // picBox_Smoothed9
            // 
            this.picBox_Smoothed9.Location = new System.Drawing.Point(954, 371);
            this.picBox_Smoothed9.Name = "picBox_Smoothed9";
            this.picBox_Smoothed9.Size = new System.Drawing.Size(256, 256);
            this.picBox_Smoothed9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Smoothed9.TabIndex = 8;
            this.picBox_Smoothed9.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 681);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.picBox_Smoothed9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.picBox_Smoothed7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.picBox_Smoothed5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picBox_Smoothed3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picBox_Hinhgoc);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hinhgoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Smoothed3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Smoothed5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Smoothed7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Smoothed9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox_Hinhgoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picBox_Smoothed3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picBox_Smoothed5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picBox_Smoothed7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picBox_Smoothed9;
    }
}

