import cv2            # sử dụng thư viện xử lý ảnh OpenV cho Python
from PIL import Image # Thư viện xử lý ảnh Pillow hỗ trợ nhiều định dạng ảnh
import numpy as np    # Thư viện toán học, đặc biệt là các tính toán ma trận

# Khai báo đường dẫn file hình
filehinh = r'lena.png'

# Đọc ảnh dùng thư viện Pillow. Ảnh PIL này chúng ta sẽ dùng để thực hiện các tác vụ xử lý & tính toán thay vì dùng OpenCV
imgPIL = Image.open(filehinh)

#Đọc ảnh với opencv
imgGoc = cv2.imread(filehinh,cv2.IMREAD_COLOR)

# Ảnh này dùng để chứa kết quả sau khi smoothed
Mask3x3 = Image.new(imgPIL.mode, imgPIL.size)
Mask5x5 = Image.new(imgPIL.mode, imgPIL.size)
Mask7x7 = Image.new(imgPIL.mode, imgPIL.size)
Mask9x9 = Image.new(imgPIL.mode, imgPIL.size)

# lấy kích thước của ảnh
height = imgPIL.size[1]
width = imgPIL.size[0]
#print(type(width))
# Tiến hành quét các điểm ảnh trong hình gốc
# Lưu ý: để dễ lập trình thì khi quét bỏ qua các viền ngoài của ảnh
for x in range(1, width-1): #từ 1 đến width-1
    for y in range(1, height-1):

        # Khai báo biến tính sum
        # Khai báo kiểu int để có thể chứa được giá trị cộng dồn của các pixel
        Rs = 0
        Gs = 0
        Bs = 0
    
        # Tiến hành quét các điểm trong mặt nạ
        for i in range(x - 1, x + 2): 
            for j in range(y - 1, y + 2):

                # Tiến hành quét các điểm trong mặt nạ
                R, G, B = imgPIL.getpixel((i, j))

                # Cộng dồn giá trị điểm ảnh đó cho mỗi kênh R-G-B tương ứng
                Rs = Rs + R
                Gs = Gs + G
                Bs = Bs + B
        
        # Tính trung bình cộng cho mỗi kênh
        # Công thức 6.6.2 cho từng kênh màu R-G-B

        K = 3*3

        Rs = int(Rs/K)
        Gs = int(Gs/K)
        Bs = int(Bs/K)
                    
        # Set điểm ảnh đã làm mượt (mờ) vào Bitmap
        # SmoothedImage.SetPixel(x, y, Color.FromArgb(Rs, Gs, Bs));
        Mask3x3.putpixel((x, y ), (Bs, Gs, Rs))

# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
HinhMask3x3 = np.array(Mask3x3)
#Mask5x5 = np.array(Mask5x5)
#Mask7x7 = np.array(Mask7x7)
#Mask9x9 = np.array(Mask9x9)

# Hiển thị hình dùng thư viện OpenCV
cv2.imshow("Hinh goc", imgGoc)
cv2.imshow("Hinh lam muot voi mat na 3x3", HinhMask3x3)
#cv2.imshow("Hinh lam muot voi mat na 5x5", Mask5x5)
#cv2.imshow("Hinh lam muot voi mat na 7x7", Mask7x7)
#cv2.imshow("Hinh lam muot voi mat na 9x9", Mask9x9)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()