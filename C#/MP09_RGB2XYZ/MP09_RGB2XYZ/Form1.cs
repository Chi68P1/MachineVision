using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP09_RGB2XYZ
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

            picBox_X.Image = HSI[0];  // kênh Hue
            picBox_Y.Image = HSI[1];  // kênh Saturation
            picBox_Z.Image = HSI[2];  // kênh Intensity
            picBox_XYZ.Image = HSI[3];  // Hình HSI
        }

        // Viết hàm chuyển đổi RGB sang HSI
        public List<Bitmap> ChuyenDoiRGBSangHSI(Bitmap Hinhgoc)
        {

            // Tạo 1 list để chứa các kết quả sau chuyển đổi
            // Trong C#.Net kiểu List là 1 mảng nhưng hay là ở chỗ nó không bắt mình phải khai báo kích thước trước
            List<Bitmap> HSI = new List<Bitmap>();

            // Tạo 3 kênh màu để chứa hình của các kênh H-S-I
            Bitmap KenhX = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            Bitmap KenhY = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            Bitmap KenhZ = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);

            // Hình HSI (kết hợp cả 3 kênh H-S-I)
            Bitmap HinhXYZ = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);

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
                    double X = (0.4124564 * R + 0.3575761 * G + 0.1804375 * B);
                    double Y = (0.2126729 * R + 0.7151522 * G + 0.0721750 * B);
                    double Z = (0.0193339 * R + 0.1191920 * G + 0.9503041 * B);
                    //===============================================================================================
                    // Gán giá trị pixel cho các kênh

                    KenhX.SetPixel(x, y, Color.FromArgb((byte)X, (byte)X, (byte)X));
                    KenhY.SetPixel(x, y, Color.FromArgb((byte)Y, (byte)Y, (byte)Y));
                    KenhZ.SetPixel(x, y, Color.FromArgb((byte)Z, (byte)Z, (byte)Z));
                    HinhXYZ.SetPixel(x, y, Color.FromArgb((byte)X, (byte)Y, (byte)Z));
                }
            // Ở trên có khai báo 1 mảng động để chứa kết quả các hình trả về sau chuyển đổi
            HSI.Add(KenhX);
            HSI.Add(KenhY);
            HSI.Add(KenhZ);
            HSI.Add(HinhXYZ);

            // Trả mảng hình kết quả sau chuyển đổi
            return HSI;
        }
    }
}
