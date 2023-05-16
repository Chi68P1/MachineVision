import cv2               #Sử dụng thư viện xử lí ảnh Opencv
from PIL import Image    #Thư viện xử lý ảnh PILLOW hỗ trợ nhiều định dạng ảnh
import numpy as np      #Thư viện toán học, đặc biệt là ma trận

#Tạo hàm làm ảnh xám
def Chuyendoianhxamaverage(hinhgoc):
    average = Image.new(hinhgoc.mode,hinhgoc.size)
    #Lấy kích thước ảnh
    width = average.size[0]
    height = average.size[1]
    #Mỗi hình là một ma trận 2 chiều nên dùng 2 vòng for quét tất cả pixel
    for x in range(width):
        for y in range(height):
            #Lấy giá trị điểm ảnh
            R, G, B = hinhgoc.getpixel((x,y))

            #Công thức chuyển đổi ảnh RGB sang mức xám sử dụng phương pháp average
            grayavg = np.uint8((R+G+B)/3)

            #Gán điểm ảnh mức xám
            average.putpixel((x,y),(grayavg,grayavg,grayavg))
    return average

#Tạo hàm EdgeDetection
def EdgeDetection(average):

    # Tạo ma trận để nhân tích chập
    matran_x = np.array([-1, -2, -1, 0, 0, 0, 1, 2, 1])
    matran_y = np.array([-1, 0, 1, -2, 0, 2, -1, 0, 1])

    ketqua = Image.new(average.mode,average.size)
    #Lấy kích thước ảnh
    width = average.size[0]
    height = average.size[1]
    #Mỗi hình là một ma trận 2 chiều nên dùng 2 vòng for quét tất cả pixel
    for x in range(1, width-1):
        for y in range(1, height-1):
            #Tạo 2 list rỗng
            gx = []
            #gy = []

            for i in range (y-1,y+2):
                for j in range (x-1, x+2):
                    R, G, B = average.getpixel((j,i)) # R = G = B # lấy giá trị theo từng hàng  
                    gx.append(R)
        
            #Chuyển kiểu dữ liệu List sang mảng
            gx1 = np.array(gx)
            gy1 = np.array(gx)

            #Nhân tích chập
            gx2 = np.dot(gx1,matran_x)
            gy2 = np.dot(gy1,matran_y)

            D = abs(gx2)  + abs(gy2)

            if (D > 130):
                ketqua.putpixel((x,y),(255,255,255))
            else:
                ketqua.putpixel((x,y),(0,0,0))
    return ketqua

#---------------------------Chương trình chính---------------------------------
# Khai báo đường dẫn file hình
filehinh = r'lena.png'

# Đọc ảnh màu dùng thư viện PIL. Ảnh PIL này sẽ dùng tính toán và xử lý thay vì dùng openCV
hinhgoc = Image.open(filehinh)

#Đọc ảnh với opencv để hiển thị ảnh
imgGoc = cv2.imread(filehinh,cv2.IMREAD_COLOR)

# Sử dụng hàm để xử lý ảnh
average = Chuyendoianhxamaverage(hinhgoc)     
ketqua = EdgeDetection(average)

# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
ketqua1 = np.array(ketqua)

cv2.imshow('Hinh goc',imgGoc)
cv2.imshow('Hinh sau khi nhan dang duong bien',ketqua1)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()