using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP11_Smoothing
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
            Bitmap SmoothedImage3x3 = ColorImageSmoothing3x3(Hinhgoc);
            Bitmap SmoothedImage5x5 = ColorImageSmoothing5x5(Hinhgoc);
            Bitmap SmoothedImage7x7 = ColorImageSmoothing7x7(Hinhgoc);
            Bitmap SmoothedImage9x9 = ColorImageSmoothing9x9(Hinhgoc);
            // Cho hiển thị hình lên PicBox
            picBox_Smoothed3.Image = SmoothedImage3x3;
            picBox_Smoothed5.Image = SmoothedImage5x5;
            picBox_Smoothed7.Image = SmoothedImage7x7;
            picBox_Smoothed9.Image = SmoothedImage9x9;
        }

        //===============================================================
        // Viết hàm ColorImageSmoothing3x3
        public Bitmap ColorImageSmoothing3x3(Bitmap Hinhgoc)
        {
            // Tạo một Bimap để chứa ảnh được làm mượt
            Bitmap SmoothedImage = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);


            // Tiến hành quét các điểm ảnh trong hình gốc
            // Lưu ý: để dễ lập trình thì khi quét bỏ qua các viền ngoài của ảnh
            for (int x = 1; x < Hinhgoc.Width - 1; x++)
                for (int y = 1; y < Hinhgoc.Height - 1; y++)
                {
                    // Khai báo biến tính sum
                    // Khai báo kiểu int để có thể chứa được giá trị cộng dồn của các pixel
                    int Rs = 0, Gs = 0, Bs = 0;

                    // Tiến hành quét các điểm trong mặt nạ
                    for (int i = x - 1; i <= x + 1; i++)
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            // Tiến hành quét các điểm trong mặt nạ
                            Color pixel = Hinhgoc.GetPixel(i, j);

                            byte R = pixel.R;
                            byte G = pixel.G;
                            byte B = pixel.B;

                            // Cộng dồn giá trị điểm ảnh đó cho mỗi kênh R-G-B tương ứng
                            Rs += R;
                            Gs += G;
                            Bs += B;
                        }

                    // Tính trung bình cộng cho mỗi kênh
                    // Công thức 6.6.2 cho từng kênh màu R-G-B

                    byte K = 3 * 3;
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);
                    
                    // Set điểm ảnh đã làm mượt (mờ) vào Bitmap
                    SmoothedImage.SetPixel(x, y, Color.FromArgb(Rs, Gs, Bs));
                }

            // Trảảnh đã làm mượt về cho hàm
            return SmoothedImage;
        }
        //===============================================================
        // Viết hàm ColorImageSmoothing5x5
        public Bitmap ColorImageSmoothing5x5(Bitmap Hinhgoc)
        {
            // Tạo một Bimap để chứa ảnh được làm mượt
            Bitmap SmoothedImage = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);


            // Tiến hành quét các điểm ảnh trong hình gốc
            // Lưu ý: để dễ lập trình thì khi quét bỏ qua các viền ngoài của ảnh
            for (int x = 2; x < Hinhgoc.Width - 2; x++)
                for (int y = 2; y < Hinhgoc.Height - 2; y++)
                {
                    // Khai báo biến tính sum
                    // Khai báo kiểu int để có thể chứa được giá trị cộng dồn của các pixel
                    int Rs = 0, Gs = 0, Bs = 0;

                    // Tiến hành quét các điểm trong mặt nạ
                    for (int i = x - 2; i <= x + 2; i++)
                        for (int j = y - 2; j <= y + 2; j++)
                        {
                            // Tiến hành quét các điểm trong mặt nạ
                            Color pixel = Hinhgoc.GetPixel(i, j);

                            byte R = pixel.R;
                            byte G = pixel.G;
                            byte B = pixel.B;

                            // Cộng dồn giá trị điểm ảnh đó cho mỗi kênh R-G-B tương ứng
                            Rs += R;
                            Gs += G;
                            Bs += B;
                        }

                    // Tính trung bình cộng cho mỗi kênh
                    // Công thức 6.6.2 cho từng kênh màu R-G-B

                    byte K = 5 * 5;
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    // Set điểm ảnh đã làm mượt (mờ) vào Bitmap
                    SmoothedImage.SetPixel(x, y, Color.FromArgb(Rs, Gs, Bs));
                }

            // Trảảnh đã làm mượt về cho hàm
            return SmoothedImage;
        }
        //===============================================================
        // Viết hàm ColorImageSmoothing5x5
        public Bitmap ColorImageSmoothing7x7(Bitmap Hinhgoc)
        {
            // Tạo một Bimap để chứa ảnh được làm mượt
            Bitmap SmoothedImage = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);


            // Tiến hành quét các điểm ảnh trong hình gốc
            // Lưu ý: để dễ lập trình thì khi quét bỏ qua các viền ngoài của ảnh
            for (int x = 3; x < Hinhgoc.Width - 3; x++)
                for (int y = 3; y < Hinhgoc.Height - 3; y++)
                {
                    // Khai báo biến tính sum
                    // Khai báo kiểu int để có thể chứa được giá trị cộng dồn của các pixel
                    int Rs = 0, Gs = 0, Bs = 0;

                    // Tiến hành quét các điểm trong mặt nạ
                    for (int i = x - 3; i <= x + 3; i++)
                        for (int j = y - 3; j <= y + 3; j++)
                        {
                            // Tiến hành quét các điểm trong mặt nạ
                            Color pixel = Hinhgoc.GetPixel(i, j);

                            byte R = pixel.R;
                            byte G = pixel.G;
                            byte B = pixel.B;

                            // Cộng dồn giá trị điểm ảnh đó cho mỗi kênh R-G-B tương ứng
                            Rs += R;
                            Gs += G;
                            Bs += B;
                        }

                    // Tính trung bình cộng cho mỗi kênh
                    // Công thức 6.6.2 cho từng kênh màu R-G-B

                    byte K = 7 * 7;
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    // Set điểm ảnh đã làm mượt (mờ) vào Bitmap
                    SmoothedImage.SetPixel(x, y, Color.FromArgb(Rs, Gs, Bs));
                }

            // Trảảnh đã làm mượt về cho hàm
            return SmoothedImage;
        }
        //===============================================================
        // Viết hàm ColorImageSmoothing5x5
        public Bitmap ColorImageSmoothing9x9(Bitmap Hinhgoc)
        {
            // Tạo một Bimap để chứa ảnh được làm mượt
            Bitmap SmoothedImage = new Bitmap(Hinhgoc.Width, Hinhgoc.Height);


            // Tiến hành quét các điểm ảnh trong hình gốc
            // Lưu ý: để dễ lập trình thì khi quét bỏ qua các viền ngoài của ảnh
            for (int x = 4; x < Hinhgoc.Width - 4; x++)
                for (int y = 4; y < Hinhgoc.Height - 4; y++)
                {
                    // Khai báo biến tính sum
                    // Khai báo kiểu int để có thể chứa được giá trị cộng dồn của các pixel
                    int Rs = 0, Gs = 0, Bs = 0;

                    // Tiến hành quét các điểm trong mặt nạ
                    for (int i = x - 4; i <= x + 4; i++)
                        for (int j = y - 4; j <= y + 4; j++)
                        {
                            // Tiến hành quét các điểm trong mặt nạ
                            Color pixel = Hinhgoc.GetPixel(i, j);

                            byte R = pixel.R;
                            byte G = pixel.G;
                            byte B = pixel.B;

                            // Cộng dồn giá trị điểm ảnh đó cho mỗi kênh R-G-B tương ứng
                            Rs += R;
                            Gs += G;
                            Bs += B;
                        }

                    // Tính trung bình cộng cho mỗi kênh
                    // Công thức 6.6.2 cho từng kênh màu R-G-B

                    byte K = 9 * 9;
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);

                    // Set điểm ảnh đã làm mượt (mờ) vào Bitmap
                    SmoothedImage.SetPixel(x, y, Color.FromArgb(Rs, Gs, Bs));
                }

            // Trảảnh đã làm mượt về cho hàm
            return SmoothedImage;
        }
        //===============================================================
    }
}
