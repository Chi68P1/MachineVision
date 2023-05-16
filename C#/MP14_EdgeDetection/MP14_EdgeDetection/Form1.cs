using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP14_EdgeDetection
{
    public partial class Form1 : Form
    {
        //Biến toàn cục
        Bitmap hinhgoc;
        public Form1()
        {
            InitializeComponent();
            //Tạo biến chứa ảnh và load ảnh
            hinhgoc = new Bitmap(@"C:\Users\Asus\source\lena.png");
            pictureBox1.Image = hinhgoc;
        }
        //Xây dựng các hầm cần thiết
        //Chuyển đổi sang ảnh mức xám
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

        // Viết hàm EdgeDetection
        public Bitmap EdgeDetection(Bitmap Hinhgoc, int nguong)
        {
            // Tạo một Bimap để chứa ảnh được nhận dạng biên
            Bitmap EdgeDetectionImage = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);

            // Tiến hành quét các điểm ảnh trong hình gốc
            // Lưu ý: để dễ lập trình thì khi quét bỏ qua các viền ngoài của ảnh
            for (int x = 1; x < Hinhgoc.Width - 1; x++)
                for (int y = 1; y < Hinhgoc.Height - 1; y++)
                {
                    // Lấy giá trị điểm ảnh tại điểm cần làm nét
                    Color pixel = Hinhgoc.GetPixel(x, y);

                    float redChannelPoint = pixel.R; // R = G = B (ảnh mức xám) 

                    // Biến chứa giá trị các kênh để set màu
                    float gx = 0;
                    float gy = 0;

                    // Tạo ma trận sobel
                    // Tạo ma trận sobel
                    float[,] xMatrix = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1  } };
                    float[,] yMatrix = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };

                    // Tiến hành quét các điểm trong mặt nạ

                    for (int i = x - 1; i <= x + 1; i++)
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            // Tiến hành quét các điểm trong mặt nạ
                            Color pixel0 = Hinhgoc.GetPixel(i, j);  // quét theo cột


                            // Cộng dồn các tích của các cặp điểm tương ứng của mặt nạ lọc và mặt nạ điểm ảnh (nhân tích chập)
                            gx += xMatrix[i - x + 1, j - y + 1] * pixel0.R;
                            gy += yMatrix[i - x + 1, j - y + 1] * pixel0.R;
                        }

                    // Công thức 3.6.7 cho từng kênh màu R-G-B. Đảm bào giá trị nằm trong khoảng 0-255

                    float D = Math.Abs(gx) + Math.Abs(gy);

                    // So sánh ngưỡng
                    if (D > nguong)
                        EdgeDetectionImage.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    else
                        EdgeDetectionImage.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                }

            // Trả ảnh đã làm nét về cho hàm
            return EdgeDetectionImage;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Xử lý hàm
            byte nguong = (byte)trackBar1.Value;
            pictureBox2.Image = EdgeDetection(ChuyenhinhRGBsanghinhxamAverage(hinhgoc), nguong);
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            //Lấy giá trị ngưỡng và show lên label
            byte nguong = (byte)trackBar1.Value;
            label3.Text = nguong.ToString();
        }
    }
}
