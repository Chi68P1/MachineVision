namespace MP07_RGB2HSI
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
            this.picBox_Hue = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.picBox_Saturation = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.picBox_Intensity = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.picBox_HSI = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hinhgoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Saturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Intensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_HSI)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox_Hinhgoc
            // 
            this.picBox_Hinhgoc.Location = new System.Drawing.Point(29, 42);
            this.picBox_Hinhgoc.Name = "picBox_Hinhgoc";
            this.picBox_Hinhgoc.Size = new System.Drawing.Size(256, 256);
            this.picBox_Hinhgoc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Hinhgoc.TabIndex = 0;
            this.picBox_Hinhgoc.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hình gốc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kênh Hue";
            // 
            // picBox_Hue
            // 
            this.picBox_Hue.Location = new System.Drawing.Point(29, 339);
            this.picBox_Hue.Name = "picBox_Hue";
            this.picBox_Hue.Size = new System.Drawing.Size(256, 256);
            this.picBox_Hue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Hue.TabIndex = 2;
            this.picBox_Hue.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 307);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Kênh Saturation";
            // 
            // picBox_Saturation
            // 
            this.picBox_Saturation.Location = new System.Drawing.Point(325, 339);
            this.picBox_Saturation.Name = "picBox_Saturation";
            this.picBox_Saturation.Size = new System.Drawing.Size(256, 256);
            this.picBox_Saturation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Saturation.TabIndex = 4;
            this.picBox_Saturation.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(615, 307);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Kênh Intensity";
            // 
            // picBox_Intensity
            // 
            this.picBox_Intensity.Location = new System.Drawing.Point(618, 339);
            this.picBox_Intensity.Name = "picBox_Intensity";
            this.picBox_Intensity.Size = new System.Drawing.Size(256, 256);
            this.picBox_Intensity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Intensity.TabIndex = 6;
            this.picBox_Intensity.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(917, 307);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Hình HSI";
            // 
            // picBox_HSI
            // 
            this.picBox_HSI.Location = new System.Drawing.Point(920, 339);
            this.picBox_HSI.Name = "picBox_HSI";
            this.picBox_HSI.Size = new System.Drawing.Size(256, 256);
            this.picBox_HSI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_HSI.TabIndex = 8;
            this.picBox_HSI.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 607);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.picBox_HSI);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.picBox_Intensity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.picBox_Saturation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picBox_Hue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picBox_Hinhgoc);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hinhgoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Saturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Intensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_HSI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox_Hinhgoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picBox_Hue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picBox_Saturation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picBox_Intensity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picBox_HSI;
    }
}

