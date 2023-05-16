using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP04_Nhiphan
{
    public partial class Form1 : Form
    {
        Bitmap Hinhgoc;
        public Form1()
        {
            InitializeComponent();
            // Load hình .jpg từ file
            // Cần phải chuyển biến này thành toàn cục (Global) để có thể sử dụng cho các hàm khác
            Hinhgoc = new Bitmap(@"C:\Users\Asus\source\lena_goc.png");

            // Hiển thị tren pictureBox
            pictureHinhgoc.Image = Hinhgoc;

            // Tính hình nhị phân và cho hiển thị
            pictureHinhBinary.Image = ChuyenhinhRGBsanghinhBinary(Hinhgoc,100);
        }

        public Bitmap ChuyenhinhRGBsanghinhBinary(Bitmap Hinhgoc, byte nguong)
        {
            Bitmap Hinhnhiphan = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            for (int x = 0; x < Hinhgoc.Width; x++)
                for (int y = 0; y < Hinhgoc.Height; y++)
                {
                    // Lấy giá trị điểm ảnh
                    Color pixel = Hinhgoc.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    // Tính giá trị mức xám cho điểm ảnh tại (x, y)
                    byte nhiphan = (byte)(0.2126 * R + 0.7152 * G + 0.0722 * B);

                    //Phân loại điểm ảnh sang nhị phân dựa vào giá trị ngưỡng
                    if (nhiphan < nguong)
                        nhiphan = 0; // màu đen
                    else
                        nhiphan = 255; // màu trắng

                    // Gán giá trị vừa tính vào hình binary
                    Hinhnhiphan.SetPixel(x, y, Color.FromArgb(nhiphan, nhiphan, nhiphan));
                }
            return Hinhnhiphan;
        }

        private void hScrollBarHinhnhiphan_ValueChanged(object sender, EventArgs e)
        {
            // Lấy giá trị ngưỡng từ giá trị của thanh cuộn
            // Do value của thanh cuộn trả về là int, trong khi ngưỡng là kiểu byte nên cần phải ép kiểu int về kiểu byte
            byte nguong = (byte)hScrollBarHinhnhiphan.Value;

            // Cho hiển thị giá trị ngưỡng
            labelThreshold.Text = nguong.ToString();

            // Gọi hàm tính ảnh nhị phân và cho hiển thị
            pictureHinhBinary.Image = ChuyenhinhRGBsanghinhBinary(Hinhgoc, nguong);
        }
    }
}
