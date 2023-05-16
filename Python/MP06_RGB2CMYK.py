import cv2           # sử dụng thư viện xử lý ảnh OpenV cho Python
import numpy as np   # Thư viện toán học, đặc biệt là các tính toán ma trận

# Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread("lena.png", cv2.IMREAD_COLOR)

# lấy kích thước của ảnh
height, width, channel = img.shape

# Khai báo 3 biến để chứa hình 3 kênh R-G-B
Cyan = np.zeros((height,width,3),np.uint8) # số 3 là 3 kênh R-G-B, mỗi kênh 8 bit
Magenta = np.zeros((height,width,3),np.uint8) 
Yellow = np.zeros((height,width,3),np.uint8) 
Black = np.zeros((height,width,3),np.uint8) 

# Ban đầu set zero cho tất cả điểm ảnh có trong 3 kênh trong mỗi hình
#Cyan[:] = [0,0,0]
#Magenta[:] = [0,0,0]
#Yellow[:] = [0,0,0]
#Black[:] = [0,0,0]

# Mỗi hình là 1 ma trận 2 chiều nên sẽ dùng 2 vòng for
# để đọc hết tất cả điểm ảnh (pixel) có trong hình
for x in range(width):
    for y in range(height):
        # lấy giá trị điểm ảnh tại vị trí (x,y)
        R = img[y,x,2]
        G = img[y,x,1]
        B = img[y,x,0]

        # Thiết lâp màu cho các kênh Cyan
        Cyan[y,x,2] = 0
        Cyan[y,x,1] = G
        Cyan[y,x,0] = B

        # Thiết lâp màu cho các kênh Magenta
        Magenta[y,x,2] = R
        Magenta[y,x,1] = 0
        Magenta[y,x,0] = B

        # Thiết lâp màu cho các kênh Yellow
        Yellow[y,x,2] = R
        Yellow[y,x,1] = G
        Yellow[y,x,0] = 0

        # Thiết lâp màu cho các kênh Black
        K = min(R, G, B)
        Black[y,x,2] = K
        Black[y,x,1] = K
        Black[y,x,0] = K

# Hiển thị hình dùng thư viện OpenCV
cv2.imshow("Hinh mau RGB goc co gai lena",img)
cv2.imshow("Kenh Cyan", Cyan)
cv2.imshow("Kenh Magenta", Magenta)
cv2.imshow("Kenh Yellow", Yellow)
cv2.imshow("Kenh Black", Black)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()