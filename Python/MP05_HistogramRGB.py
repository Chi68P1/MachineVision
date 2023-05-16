import cv2                       # sử dụng thư viện xử lý ảnh OpenV cho Python
from PIL import Image            # Thư viện xủ lý ảnh Pillow hỗ trợ nhiều định dạng ảnh
import numpy as np               # Thư viện toán học, đặc biệt là các tính toán ma trận
import matplotlib.pyplot as plt  # Thư viện vẽ biểu đồ

# =========================================================================================
# Tính histogram của ảnh RGB
# =========================================================================================
def TinhHistogram(imgPIL):
    # Mỗi pixel có giá trị từ 0 - 255, nên phải khai báo một mảng có 
    # 256 phần tử để chứa số đếm cảu các pixels có cùng giá trị
    his1 = np.zeros(256)
    his2 = np.zeros(256)
    his3 = np.zeros(256)
    # Kích thước ảnh
    w = imgPIL.size[0]
    h = imgPIL.size[1]

    for x in range (w):
        for y in range (h):
            # Lấy giá trị xám tại điểm (x, y)
            gR, gG, gB = imgPIL.getpixel((x, y))

            # Giá trị gR tính ra cũng chính là phần tử thứ gR trong mảng his đã khai báo ở trên
            # sẽ tăng số đếm của phần tử thứ gR lên 1
            his1[gR] += 1
            his2[gG] += 1
            his3[gB] += 1

    return his1,his2,his3
# =========================================================================================
# End: TinhHistogram(imgPIL)
# =========================================================================================

# =========================================================================================
# Vẽ biểu đồ Histogram dùng thư viên matplotlib
# =========================================================================================
def VeBieuDoHistogram(his1,his2,his3):
    w = 6
    h = 4
    plt.figure("Biểu đồ histogram ảnh RGB", figsize=(((w,h))), dpi=100)
    trucX = np.zeros(256)
    trucX = np.linspace(0, 255, 256) #(giá trị đầu, giá trị cuối, số phần tử)
    plt.plot(trucX, his1, color ="red")
    plt.plot(trucX, his2, color ="green")
    plt.plot(trucX, his3, color ="blue")
    plt.title("Biểu đồ Histogram")
    plt.xlabel("Giá trị điểm ảnh")
    plt.ylabel("Số điểm cùng giá trị")
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

# Hiển thị ảnh
cv2.imshow('Anh goc', img)

# Tính Histogram
his1, his2, his3 = TinhHistogram(imgPIL)

# Hiển thị biểu đồ Histogram
VeBieuDoHistogram(his1,his2,his3)

# Bấm phím bất kì thoát chương trình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cấp phát cho các cửa sổ
cv2.destroyAllWindows()

# =========================================================================================
# --------------------------------End: CHƯƠNG TRÌNH CHÍNH--------------------------------
# =========================================================================================