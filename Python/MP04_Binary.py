import cv2            # sử dụng thư viện xử lý ảnh OpenV cho Python
from PIL import Image # Thư viện xủ lý ảnh Pillow hỗ trợ nhiều định dạng ảnh
import numpy as np    # Thư viện toán học, đặc biệt là các tính toán ma trận

# Khai báo đường dẫn file hình
filehinh = r'lena.png'

# Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)

# Đọc ảnh dùng thư viện Pillow. Ảnh PIL này chúng ta sẽ dùng để thực hiện các tác vụ xử lý & tính toán thay vì dùng OpenCV
imgPIL = Image.open(filehinh)

# Tạo một ảnh có cùng kích thước và mode với ảnh imgPIL
# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Binary
Binary = Image.new(imgPIL.mode, imgPIL.size)

# lấy kích thước của ảnh
height = Binary.size[1]
width = Binary.size[0]

# Thiết lập một giá trị ngưỡng để tính điểm ảnh nhị phân
Nguong = 100

# Mỗi hình là 1 ma trận 2 chiều nên sẽ dùng 2 vòng for
# để đọc hết tất cả điểm ảnh (pixel) có trong hình
for x in range(width):
    for y in range(height):
        # lấy giá trị điểm ảnh tại vị trí (x,y)
        R, G, B = imgPIL.getpixel((x, y))
         
        #-----------------------------------Luminance---------------------------------------
        # Công thức chuyển đổi ảnh màu RGb sang Grayscale dùng phương pháp Luminance
        gray = np.uint8(0.2126*R + 0.7152*G + 0.0722*B)

        # Xác định giá trị điểm nhị phân
        if (gray < Nguong):
            Binary.putpixel((x, y ), (0, 0, 0)) # màu đen
        else:
            Binary.putpixel((x, y ), (255, 255, 255)) # màu trắng

# Chuyển ảnh từ PIL sang OpenCv để hiển thị bằng OpenCV
Nhiphan = np.array(Binary)

# Hiển thị hình dùng thư viện OpenCV
cv2.imshow("Hinh mau RGB goc co gai lena", img)

cv2.imshow("Hinh Binary", Nhiphan)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()