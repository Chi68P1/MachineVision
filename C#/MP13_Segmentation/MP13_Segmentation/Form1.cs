using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP13_Segmentation
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
        double[] vectoA(Bitmap hinhgoc, int x1, int y1, int x2, int y2) // kiểu double là kiểu dữ liệu số thực nhưng chính xác hơn
        {
            //Tạo 1 ma trận dữ liệu là double do giá trị cộng dồn có thể rất lớn
            double[] A = new double[3];
            A[0] = A[1] = A[2] = 0;

            for (int x = x1; x <= x2; x++)
                for (int y = y1; y <= y2; y++)
                {
                    Color pixel = hinhgoc.GetPixel(x, y);
                    //Cộng dồn giá trị
                    A[0] += pixel.R;
                    A[1] += pixel.G;
                    A[2] += pixel.B;

                }
            //Tính toán diện tích của vùng vecto
            Double size = Math.Abs(x2 - x1) * Math.Abs(y2 - y1);
            A[0] /= size;
            A[1] /= size;
            A[2] /= size;
            //Giá trị chúng ta cần là mảng A
            return A;
        }

        public Bitmap Segmentation(Bitmap hinhgoc, double[] A, int nguong)
        {
            Bitmap ketqua = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    Color pixel = hinhgoc.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;
                    //Tính toán giá trị so sánh D
                    byte D = (byte)(Math.Sqrt((R - A[0]) * (R - A[0]) + (G - A[1]) * (G - A[1]) + (B - A[2]) * (B - A[2])));
                    //So sánh với nguong D < = nguong thì thuộc backgrond nên cho giá trị màu trắng
                    //Cho so sánh > trước do đa số pixel sẽ thuôc ảnh gốc nên chương trình
                    if (D > nguong)
                        ketqua.SetPixel(x, y, Color.FromArgb(R, G, B));
                    else
                        ketqua.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                }
            return ketqua;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Lấy các giá trị trên giao diện
            int x1 = (int)Convert.ToInt16(textBox1.Text);
            int y1 = (int)Convert.ToInt16(textBox3.Text);
            int x2 = (int)Convert.ToInt16(textBox2.Text);
            int y2 = (int)Convert.ToInt16(textBox4.Text);
            byte nguong = (byte)trackBar1.Value;
            //Xử lý hàm
            double[] A = vectoA(hinhgoc, x1, y1, x2, y2);
            pictureBox2.Image = Segmentation(hinhgoc, A, nguong);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //Lấy giá trị ngưỡng và show lên label
            byte nguong = (byte)trackBar1.Value;
            label9.Text = nguong.ToString();
        }
    }
}
