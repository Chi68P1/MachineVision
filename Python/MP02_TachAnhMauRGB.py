import cv2            # sử dụng thư viện xử lý ảnh OpenV cho Python
import numpy as np    # Thư viện toán học, đặc biệt là các tính toán ma trận

# Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread("jisoo.jpg", cv2.IMREAD_COLOR)

# lấy kích thước của ảnh
height, width, channel = img.shape

# Khai báo 3 biến để chứa hình 3 kênh R-G-B
red = np.zeros((height,width,3),np.uint8) # số 3 là 3 kênh R-G-B, mỗi kênh 8 bit
green = np.zeros((height,width,3),np.uint8) 
blue = np.zeros((height,width,3),np.uint8) 

# Ban đầu set zero cho tất cả điểm ảnh có trong 3 kênh trong mỗi hình
red[:] = [0,0,0]
green[:] = [0,0,0]
blue[:] = [0,0,0]

# Mỗi hình là 1 ma trận 2 chiều nên sẽ dùng 2 vòng for
# để đọc hết tất cả điểm ảnh (pixel) có trong hình
for x in range(width):
    for y in range(height):
        # lấy giá trị điểm ảnh tại vị trí (x,y)
        R = img[y,x,2]
        G = img[y,x,1]
        B = img[y,x,0]

        # Thiết lâp màu cho các kênh
        red[y,x,2] = R
        green[y,x,1] = G
        blue[y,x,0] = B

# Hiển thị hình dùng thư viện OpenCV
cv2.imshow("Hinh mau RGB goc co gai lena",img)
cv2.imshow("Kenh Red", red)
cv2.imshow("Kenh Green", green)
cv2.imshow("Kenh Blue", blue)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()