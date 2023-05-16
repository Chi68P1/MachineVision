using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace MP05_Histogram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Load hình .jpg từ file
            Bitmap Hinhgoc = new Bitmap(@"C:\Users\Asus\source\bird_small.jpg");

            // Hiển thị tren pictureBox
            pictureHinhgoc.Image = Hinhgoc;

            // Tính hình mức xám theo phương pháp Luminance và cho hiển thị
            pictureHinhxamLuminance.Image = ChuyenhinhRGBsanghinhxamLuminance(Hinhgoc);

            // BÂY GIỜ GỌI CÁC HÀM ĐÃ VIẾT ĐỂ VẼ BIỂU ĐỒ HISTOGRAM
            //======================================================================

            // Tính histogram
            double[] histogram = TinhHistogram(ChuyenhinhRGBsanghinhxamLuminance(Hinhgoc)); // = Hinhmucxam

            // Chuyển đổi kiểu dữ liệu
            PointPairList points = ChuyenDoiHistogram(histogram);

            // Vẽ biểu đồ histogram và cho hiển thị
            zGHistogram.GraphPane = BieuDoHistogram(points);
            zGHistogram.Refresh();
        }

        /// <summary>
        /// Khai báo hàm tính toán mức xám theo phương pháp Linear Luminance
        /// </summary>
        /// <param name="Hinhgoc"></param>
        /// <returns></returns>
        /// 
        public Bitmap ChuyenhinhRGBsanghinhxamLuminance(Bitmap Hinhgoc)
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
                    byte gray = (byte)(0.2126 * R + 0.7152 * G + 0.0722 * B);

                    // Gán giá trị mức xám vừa tính vào hình mức xám
                    Hinhmucxam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            return Hinhmucxam;
        }

        // Tính Histogram của ảnh xám
        public double[] TinhHistogram(Bitmap Hinhmucxam)
        {
            // Mỗi pixel mức xám có giá trị từ 0 - 255, do vậy phải khai báo một mảng có 256 phần tử dùng để chứa số đếm
            // của các pixels có cùng mức xám trong ảnh.
            // Chúng ta nên dùng kiểu double vì tổng số đếm có thể rất lớn, phụ thuộc kích thước của ảnh.
            double[] histogram = new double[256];

            for (int x = 0; x < Hinhmucxam.Width; x++)
                for (int y = 0; y < Hinhmucxam.Height; y++)
                {
                    Color color = Hinhmucxam.GetPixel(x, y);
                    byte gray = color.R; //trong hình mức xám giá trị kênh R cũng giống G hoặc B

                    // Giá trị gray tính ra cũng chính là phần tử thứ gray trong Histogram đã khai báo
                    // Sẽ tăng số đếm của phần tử thứ gray lên 1
                    histogram[gray]++;
                }
            return histogram;
        }

        PointPairList ChuyenDoiHistogram(double[] histogram)
        {
            // PointPairList là kiểu dữ liệu của ZedGraph để vẽ biểu đồ
            PointPairList points = new PointPairList();

            for (int i = 0; i < histogram.Length; i++)
            {
                // i tương ứng trục nằm ngang từ 0 - 255
                // histogram[i] tương ứng trục đứng, là số pixels cùng mức xám
                points.Add(i, histogram[i]);   //[4,5,2,5,6,...]
            }
            return points;
        }

        // Thiết lập biểu đồ trong ZedGraph
        public GraphPane BieuDoHistogram(PointPairList histogram)
        {
            // GraphPane là đối tượng biểu đồ trong Zedgraph
            GraphPane gp = new GraphPane();
            gp.Title.Text = @"Biểu đồ Histogram"; // Tên của biểu đồ
            gp.Rect = new Rectangle(0, 0, 650, 500); // Khung chứa biểu đồ

            // Thiết lập trục ngang
            gp.XAxis.Title.Text = @"Giá trị mức xám của các điểm ảnh";
            gp.XAxis.Scale.Min = 0; // Nhỏ nhất là 0
            gp.XAxis.Scale.Max = 255; // Lớn nhất là 255
            gp.XAxis.Scale.MajorStep = 5; // mỗi bước chính là 5
            gp.XAxis.Scale.MinorStep = 1; // mỗi bước trong bước chính là 1

            // Tương tự thiết lập cho trục đứng
            gp.YAxis.Title.Text = @"Số điểm ảnh có cùng mức xám";
            gp.YAxis.Scale.Min = 0; 
            gp.YAxis.Scale.Max = 15000; // Số này phải lớn hơn kích thước ảnh (w x h)
            gp.YAxis.Scale.MajorStep = 5; // mỗi bước chính là 5
            gp.YAxis.Scale.MinorStep = 1; // mỗi bước trong bước chính là 1

            // Dùng biểu đồ dạng bar để biểu diễn histogram
            gp.AddBar("Histogram", histogram, Color.OrangeRed);

            return gp;
        }
    }
}
