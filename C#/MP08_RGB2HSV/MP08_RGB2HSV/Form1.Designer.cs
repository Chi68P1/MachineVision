namespace MP08_RGB2HSV
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
            this.label3 = new System.Windows.Forms.Label();
            this.picBox_Hue = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.picBox_Saturation = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.picBox_Value = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.picBox_HSV = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hinhgoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Saturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_HSV)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox_Hinhgoc
            // 
            this.picBox_Hinhgoc.Location = new System.Drawing.Point(29, 36);
            this.picBox_Hinhgoc.Name = "picBox_Hinhgoc";
            this.picBox_Hinhgoc.Size = new System.Drawing.Size(256, 256);
            this.picBox_Hinhgoc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Hinhgoc.TabIndex = 0;
            this.picBox_Hinhgoc.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hình gốc";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Kênh HUE";
            // 
            // picBox_Hue
            // 
            this.picBox_Hue.Location = new System.Drawing.Point(29, 345);
            this.picBox_Hue.Name = "picBox_Hue";
            this.picBox_Hue.Size = new System.Drawing.Size(256, 256);
            this.picBox_Hue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Hue.TabIndex = 4;
            this.picBox_Hue.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(341, 312);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Kênh SATURATION";
            // 
            // picBox_Saturation
            // 
            this.picBox_Saturation.Location = new System.Drawing.Point(344, 345);
            this.picBox_Saturation.Name = "picBox_Saturation";
            this.picBox_Saturation.Size = new System.Drawing.Size(256, 256);
            this.picBox_Saturation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Saturation.TabIndex = 6;
            this.picBox_Saturation.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(656, 312);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Kênh VALUE";
            // 
            // picBox_Value
            // 
            this.picBox_Value.Location = new System.Drawing.Point(659, 345);
            this.picBox_Value.Name = "picBox_Value";
            this.picBox_Value.Size = new System.Drawing.Size(256, 256);
            this.picBox_Value.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_Value.TabIndex = 8;
            this.picBox_Value.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(976, 312);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Hình HSV";
            // 
            // picBox_HSV
            // 
            this.picBox_HSV.Location = new System.Drawing.Point(979, 345);
            this.picBox_HSV.Name = "picBox_HSV";
            this.picBox_HSV.Size = new System.Drawing.Size(256, 256);
            this.picBox_HSV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_HSV.TabIndex = 10;
            this.picBox_HSV.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 628);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.picBox_HSV);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.picBox_Value);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.picBox_Saturation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.picBox_Hue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picBox_Hinhgoc);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hinhgoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Hue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Saturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_HSV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox_Hinhgoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picBox_Hue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picBox_Saturation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picBox_Value;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox picBox_HSV;
    }
}

