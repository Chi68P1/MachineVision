import cv2                       # sử dụng thư viện xử lý ảnh OpenV cho Python
from PIL import Image            # Thư viện xủ lý ảnh Pillow hỗ trợ nhiều định dạng ảnh
import numpy as np               # Thư viện toán học, đặc biệt là các tính toán ma trận
import matplotlib.pyplot as plt  # Thư viện vẽ biểu đồ

# =========================================================================================
# Thuật toán chuyển đổi ảnh màu RGB sang ảnh mức xám (Grayscale) dùng phương pháp Luminance
# =========================================================================================
def ChuyenDoiAnhMauRGBSangAnhXamLuminance(imgPIL):
    # Tạo một ảnh có cùng kích thước và mode với ảnh imgPIL
    # Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Grayscale dùng phương pháp Luminance
    luminance = Image.new(imgPIL.mode, imgPIL.size)

    # lấy kích thước của ảnh
    height = luminance.size[1]
    width = luminance.size[0]

    # Mỗi hình là 1 ma trận 2 chiều nên sẽ dùng 2 vòng for
    # để đọc hết tất cả điểm ảnh (pixel) có trong hình
    for x in range(width):
        for y in range(height):
            # lấy giá trị điểm ảnh tại vị trí (x,y)
            R, G, B = imgPIL.getpixel((x, y))
            
            # Công thức chuyển đổi ảnh màu RGb sang Grayscale dùng phương pháp Luminance
            gray = np.uint8(0.2126*R + 0.7152*G + 0.0722*B)

            # Gán giá trị mức xám vừa tính cho ảnh xám
            luminance.putpixel((x, y ), (gray, gray, gray))
    
    return luminance
# =========================================================================================
# End: ChuyenDoiAnhMauRGBSangAnhXamLuminance(HinhRGB)
# =========================================================================================

# =========================================================================================
# Tính histogram của ảnh xám
# =========================================================================================
def TinhHistogram(HinhXamPIL):
    # Mỗi pixel có giá trị từ 0 - 255, nên phải khai báo một mảng có 
    # 256 phần tử để chứa số đếm cảu các pixels có cùng giá trị
    his = np.zeros(256)

    # Kích thước ảnh
    w = HinhXamPIL.size[0]
    h = HinhXamPIL.size[1]

    for x in range (w):
        for y in range (h):
            # Lấy giá trị xám tại điểm (x, y)
            gR, gG, GB = HinhXamPIL.getpixel((x, y))

            # Giá trị gray tính ra cũng chính là phần tử thứ gray trong mảng his đã khai báo ở trên
            # sẽ tăng số đếm của phần tử thứ gray lên 1
            his[gR] += 1

    return his
# =========================================================================================
# End: TinhHistogram(HinhXamPIL)
# =========================================================================================

# =========================================================================================
# Vẽ biểu đồ Histogram dùng thư viên matplotlib
# =========================================================================================
def VeBieuDoHistogram(his):
    w = 6
    h = 4
    plt.figure("Biểu đồ histogram ảnh xám", figsize=(((w,h))), dpi=100)
    trucX = np.zeros(256)
    trucX = np.linspace(0, 255, 256) #(giá trị đầu, giá trị cuối, số phần tử)
    plt.plot(trucX, his, color ="orange")
    plt.title("Biểu đồ Histogram")
    plt.xlabel("Giá trị mức xám")
    plt.ylabel("Số điểm cùng giá trị mức xám")
    plt.show()
# =========================================================================================
# End: VeBieuDoHistogram(his)
# =========================================================================================

# =========================================================================================
# --------------------------------Begin: CHƯƠNG TRÌNH CHÍNH--------------------------------
# Lưu ý các hàm con phải khai báo trước khi chương trình chính gọi
# =========================================================================================
# Khai báo đường dẫn file hình
filehinh = r"bird_small.jpg"

# Đọc ảnh dùng thư viện PIL
imgPIL = Image.open(filehinh)

# Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)

# Chuyển sang ảnh mức xám
HinhXamPIL = ChuyenDoiAnhMauRGBSangAnhXamLuminance(imgPIL)

# Tính Histogram
his = TinhHistogram(HinhXamPIL)

# Chuyển ảnh PIL sang OpenCV để hiển thị bằng thư viện cv2
HinhXamCV = np.array(imgPIL)
cv2.imshow('Anh muc xam', img)

# Hiển thị biểu đồ Histogram
VeBieuDoHistogram(his)

# Bấm phím bất kì thoát chương trình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cấp phát cho các cửa sổ
cv2.destroyAllWindows()

# =========================================================================================
# --------------------------------End: CHƯƠNG TRÌNH CHÍNH--------------------------------
# =========================================================================================