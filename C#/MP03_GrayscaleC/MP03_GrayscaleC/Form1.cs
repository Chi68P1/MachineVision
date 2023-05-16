using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP03_GrayscaleC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Load hình .jpg từ file
            Bitmap Hinhgoc = new Bitmap(@"C:\Users\Asus\source\lena.png");
            
            // Hiển thị trên pictureBox
            pictureHinhgoc.Image = Hinhgoc;

            // Tính hình mức xám theo phương pháp Ligthness và cho hiển thị
            pictureHinhxamLightness.Image = ChuyenhinhRGBsanghinhxamLightness(Hinhgoc);

            // Tính hình mức xám theo phương pháp Average và cho hiển thị
            pictureHinhxamAverage.Image = ChuyenhinhRGBsanghinhxamAverage(Hinhgoc);

            // Tính hình mức xám theo phương pháp Average và cho hiển thị
            pictureHinhxamLuminance.Image = ChuyenhinhRGBsanghinhxamLuminance(Hinhgoc);
        }

        /// <summary>
        /// Khai báo hàm tính toán mức xám theo phương pháp Average
        /// </summary>
        /// <param name="Hinhgoc"></param>
        /// <returns></returns>

        public Bitmap ChuyenhinhRGBsanghinhxamAverage(Bitmap Hinhgoc)
        {
            Bitmap Hinhmucxam = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            for (int x = 0; x < Hinhgoc.Width; x++)
                for (int y = 0; y < Hinhgoc.Height; y++)
                {
                    // Lấy giá trị điểm ảnh
                    Color pixel = Hinhgoc.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    // Tính giá trị mức xám cho điểm ảnh tại (x, y)
                    byte gray = (byte)((R + G + B) / 3);

                    // Gán giá trị mức xám vừa tính vào hình mức xám
                    Hinhmucxam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            return Hinhmucxam;
        }

        /// <summary>
        /// Khai báo hàm tính toán mức xám theo phương pháp Ligthness
        /// </summary>
        /// <param name="Hinhgoc"></param>
        /// <returns></returns>
        /// 
        public Bitmap ChuyenhinhRGBsanghinhxamLightness(Bitmap Hinhgoc)
        {
            Bitmap Hinhmucxam = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            for (int x = 0; x < Hinhgoc.Width; x++)
                for (int y = 0; y < Hinhgoc.Height; y++)
                {
                    // Lấy giá trị điểm ảnh
                    Color pixel = Hinhgoc.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    // Tính giá trị mức xám cho điểm ảnh tại (x, y)
                    byte max = Math.Max(R, Math.Max(G, B));
                    byte min = Math.Min(R, Math.Min(G, B));
                    byte gray = (byte)((max + min) / 2);

                    // Gán giá trị mức xám vừa tính vào hình mức xám
                    Hinhmucxam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            return Hinhmucxam;
        }

        /// <summary>
        /// Khai báo hàm tính toán mức xám theo phương pháp Linear Luminance
        /// </summary>
        /// <param name="Hinhgoc"></param>
        /// <returns></returns>
        /// 
        public Bitmap ChuyenhinhRGBsanghinhxamLuminance(Bitmap Hinhgoc)
        {
            Bitmap Hinhmucxam = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            for (int x = 0; x < Hinhgoc.Width; x++)
                for (int y = 0; y < Hinhgoc.Height; y++)
                {
                    // Lấy giá trị điểm ảnh
                    Color pixel = Hinhgoc.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    // Tính giá trị mức xám cho điểm ảnh tại (x, y)
                    byte gray = (byte)(0.2126 * R + 0.7152 * G + 0.0722 * B);

                    // Gán giá trị mức xám vừa tính vào hình mức xám
                    Hinhmucxam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            return Hinhmucxam;
        }
    }
}
