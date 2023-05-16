using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP08_RGB2HSV
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
            List<Bitmap> HSI = ChuyenDoiRGBSangHSV(Hinhgoc);

            // Hàm trên trả về 4 màu tương ứng thứ tự từ 0-3 là H-S-I-HSI
            // Hiển thị kết quả lên các picBox

            picBox_Hue.Image = HSI[0];  // kênh Hue
            picBox_Saturation.Image = HSI[1];  // kênh Saturation
            picBox_Value.Image = HSI[2];  // kênh Intensity
            picBox_HSV.Image = HSI[3];  // Hình HSI
        }

        // Viết hàm chuyển đổi RGB sang HSI
        public List<Bitmap> ChuyenDoiRGBSangHSV(Bitmap Hinhgoc)
        {

            // Tạo 1 list để chứa các kết quả sau chuyển đổi
            // Trong C#.Net kiểu List là 1 mảng nhưng hay là ở chỗ nó không bắt mình phải khai báo kích thước trước
            List<Bitmap> HSI = new List<Bitmap>();

            // Tạo 3 kênh màu để chứa hình của các kênh H-S-I
            Bitmap Hue = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            Bitmap Saturation = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            Bitmap Value = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);

            // Hình HSI (kết hợp cả 3 kênh H-S-I)
            Bitmap HSVimg = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);

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

                    // Dựa theo công thức trong sách để viết
                    // t1 là phần tử số của công thức
                    double t1 = ((R - G) + (R - B)) / 2;

                    // t2 là phần mẫu số của công thức tính theta
                    double t2 = Math.Sqrt((R - G) * (R - G) + (G - B) * (R - B));

                    // Chú ý kết quả trả về của hàm Acos trong C#.NET là radian
                    double theta = Math.Acos(t1 / t2);

                    //================================================================================================
                    // Công thức tính giá trị Hue
                    double H = 0;

                    // Nếu mà Blue <= Green thì Hue  = theta
                    if (B <= G)
                        H = theta;
                    else // Còn nếu Blue > Green thì Hue tính theo công thức này
                        H = 2 * Math.PI - theta; // Do theta là radian tính ở trên nên thay vì dùng 360 thì ta dùng PI

                    // Chuyển đổi H từ radian sang degree
                    H = H * 180 / Math.PI;

                    //================================================================================================
                    // Công thức tính giá trị kênh Saturation
                    double S = 1 - 3 * Math.Min(R, Math.Min(G, B)) / (R + G + B);

                    // Do giá trị tính ra của S nằm trong khoảng [0, 1]
                    // Để mà bitmap có thể hiện thị được thì phải convert S sang khoảng giá trị [0, 255] 
                    // Công thức dưới đây giúp chuyển đổi từ rank [0, 1] sang rank [0, 255]
                    // S = S * 255

                    //================================================================================================
                    // Công thức tính kênh Value
                    double V = Math.Max(R, Math.Max(G, B));

                    // Cho hiển thị giá trị các kênh H-S-V tại các pictureBox
                    // Ép kiểu dữ liệu của các giá trị H-S-V về kiểu byte để hình bitmap hiểu và hiện thị được
                    Hue.SetPixel(x, y, Color.FromArgb((byte)H, (byte)H, (byte)H));

                    // Riêng giá trị S, hoặc mình có thể normalized như trên hoặc mình chỉ normalized lúc hiển thị này
                    Saturation.SetPixel(x, y, Color.FromArgb((byte)(S * 255), (byte)(S * 255), (byte)(S * 255)));
                    Value.SetPixel(x, y, Color.FromArgb((byte)V, (byte)V, (byte)V));

                    // Giá trị S chưa normalized dùng để kết hợp với các kênh H & V để tạo hình HSV
                    // Thực ra hình HSI để hiển thị nhìn thấy, khi tính toán xử lý thì vẫn dùng 
                    // từng kênh H-S-I riêng lẻ tùy mục đích khác nhau
                    HSVimg.SetPixel(x, y, Color.FromArgb((byte)H, (byte)(S * 255), (byte)V));
                }
            // Ở trên có khai báo 1 mảng động để chứa kết quả các hình trả về sau chuyển đổi
            HSI.Add(Hue);
            HSI.Add(Saturation);
            HSI.Add(Value);
            HSI.Add(HSVimg);

            // Trả mảng hình kết quả sau chuyển đổi
            return HSI;
        }
    }
}
