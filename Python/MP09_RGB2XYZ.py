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
# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang kênh X
KenhX = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang kênh Y
KenhY = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang kênh Z
KenhZ = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang hình XYZ
HinhXYZ = Image.new(imgPIL.mode, imgPIL.size)

# lấy kích thước của ảnh
height = imgPIL.size[1]
width = imgPIL.size[0]

# Mỗi hình là 1 ma trận 2 chiều nên sẽ dùng 2 vòng for
# để đọc hết tất cả điểm ảnh (pixel) có trong hình
for x in range(width):
    for y in range(height):
        # lấy giá trị điểm ảnh tại vị trí (x,y)
        #R = img[y,x,2]
        #G = img[y,x,1]
        #B = img[y,x,0]
        R, G, B = imgPIL.getpixel((x, y))
        
        # Tính giá trị từng kênh X_Y_Z
        X = np.uint8(0.4124564*R + 0.3575761*G + 0.1804375*B)
        Y = np.uint8(0.2126729*R + 0.7151522*G + 0.0721750*B)
        Z = np.uint8(0.0193339*R + 0.1191920*G + 0.9503041*B)
        #===============================================================================================
        # Gán giá trị pixel cho các kênh
        KenhX.putpixel((x, y ), (X, X, X))
        KenhY.putpixel((x, y ), (Y, Y, Y))
        KenhZ.putpixel((x, y ), (Z, Z, Z))
        HinhXYZ.putpixel((x, y ), (Z, Y, X)) # đảo Z Y X

# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
KenhX = np.array(KenhX)
KenhY = np.array(KenhY)
KenhZ = np.array(KenhZ)
HinhXYZ= np.array(HinhXYZ)

# Hiển thị hình dùng thư viện OpenCV
cv2.imshow("Hinh mau RGB goc co gai lena", img)
cv2.imshow("Kenh X", KenhX)
cv2.imshow("Kenh Y", KenhY)
cv2.imshow("Kenh Z", KenhZ)
cv2.imshow("Hinh XYZ", HinhXYZ)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()