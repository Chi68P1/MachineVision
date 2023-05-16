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

namespace MP05_HistogramRGB
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

            // BÂY GIỜ GỌI CÁC HÀM ĐÃ VIẾT ĐỂ VẼ BIỂU ĐỒ HISTOGRAM
            //======================================================================

            // Tính histogram
            double[,] histogram = TinhHistogram(Hinhgoc); 

            // Chuyển đổi kiểu dữ liệu
            List<PointPairList> points = ChuyenDoiHistogram(histogram);

            // Vẽ biểu đồ histogram và cho hiển thị
            zGHistogram.GraphPane = BieuDoHistogram(points);
            zGHistogram.Refresh();
        }

        // Tính Histogram của ảnh màu RGB
        public double[,] TinhHistogram(Bitmap bmp)
        {
            // Chúng ta dùng mảng 2 chiều để chứa thông tin histogram cho các kênh R G và B
            // 3 : là số kênh màu cần lưu
            // 256 : là cần 256 vị trí tương ứng giá trị màu từ 0 đến 255
            double[,] histogram = new double[3, 256];

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color color = bmp.GetPixel(x, y);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;

                    histogram[0, R]++; // histogram của kênh màu R [..,..,..,..,..,+1,..,..,..] R=5
                    histogram[1, G]++; // histogram của kênh màu G [..,..,..,..,..,+1,..,..,..] G=5
                    histogram[2, B]++; // histogram của kênh màu B [..,..,..,..,..,+1,..,..,..] B=5
                }
            return histogram; // trả mảng 2 chiều chứa thông tin histogram của R-G-B
        }

        List<PointPairList> ChuyenDoiHistogram(double[,] histogram)
        {
            // Dùng một mảng không cần khai báo trước số lượng phần tử để chứa các chuyển đổi
            List<PointPairList> points = new List<PointPairList>();
            PointPairList redPoints = new PointPairList(); // Chuyển đổi histogram kênh R
            PointPairList greenPoints = new PointPairList(); // Chuyển đổi histogram kênh G
            PointPairList bluePoints = new PointPairList(); // Chuyển đổi histogram kênh B

            for (int i = 0; i < 256; i++)
            {
                // i tương ứng trục nằm ngang từ 0 - 255
                // histogram[i] tương ứng trục đứng, là số pixels cúng mức xám
                redPoints.Add(i, histogram[0, i]); // Chuyển đổi cho kênh R
                greenPoints.Add(i, histogram[1, i]); // Chuyển đổi cho kênh G
                bluePoints.Add(i, histogram[2, i]); // Chuyển đổi cho kênh B
            }

            // Sau khi kết thúc vòng for thì thông tin histogram của các kênh R G B đã được
            // chuyển đổi thành công, giờ add các kênh vào mảng points để trả về cho hàm
            points.Add(redPoints);
            points.Add(greenPoints);
            points.Add(bluePoints);

            return points;
        }

        // Thiết lập biểu đồ trong ZedGraph
        public GraphPane BieuDoHistogram(List<PointPairList> points)
        {
            // GraphPane là đối tượng biểu đồ trong Zedgraph
            GraphPane gp = new GraphPane();
            gp.Title.Text = @"Biểu đồ Histogram"; // Tên của biểu đồ
            gp.Rect = new Rectangle(0, 0, 650, 500); // Khung chứa biểu đồ

            // Thiết lập trục ngang
            gp.XAxis.Title.Text = @"Giá trị màu của các điểm ảnh";
            gp.XAxis.Scale.Min = 0; // Nhỏ nhất là 0
            gp.XAxis.Scale.Max = 255; // Lớn nhất là 255
            gp.XAxis.Scale.MajorStep = 5; // mỗi bước chính là 5
            gp.XAxis.Scale.MinorStep = 1; // mỗi bước trong bước chính là 1

            // Tương tự thiết lập cho trục đứng
            gp.YAxis.Title.Text = @"Số điểm ảnh có cùng giá trị màu";
            gp.YAxis.Scale.Min = 0;
            gp.YAxis.Scale.Max = 15000; // Số này phải lớn hơn kích thước ảnh (w x h)
            gp.YAxis.Scale.MajorStep = 5; // mỗi bước chính là 5
            gp.YAxis.Scale.MinorStep = 1; // mỗi bước trong bước chính là 1

            // Dùng biểu đồ dạng bar để biểu diễn histogram
            gp.AddBar("Histogram's Red", points[0], Color.Red);
            gp.AddBar("Histogram's Green", points[1], Color.Green);
            gp.AddBar("Histogram's Blue", points[2], Color.Blue);

            return gp;
        }
    }
}
