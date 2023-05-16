import cv2            # sử dụng thư viện xử lý ảnh OpenV cho Python
from PIL import Image # Thư viện xử lý ảnh Pillow hỗ trợ nhiều định dạng ảnh
import numpy as np    # Thư viện toán học, đặc biệt là các tính toán ma trận

# Khai báo đường dẫn file hình
filehinh = r'jisoo.jpg'

# Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)

# Đọc ảnh dùng thư viện Pillow. Ảnh PIL này chúng ta sẽ dùng để thực hiện các tác vụ xử lý & tính toán thay vì dùng OpenCV
imgPIL = Image.open(filehinh)

# Tạo một ảnh có cùng kích thước và mode với ảnh imgPIL
# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Grayscale dùng phương pháp Average
average = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Grayscale dùng phương pháp Lightness
lightness = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Grayscale dùng phương pháp Luminance
luminance = Image.new(imgPIL.mode, imgPIL.size)

# lấy kích thước của ảnh
height = average.size[1]
width = average.size[0]

# Mỗi hình là 1 ma trận 2 chiều nên sẽ dùng 2 vòng for
# để đọc hết tất cả điểm ảnh (pixel) có trong hình
for x in range(width):
    for y in range(height):
        # lấy giá trị điểm ảnh tại vị trí (x,y)
        R, G, B = imgPIL.getpixel((x, y))
        
        #-----------------------------------Average---------------------------------------
        # Công thức chuyển đổi ảnh màu RGb sang Grayscale dùng phương pháp Average
        grayAverage = np.uint8((R + G + B)/3)
        # Gán giá trị mức xám vừa tính cho ảnh xám
        average.putpixel((x, y ), (grayAverage, grayAverage, grayAverage))

        #-----------------------------------Lightness---------------------------------------
        # Công thức chuyển đổi ảnh màu RGB sang Grayscale dùng phương pháp Lightness
        MAX = max(R, G, B)
        MIN = min(R, G, B)
        grayLightness = np.uint8((MAX + MIN)/2)
        # Gán giá trị mức xám vừa tính cho ảnh xám
        lightness.putpixel((x, y ), (grayLightness, grayLightness, grayLightness))

        #-----------------------------------Luminance---------------------------------------
        # Công thức chuyển đổi ảnh màu RGb sang Grayscale dùng phương pháp Luminance
        grayLuminance = np.uint8(0.2126*R + 0.7152*G + 0.0722*B)
        # Gán giá trị mức xám vừa tính cho ảnh xám
        luminance.putpixel((x, y ), (grayLuminance, grayLuminance, grayLuminance))

# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
anhmucxamAverage = np.array(average)
anhmucxamLightness = np.array(lightness)
anhmucxamLuminance = np.array(luminance)

# Hiển thị hình dùng thư viện OpenCV
cv2.imshow("Hinh mau RGB goc co gai lena", img)
cv2.imshow("Hinh muc xam dung phuong phap Average", anhmucxamAverage)
cv2.imshow("Hinh muc xam dung phuong phap Lightness", anhmucxamLightness)
cv2.imshow("Hinh muc xam dung phuong phap Luminance", anhmucxamLuminance)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()