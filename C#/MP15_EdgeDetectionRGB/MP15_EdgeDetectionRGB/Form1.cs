using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP15_EdgeDetectionRGB
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

        // Viết hàm EdgeDetectionRGB
        public Bitmap EdgeDetectionRGB(Bitmap Hinhgoc, int nguong)
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

                    double redChannelPoint = pixel.R; // R = G = B (ảnh mức xám) 

                    // Biến chứa giá trị các kênh để set màu
                    double gxR = 0;
                    double gyR = 0;
                    double gxG = 0;
                    double gyG = 0;
                    double gxB = 0;
                    double gyB = 0;

                    // Tạo ma trận sobel
                    // Tạo ma trận sobel
                    double[,] xMatrix = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
                    double[,] yMatrix = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };

                    // Tiến hành quét các điểm trong mặt nạ

                    for (int i = x - 1; i <= x + 1; i++)
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            // Tiến hành quét các điểm trong mặt nạ
                            Color pixel0 = Hinhgoc.GetPixel(i, j);  // quét theo cột

                            // Cộng dồn các tích của các cặp điểm tương ứng của mặt nạ lọc và mặt nạ điểm ảnh (nhân tích chập)
                            gxR += xMatrix[i - x + 1, j - y + 1] * pixel0.R;
                            gyR += yMatrix[i - x + 1, j - y + 1] * pixel0.R;
                            gxG += xMatrix[i - x + 1, j - y + 1] * pixel0.G;
                            gyG += yMatrix[i - x + 1, j - y + 1] * pixel0.G;
                            gxB += xMatrix[i - x + 1, j - y + 1] * pixel0.B;
                            gyB += yMatrix[i - x + 1, j - y + 1] * pixel0.B;
                        }

                    double gxx = gxR * gxR + gxG * gxG + gxB * gxB;
                    double gyy = gyR * gyR + gyG * gyG + gyB * gyB;
                    double gxy = gxR * gyR + gxG * gxG + gxB * gyB;

                    double theta = 0.5 * Math.Atan2(2 * gxy,gxx - gyy); // Atan2 trả về giá trị -pi -> pi

                    double Fo = Math.Sqrt(0.5 * ((gxx + gyy) + (gxx - gyy) * Math.Cos(2 * theta) + 2 * gxy * Math.Sin(2 * theta)));

                    // So sánh ngưỡng
                    if (Fo >= nguong)
                        EdgeDetectionImage.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    else
                        EdgeDetectionImage.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                }

            // Trả ảnh đã làm nét về cho hàm
            return EdgeDetectionImage;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Xử lý hàm
            byte nguong = (byte)trackBar1.Value;
            pictureBox2.Image = EdgeDetectionRGB(hinhgoc, nguong);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //Lấy giá trị ngưỡng và show lên label
            byte nguong = (byte)trackBar1.Value;
            label4.Text = nguong.ToString();
        }
    }
}
