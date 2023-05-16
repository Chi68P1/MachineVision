import cv2               #Sử dụng thư viện xử lí ảnh Opencv
from PIL import Image    #Thư viện xử lý ảnh PILLOW hỗ trợ nhiều định dạng ảnh
import numpy as np      #Thư viện toán học, đặc biệt là ma trận

# Tạo hàm moothingImage
def smoothingColorImage(imgPIL , Mask):

    a = (int) ((Mask - 1)/2)
    #Ví dụ mặt nạ 3x3 thì giá trị a sẽ bằng 1 suy ra bỏ viền ảnh 1 đơn vị

    # Tạo một biến chứa ảnh sau khi Smoothing
    smoothedImage = Image.new(imgPIL.mode,imgPIL.size)

    # Lấy kích thước ảnh
    width = smoothedImage.size[0]
    height = smoothedImage.size[1]

    # Tiến hành quét. Giả sử mặt nạ 3x3 thì range(1 , width -1)
    for x in range(a , width - a):
        for y in range(a , height - a):

            # Khai báo biến tính sum
            # Khai báo kiểu int để có thể chứa được giá trị cộng dồn của các pixel
            Rs = 0
            Gs = 0
            Bs = 0

            # Tiến hành quét các điểm trong mặt nạ
            for i in range( x - a , x + a + 1):
                for j in range( y - a , y + a + 1):

                    # Lấy giá trị điểm ảnh
                    R , G , B = imgPIL.getpixel((i,j))

                    # Cộng dồn giá trị điểm ảnh đó cho mỗi kênh R-G-B tương ứng
                    Rs = Rs + R
                    Gs = Gs + G
                    Bs = Bs + B

            # Tính trung bình cộng cho mỗi kênh
            # Công thức 6.6.2 cho từng kênh màu R-G-B
            Rs = np.uint8 ( Rs / (Mask * Mask) )
            Gs = np.uint8 ( Gs / (Mask * Mask) )
            Bs = np.uint8 ( Bs / (Mask * Mask) )

            smoothedImage.putpixel ( (x,y) , ( Bs, Gs, Rs) )
    return smoothedImage

#-----------------------------------Chương trình chính -----------------------------------------------------

# Khai báo đường dẫn file hình
filehinh = r'lena.png'

# Đọc ảnh màu dùng thư viện PIL. Ảnh PIL này sẽ dùng tính toán và xử lý thay vì dùng openCV
imgPIL = Image.open(filehinh)

#Đọc ảnh với opencv
imgGoc = cv2.imread(filehinh,cv2.IMREAD_COLOR)

# Sử dụng hàm smoothedImage để làm mượt ảnh
smoothedImage3 = smoothingColorImage(imgPIL,3) 
smoothedImage5 = smoothingColorImage(imgPIL,5) 
smoothedImage7 = smoothingColorImage(imgPIL,7) 
smoothedImage9 = smoothingColorImage(imgPIL,9) 

# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
showSmoothedImage3 = np.array(smoothedImage3)
showSmoothedImage5 = np.array(smoothedImage5)
showSmoothedImage7 = np.array(smoothedImage7)
showSmoothedImage9 = np.array(smoothedImage9)

# Hiển thị hình dùng thư viện OpenCV
cv2.imshow('Hinh goc',imgGoc)
cv2.imshow('Hinh sau khi lam muot 3x3',showSmoothedImage3)
cv2.imshow('Hinh sau khi lam muot 5x5',showSmoothedImage5)
cv2.imshow('Hinh sau khi lam muot 7x7',showSmoothedImage7)
cv2.imshow('Hinh sau khi lam muot 9x9',showSmoothedImage9)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()