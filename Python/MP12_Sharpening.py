import cv2               #Sử dụng thư viện xử lí ảnh Opencv
from PIL import Image    #Thư viện xử lý ảnh PILLOW hỗ trợ nhiều định dạng ảnh
import numpy as np      #Thư viện toán học, đặc biệt là ma trận

# Tạo hàm moothingImage
def sharpeningColorImage(imgPIL,n):

    # Tạo một biến chứa ảnh sau khi sharpening
    sharpenedImage = Image.new(imgPIL.mode,imgPIL.size)

    # Lấy kích thước ảnh
    width = sharpenedImage.size[0]
    height = sharpenedImage.size[1]

    # Tiến hành quét. Giả sử mặt nạ 3x3 thì range(1 , width -1)
    for x in range(1 , width - 1):
        for y in range(1 , height - 1):
            c = 0
            # Lấy giá trị điểm ảnh tại điểm cần làm nét
            redChannelPoint , greenChannelPoint , blueChannelPoint = imgPIL.getpixel((x,y))

            # Tạo ma trận mặt nạ lọc (1 chiều)
            if (n == 1):
                filterMaskMatrix = np.array([0, 1, 0, 1, -4, 1, 0, 1, 0])
                c = -1
            elif (n == 2):
                filterMaskMatrix = np.array([1, 1, 1, 1, -8, 1, 1, 1, 1])
                c = -1
            elif (n == 3):
                filterMaskMatrix = np.array([0, -1, 0, -1, 4, -1, 0, -1, 0])
                c = 1
            else:
                filterMaskMatrix = np.array([-1,-1, -1, -1, 8, -1, -1, -1, -1])
                c = 1

            # Tạo list rỗng để chứa các điểm xung quanh điểm càn làm nét
            redChannelPointList = []
            greenChannelPointList = []
            blueChannelPointList = []

            # Tiến hành quét các điểm xung quanh điểm cần làm nét
            for j in range( y - 1 , y + 2):
                for i in range( x - 1 , x + 2):

                    # Lấy giá trị điểm ảnh
                    R , G , B = imgPIL.getpixel((i,j)) # lấy giá trị theo từng hàng  

                    redChannelPointList.append(R)
                    greenChannelPointList.append(G)
                    blueChannelPointList.append(B)
            
            
            # Chuyển từ kiểu list sang mảng 1 chiều
            redChannelPointMatrix = np.array(redChannelPointList)
            greenChannelPointMatrix = np.array(greenChannelPointList)
            blueChannelPointMatrix = np.array(blueChannelPointList)

            # Tính giá trị Laplacian bằng cách nhân vô hướng (tích chập) 2 ma trận
            redLaplacian = np.dot(redChannelPointMatrix, filterMaskMatrix)
            greenLaplacian = np.dot(greenChannelPointMatrix, filterMaskMatrix)
            blueLaplacian = np.dot(blueChannelPointMatrix, filterMaskMatrix)

            # Công thức 3.6.7 cho từng kênh màu R-G-B
            redValue = redChannelPoint + c*redLaplacian       # kiểu dữ liệu int32
            greenValue = greenChannelPoint + c*greenLaplacian   
            blueValue = blueChannelPoint + c*blueLaplacian

            # gán giá trị màu 
            sharpenedImage.putpixel ((x,y),(blueValue, greenValue, redValue))
    return sharpenedImage 

#-----------------------------------Chương trình chính -----------------------------------------------------

# Khai báo đường dẫn file hình
filehinh = r'lena.png'

# Đọc ảnh màu dùng thư viện PIL. Ảnh PIL này sẽ dùng tính toán và xử lý thay vì dùng openCV
imgPIL = Image.open(filehinh)

#Đọc ảnh với opencv
imgGoc = cv2.imread(filehinh,cv2.IMREAD_COLOR)

# Sử dụng hàm sharpeningColorImage để làm mượt ảnh
# 1, 2 tương ứng c = -1
# 3, 4 tương ứng c = 1 
sharpenedImage = sharpeningColorImage(imgPIL,1)     

# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
showSharpenedImage = np.array(sharpenedImage)

cv2.imshow('Hinh goc',imgGoc)
cv2.imshow('Hinh sau khi lam net',showSharpenedImage)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()