using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP12_Sharpening
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

            // Chuyển đổi ảnh
            Bitmap SharpeningImage = ColorImageSharpening(Hinhgoc);

            // Cho hiển thị hình lên PicBox
            picBox_Sharpening.Image = SharpeningImage;
        }
        //===============================================================
        // Viết hàm ColorImageSharpening
        public Bitmap ColorImageSharpening(Bitmap Hinhgoc)
        {
            // Tạo một Bimap để chứa ảnh được làm nét
            Bitmap SharpeningImage = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);

            // Tiến hành quét các điểm ảnh trong hình gốc
            // Lưu ý: để dễ lập trình thì khi quét bỏ qua các viền ngoài của ảnh
            for (int x = 1; x < Hinhgoc.Width - 1; x++)
                for (int y = 1; y < Hinhgoc.Height - 1; y++)
                {
                    // Lấy giá trị điểm ảnh tại điểm cần làm nét
                    Color pixel = Hinhgoc.GetPixel(x, y);

                    float redChannelPoint = pixel.R;
                    float greenChannelPoint = pixel.G;
                    float blueChannelPoint = pixel.B;

                    // Biến chứa giá trị các kênh để set màu
                    float redValue = 0;
                    float greenValue = 0;
                    float blueValue = 0;

                    // Tạo ma trận mặt nạ lọc (2 chiều)
                    //c = 1
                    float[,] filterMaskMatrix = { { 0, -1, 0 }, { -1, 4, -1 }, { 0, -1, 0 } };
                    //float[,] filterMaskMatrix = { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };

                    //c = -1
                    //float[,] filterMaskMatrix = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                    //float[,] filterMaskMatrix = { { 1, 1, 1 }, { 1, -8, 1 }, { 1, 1, 1 } };

                    // Tiến hành quét các điểm trong mặt nạ

                    for (int i = x - 1; i <= x + 1; i++)
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            // Tiến hành quét các điểm trong mặt nạ
                            Color pixel0 = Hinhgoc.GetPixel(i, j);  // quét theo cột


                            // Cộng dồn các tích của các cặp điểm tương ứng của mặt nạ lọc và mặt nạ điểm ảnh (nhân tích chập)
                            redValue += filterMaskMatrix[i - x + 1, j - y + 1] * pixel0.R;
                            greenValue += filterMaskMatrix[i - x + 1, j - y + 1] * pixel0.G;
                            blueValue += filterMaskMatrix[i - x + 1, j - y + 1] * pixel0.B;
                        }

                    // Công thức 3.6.7 cho từng kênh màu R-G-B. Đảm bào giá trị nằm trong khoảng 0-255

                    redValue = Math.Max(0, Math.Min(255, redChannelPoint + redValue));
                    greenValue = Math.Max(0, Math.Min(255, greenChannelPoint + greenValue));
                    blueValue = Math.Max(0, Math.Min(255, blueChannelPoint + blueValue));

                    // Gán giá trị ảnh
                    SharpeningImage.SetPixel(x, y, Color.FromArgb((byte)redValue, (byte)greenValue, (byte)blueValue));
                }

            // Trả ảnh đã làm nét về cho hàm
            return SharpeningImage;

        }
    }
}
