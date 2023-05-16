using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP10_RGB2YCrCb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Load hình 
            Bitmap Hinhgoc = new Bitmap(@"C:\Users\Asus\source\lena.png");

            // Cho hiển thị hình lên PicBox
            picBox_Hinhgoc.Image = Hinhgoc;

            // Hiển thị các kênh màu CMYK đã được chuyển đổi từ RGB
            // Gọi hàm chuyển đổi RGB sang CMYK
            List<Bitmap> HSI = ChuyenDoiRGBSangHSI(Hinhgoc);

            // Hàm trên trả về 4 màu tương ứng thứ tự từ 0-3 là H-S-I-HSI
            // Hiển thị kết quả lên các picBox

            picBox_Y.Image = HSI[0];  // kênh Hue
            picBox_Cr.Image = HSI[1];  // kênh Saturation
            picBox_Cb.Image = HSI[2];  // kênh Intensity
            picBox_YCrCb.Image = HSI[3];  // Hình HSI
        }

        // Viết hàm chuyển đổi RGB sang HSI
        public List<Bitmap> ChuyenDoiRGBSangHSI(Bitmap Hinhgoc)
        {

            // Tạo 1 list để chứa các kết quả sau chuyển đổi
            // Trong C#.Net kiểu List là 1 mảng nhưng hay là ở chỗ nó không bắt mình phải khai báo kích thước trước
            List<Bitmap> HSI = new List<Bitmap>();

            // Tạo 3 kênh màu để chứa hình của các kênh H-S-I
            Bitmap KenhY = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            Bitmap KenhCr = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            Bitmap KenhCb = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);

            // Hình HSI (kết hợp cả 3 kênh H-S-I)
            Bitmap HinhYCrCb = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);

            // Mình tiến hành quét ảnh gốc. Quét cột theo cột
            for (int x = 0; x < Hinhgoc.Width; x++)
                for (int y = 0; y < Hinhgoc.Height; y++)
                {

                    // Lấy thông tin điểm ảnh tại vị trí (x,y)
                    Color pixel = Hinhgoc.GetPixel(x, y);

                    // Phần tính H-S-I dùng kiểu double do quá trình tính toán các giá trị H-S-I
                    // kết quả trả về của các công thức đều là kiểu double (số thực)
                    double R = pixel.R;
                    double G = pixel.G;
                    double B = pixel.B;

                    // Tính giá trị từng kênh X_Y_Z
                    double Y = (16 + 65.738 * R / 256 + 129.057 * G / 256 + 25.064 * B / 256);
                    double Cr = (128 - 37.945 * R / 256 - 74.494 * G / 256 + 112.439 * B / 256);
                    double Cb = (128 + 112.439 * R / 256 - 94.154 * G / 256 - 18.285 * B / 256);
                    //===============================================================================================
                    // Gán giá trị pixel cho các kênh

                    KenhY.SetPixel(x, y, Color.FromArgb((byte)Y, (byte)Y, (byte)Y));
                    KenhCr.SetPixel(x, y, Color.FromArgb((byte)Cr, (byte)Cr, (byte)Cr));
                    KenhCb.SetPixel(x, y, Color.FromArgb((byte)Cb, (byte)Cb, (byte)Cb));
                    HinhYCrCb.SetPixel(x, y, Color.FromArgb((byte)Y, (byte)Cr, (byte)Cb));
                }
            // Ở trên có khai báo 1 mảng động để chứa kết quả các hình trả về sau chuyển đổi
            HSI.Add(KenhY);
            HSI.Add(KenhCr);
            HSI.Add(KenhCb);
            HSI.Add(HinhYCrCb);

            // Trả mảng hình kết quả sau chuyển đổi
            return HSI;
        }
    }
}
