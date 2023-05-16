import cv2               #Sử dụng thư viện xử lí ảnh Opencv
from PIL import Image    #Thư viện xử lý ảnh PILLOW hỗ trợ nhiều định dạng ảnh
import numpy as np      #Thư viện toán học, đặc biệt là ma trận

# Tạo hàm segmentation
def ColorImageSegmentation(imgPIL,x1,y1,x2,y2,nguong):

    # Tạo một biến chứa ảnh sau khi phân đoạn
    seg_Image = Image.new(imgPIL.mode,imgPIL.size)

    # Tạo biến chứa trung bình màu của các kênh
    aR = 0
    aG = 0
    aB = 0

    # Tiến hành quét các điểm xung quanh điểm cần tính trung bình
    for i in range( x1 , x2 + 1):
        for j in range( y1 , y2 + 1):

            # Lấy giá trị điểm ảnh
            R , G , B = imgPIL.getpixel((i,j)) # lấy giá trị theo từng hàng  

            aR = aR + R
            aG = aG + G
            aB = aB + B
            
        # Tính trung bình
    aR = np.uint8(aR / ( (x2 - x1)*(y2 - y1)))
    aG = np.uint8(aG / ( (x2 - x1)*(y2 - y1)))
    aB = np.uint8(aB / ( (x2 - x1)*(y2 - y1)))
    
    # Lấy kích thước ảnh
    width = seg_Image.size[0]
    height = seg_Image.size[1]

    # Tiến hành quét ảnh
    for x in range(0 , width):
        for y in range(0 , height):
            # Lấy giá trị điểm ảnh tại điểm cần phân đoạn
            Zr , Zg , Zb = imgPIL.getpixel((x,y))

            # Euclidean distance
            D = np.sqrt((Zr-aR)**2 +(Zg-aG)**2 +(Zb-aB)**2)

            # so sánh với ngưỡng
            if (D <= nguong):
                seg_Image.putpixel ((x,y),(255, 255, 255))
            else:
                seg_Image.putpixel ((x,y),(Zb, Zg, Zr))
    # trả về
    return seg_Image

#-----------------------------------Chương trình chính -----------------------------------------------------

# Khai báo đường dẫn file hình
filehinh = r'lena.png'

# Đọc ảnh màu dùng thư viện PIL. Ảnh PIL này sẽ dùng tính toán và xử lý thay vì dùng openCV
imgPIL = Image.open(filehinh)

#Đọc ảnh với opencv
imgGoc = cv2.imread(filehinh,cv2.IMREAD_COLOR)

# Sử dụng hàm ColorImageSegmentation để phân đoạn ảnh
# ColorImageSegmentation(imgPIL,x1,y1,x2,y2,nguong)
seg_Image = ColorImageSegmentation(imgPIL,80,400,150,500,45)     

# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
show_seg_Image = np.array(seg_Image)

cv2.imshow('Hinh goc',imgGoc)
cv2.imshow('Hinh sau khi phan doan',show_seg_Image)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()