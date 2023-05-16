using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MP01_TachAnhMauRGB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Tạo một biến chứa đường dẫn nơi lưu hình màu RGB gốc cần xử lý.
            // Lưu ý: cần phải có kí tự @ phía trước để C#.NET biết là chuỗi Unicode để không bị báo lỗi.
            string filehinh = @"C:\Users\Asus\source\lena_goc.png";

            // Tạo một biến chứa hình bitmap được load từ file hình
            Bitmap hinhgoc = new Bitmap(filehinh);

            //Hiển thị hình gôc trong pixBox đã tạo
            picBox_Goc.Image = hinhgoc;

            //Khai báo 3 hình Bitmap để chứa 3 hình kênh R, G và B
            Bitmap red = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap green = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            Bitmap blue = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            // Mỗi hình là 1 ma trận 2 chiều nên sẽ dùng 2 vòng for
            // để đọc hêt các điểm ảnh (pixel) có trong hình

            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    // Đọc giá trị  pixel tại điểm ảnh có vị trí (x,y)
                    Color pixel = hinhgoc.GetPixel(x, y);

                    // Mỗi pixel chứa 4 thông tin gồm giá trị màu
                    // R, G, B và thông tin giá trị độ trong suốt A
                    byte R = pixel.R; // giá trị kênh RED
                    byte G = pixel.G; // giá trị kênh GREEN
                    byte B = pixel.B; // giá trị kênh BLUE
                    byte A = pixel.A; // độ trong suốt

                    // Set các giá trị pixel đọc được cho các hình chứa
                    // các kênh màu tương ứng R, G, B
                    red.SetPixel(x, y, Color.FromArgb(A, R, 0, 0));
                    green.SetPixel(x, y, Color.FromArgb(A, 0, G, 0));
                    blue.SetPixel(x, y, Color.FromArgb(A, 0, 0, B));
                }

            //hiển thị 3 hình kênh màu R, G, B tại các picBox đã tạo
            picBox_R.Image = red;
            picBox_G.Image = green;
            picBox_B.Image = blue;
        }
    }
}
