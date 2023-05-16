namespace MP05_Histogram
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
            this.components = new System.ComponentModel.Container();
            this.pictureHinhgoc = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureHinhxamLuminance = new System.Windows.Forms.PictureBox();
            this.zGHistogram = new ZedGraph.ZedGraphControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHinhgoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHinhxamLuminance)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureHinhgoc
            // 
            this.pictureHinhgoc.Location = new System.Drawing.Point(12, 29);
            this.pictureHinhgoc.Name = "pictureHinhgoc";
            this.pictureHinhgoc.Size = new System.Drawing.Size(500, 381);
            this.pictureHinhgoc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureHinhgoc.TabIndex = 0;
            this.pictureHinhgoc.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ảnh gốc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 433);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ảnh mức xám";
            // 
            // pictureHinhxamLuminance
            // 
            this.pictureHinhxamLuminance.Location = new System.Drawing.Point(12, 453);
            this.pictureHinhxamLuminance.Name = "pictureHinhxamLuminance";
            this.pictureHinhxamLuminance.Size = new System.Drawing.Size(500, 381);
            this.pictureHinhxamLuminance.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureHinhxamLuminance.TabIndex = 2;
            this.pictureHinhxamLuminance.TabStop = false;
            // 
            // zGHistogram
            // 
            this.zGHistogram.Location = new System.Drawing.Point(536, 29);
            this.zGHistogram.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zGHistogram.Name = "zGHistogram";
            this.zGHistogram.ScrollGrace = 0D;
            this.zGHistogram.ScrollMaxX = 0D;
            this.zGHistogram.ScrollMaxY = 0D;
            this.zGHistogram.ScrollMaxY2 = 0D;
            this.zGHistogram.ScrollMinX = 0D;
            this.zGHistogram.ScrollMinY = 0D;
            this.zGHistogram.ScrollMinY2 = 0D;
            this.zGHistogram.Size = new System.Drawing.Size(933, 681);
            this.zGHistogram.TabIndex = 4;
            this.zGHistogram.UseExtendedPrintDialog = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 1055);
            this.Controls.Add(this.zGHistogram);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureHinhxamLuminance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureHinhgoc);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureHinhgoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHinhxamLuminance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureHinhgoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureHinhxamLuminance;
        private ZedGraph.ZedGraphControl zGHistogram;
    }
}

