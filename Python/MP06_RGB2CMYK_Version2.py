import cv2            # sử dụng thư viện xử lý ảnh OpenV cho Python
from PIL import Image # Thư viện xử lý ảnh Pillow hỗ trợ nhiều định dạng ảnh
import numpy as np    # Thư viện toán học, đặc biệt là các tính toán ma trận

# Khai báo đường dẫn file hình
filehinh = r'lena.png'

# Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)

# Đọc ảnh dùng thư viện Pillow. Ảnh PIL này chúng ta sẽ dùng để thực hiện các tác vụ xử lý & tính toán thay vì dùng OpenCV
imgPIL = Image.open(filehinh)

# Tạo một ảnh có cùng kích thước và mode với ảnh imgPIL
# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Cyan
Cyan = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Magenta
Magenta = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Yellow
Yellow = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Black
Black = Image.new(imgPIL.mode, imgPIL.size)

# lấy kích thước của ảnh
height = Cyan.size[1]
width = Cyan.size[0]

# Mỗi hình là 1 ma trận 2 chiều nên sẽ dùng 2 vòng for
# để đọc hết tất cả điểm ảnh (pixel) có trong hình
for x in range(width):
    for y in range(height):
        # lấy giá trị điểm ảnh tại vị trí (x,y)
        R, G, B = imgPIL.getpixel((x, y))
        
        #-----------------------------------Cyan---------------------------------------
        # Màu Cyan (xanh dương) là kết hợp giữa Green và Blue, vậy nên mình set kênh Red = 0
        Cyan.putpixel((x, y ), (B, G, 0))

        #-----------------------------------Magenta---------------------------------------
        # Màu Magenta (tím) là kết hợp giữa Red và Blue, nên set kênh Green = 0
        Magenta.putpixel((x, y ), (B, 0, R))

        #-----------------------------------Yellow---------------------------------------
        # Màu Yellow(vàng) là kết hợp giữa Red và Green, nên set kênh Blue = 0
        Yellow.putpixel((x, y ), (0, G, R))

        #-----------------------------------Black---------------------------------------
        # Màu Black(đen) đơn giản là lấy MIN(R, G, B)
        K = min(R, G, B)
        Black.putpixel((x, y), (K, K, K))


# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
KenhmauCyan = np.array(Cyan)
KenhmauMagenta = np.array(Magenta)
KehmauYellow = np.array(Yellow)
KenhmauBlack= np.array(Black)

# Hiển thị hình dùng thư viện OpenCV
cv2.imshow("Hinh mau RGB goc co gai lena", img)
cv2.imshow("Kenh mau Cyan", KenhmauCyan)
cv2.imshow("Kenh mau Magenta", KenhmauMagenta)
cv2.imshow("Kenh mau Yellow", KehmauYellow)
cv2.imshow("Kenh mau Black", KenhmauBlack)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()