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
# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang kênh Y
KenhY = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang kênh Cr
KenhCr = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang kênh Cb
KenhCb = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang hình YCrCb
HinhYCrCb = Image.new(imgPIL.mode, imgPIL.size)

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
        Y = np.uint8(16 + 65.738*R/256 + 129.057*G/256 + 25.064*B/256)
        Cb = np.uint8(128 - 37.945*R/256 - 74.494*G/256 + 112.439*B/256)
        Cr = np.uint8(128 + 112.439*R/256 - 94.154*G/256 - 18.285*B/256)
        #===============================================================================================
        # Gán giá trị pixel cho các kênh
        KenhY.putpixel((x, y ), (Y, Y, Y))
        KenhCr.putpixel((x, y ), (Cr, Cr, Cr))
        KenhCb.putpixel((x, y ), (Cb, Cb, Cb))
        HinhYCrCb.putpixel((x, y ), (Cr, Cb, Y)) 

# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
KenhY = np.array(KenhY)
KenhCr = np.array(KenhCr)
KenhCb = np.array(KenhCb)
HinhYCrCb= np.array(HinhYCrCb)

# Hiển thị hình dùng thư viện OpenCV
cv2.imshow("Hinh mau RGB goc co gai lena", img)
cv2.imshow("Kenh X", KenhY)
cv2.imshow("Kenh Cr", KenhCr)
cv2.imshow("Kenh Cb", KenhCb)
cv2.imshow("Hinh XYZ", HinhYCrCb)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()