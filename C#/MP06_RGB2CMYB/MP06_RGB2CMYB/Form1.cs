using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP06_RGB2CMYB
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
            List<Bitmap> CMYK = ChuyenDoiRGBSangCMYK(Hinhgoc);

            // Hàm trên trả về 4 màu tương ứng thứ tự từ 0-3 là C-M-Y-K
            // Hiển thị kết quả lên các picBox

            picBox_Cyan.Image = CMYK[0];  // kênh màu Cyan
            picBox_Magenta.Image = CMYK[1];  // kênh màu Magenta
            picBox_Yellow.Image = CMYK[2];  // kênh màu Yellow
            picBox_Black.Image = CMYK[3];  // kênh màu Black
        }
        public List<Bitmap> ChuyenDoiRGBSangCMYK(Bitmap Hinhgoc)
        {
            /*=====================================================================================
            Các em lưu ý việc tính chuyển đổi hệ màu RGB sang CMYK và ngược lại đơn giản là sự 
            pha trộn màu của các kênh tương ứng, các em không cần phải dùng công thức tính gì 
            cho nặng chương trình tính toán cũng như làm suy giảm giá trị màu sau mỗi lần chuyển đổi:
                - Màu Cyan (xanh dương) là kết hợp giữa Green và Blue, vậy nên mình set kênh Red = 0
                - Màu Magenta (tím) là kết hợp giữa Red và Blue, nên set kênh Green = 0
                - Màu Yellow (vàng) là kết hợp giữa Red và Green, nên set kênh Blue = 0
                - Màu Black (đen) đơn giản là lấy MIN(R, G, B)
            ======================================================================================*/


            // Tạo 1 list để chứa 4 kênh ảnh tương ứng C-M-Y-K
            // Trong C#.Net kiểu List là 1 mảng nhưng hay là ở chỗ nó không bắt mình phải khai báo kích thước trước
            List<Bitmap> CMYK = new List<Bitmap>();

            // Tiếp theo là mình tạo ra 4 hình Bitmap, chưa chứa thông tin j hết chỉ là kích thước mỗi hình 
            // phải bằng kích thước của hình gốc để sau này việc tính toán chuyển đổi kênh màu được thực hiện đúng cho từng pixel
            // Mỗi kênh trong không gian màu CMYK được hiển thị bởi 1 hình bitmap
            Bitmap Cyan = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            Bitmap Magenta = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            Bitmap Yellow = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);
            Bitmap Black = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);

            // Mình tiến hành quét ảnh gốc 
            for (int x = 0; x < Hinhgoc.Width; x++)
                for (int y = 0; y < Hinhgoc.Height; y++)
                {
                    // Lấy điểm ảnh. Tại mỗi điểm ảnh (pixel) đang được quét mình tiến hành lấy thông tin tại điểm ảnh đó
                    // Do là ảnh gốc RGB nên mỗi pixel sẽ chứa thông tin của 3 kênh màu R, G, B tương ứng
                    Color pixel = Hinhgoc.GetPixel(x, y);
                    byte R = pixel.R; // Lưu ý mỗi kênh màu được biểu diễn bởi 8 bits = 1 byte 
                    byte G = pixel.G; // Nên dùng dữ liệu byte để chứa thông tin của từn kênh
                    byte B = pixel.B;

                    // Tiến hành phối màu
                    // Màu Cyan (xanh dương) là kết hợp giữa Green và Blue, vậy nên mình set kênh Red = 0
                    Cyan.SetPixel(x, y, Color.FromArgb(0, G, B));

                    // Màu Magenta (tím) là kết hợp giữa Red và Blue, nên set kênh Green = 0
                    Magenta.SetPixel(x, y, Color.FromArgb(R, 0, B));

                    // Màu Yellow(vàng) là kết hợp giữa Red và Green, nên set kênh Blue = 0
                    Yellow.SetPixel(x, y, Color.FromArgb(R, G, 0));

                    // Màu Black(đen) đơn giản là lấy MIN(R, G, B)
                    byte K = Math.Min(R, Math.Min(G, B)); // Do Min chỉ có 2 đối số đầu vào nên phải dùng 2 lần
                    Black.SetPixel(x, y, Color.FromArgb(K, K, K));
                }
            // Add các hình tương ứng các kênh màu C-M-Y-K vào List
            // Do List là kiểu dữ liệu mảng (Array) không cần biết trước kích thước nên mình có thể add các elements của List vào
            // mà không cần quan tâm nó có bị tràn hay không ( cái này là rất hay của C# so với C++)
            CMYK.Add(Cyan);
            CMYK.Add(Magenta);
            CMYK.Add(Yellow);
            CMYK.Add(Black);

            // Hàm trả về là 1 List 4 ảnh Bitmap tương ứng 4 kênh màu C-M-Y-K
            return CMYK;
        }
    }
}
