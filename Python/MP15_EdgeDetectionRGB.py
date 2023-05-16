import cv2               #Sử dụng thư viện xử lí ảnh Opencv
from PIL import Image    #Thư viện xử lý ảnh PILLOW hỗ trợ nhiều định dạng ảnh
import numpy as np      #Thư viện toán học, đặc biệt là ma trận

#Tạo hàm EdgeDetectionRGB
def EdgeDetectionRGB(image):

    # Tạo ma trận để nhân tích chập
    matran_x = np.array([-1, -2, -1, 0, 0, 0, 1, 2, 1])
    matran_y = np.array([-1, 0, 1, -2, 0, 2, -1, 0, 1])

    ketqua = Image.new(image.mode,image.size)
    #Lấy kích thước ảnh
    width = image.size[0]
    height = image.size[1]
    #Mỗi hình là một ma trận 2 chiều nên dùng 2 vòng for quét tất cả pixel
    for x in range(1, width-1):
        for y in range(1, height-1):
            #Tạo 6 list rỗng để chứa các gradient
            gxR = []
            gxG = []
            gxB = []
            gyR = []
            gyG = []
            gyB = []

            for j in range (y-1,y+2):
                for i in range (x-1, x+2):
                    B, G, R = image.getpixel((i,j)) # lấy giá trị theo từng hàng  
                    gxR.append(R) # list
                    gyR.append(R)
                    gxG.append(G)
                    gyG.append(G)
                    gxB.append(B)
                    gyB.append(B)


            #Chuyển kiểu dữ liệu List sang mảng
            gxR = np.array(gxR)
            gyR = np.array(gyR)
            gxG = np.array(gxG)
            gyG = np.array(gyG)
            gxB = np.array(gxB)
            gyB = np.array(gyB)


            #Nhân tích chập
            gxR = np.dot(gxR,matran_x)
            gyR = np.dot(gyR,matran_y)
            gxG = np.dot(gxG,matran_x)
            gyG = np.dot(gyG,matran_y)
            gxB = np.dot(gxB,matran_x)
            gyB = np.dot(gyB,matran_y)

            gxx = gxR*gxR + gxG*gxG +  gxB*gxB
            gyy = gyR*gyR + gyG*gyG +  gyB*gyB
            gxy = gxR*gyR + gxG*gxG +  gxB*gyB

            theta = 0.5*np.arctan2(2*gxy,gxx-gyy)
            Fo = np.sqrt(0.5*((gxx+gyy)+(gxx-gyy)*np.cos(2*theta)+2*gxy*np.sin(2*theta)))

            if (Fo >= 130):
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
ketqua = EdgeDetectionRGB(hinhgoc)

# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
ketqua1 = np.array(ketqua)

cv2.imshow('Hinh goc',imgGoc)
cv2.imshow('Hinh sau khi nhan dang duong bien',ketqua1)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()