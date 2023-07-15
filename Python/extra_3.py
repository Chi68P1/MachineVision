import numpy as np
from PIL import Image
import cv2

#Đọc ảnh với Image của PIL
imageGoc = Image.open('lena.png')
# tạo 2 biến lưu thông số kết quả
ketquaMode = imageGoc.mode
ketquaSize = imageGoc.size

matrix_1 = np.ones((5,1))

matrix_2 = np.ones((1,5))   #[1,1,1,1,1]

matrix_3 = np.array([ [0, 0, 1, 0, 0],
                      [0, 1, 1, 1, 0],
                      [1, 1, 1, 1, 1],
                      [0, 1, 1, 1, 0],
                      [0, 0, 1, 0, 0]])                

# tinh nhi phan
def binaryImage(imgPIL):
    #phuong phap luminance
    binary = Image.new(imgPIL.mode, imgPIL.size)

    #lấy kích thước cao và ngang của ảnh (0 la chieu ngang, 1 la chieu cao)
    width = binary.size[0]
    height = binary.size[1]

    for x in range(width):
        for y in range(height):
            r, g, b = imgPIL.getpixel((x,y))

            if np.uint8(r*0.2126 + g*0.7152 + 0.0722*b) > 100: 
                binary.putpixel((x,y),(255,255,255))
            else:  binary.putpixel((x,y),(0,0,0))

    return binary

# thêm viền cho ảnh để đầu ra giữ nguyên kích thước
def addPadding(image, wide, hei):

    # Calculate the new dimensions for the image including the white border
    new_width = image.size[0] + 2*wide
    new_height = image.size[1] + 2*hei

    # Create a new image with the new dimensions
    new_image = Image.new(image.mode, (new_width, new_height))

    # Paste the original image into the center of the new image to create the white border
    new_image.paste(image, ( wide , hei ))

    return new_image

# co ảnh
def erosionImage(image, matrix):

    ketqua = Image.new(ketquaMode, ketquaSize)
    # do cột của ma trận sẽ là trục x của hình ảnh, tương tự với hàng
    wide = int (matrix.shape[1]/2) 
    hei = int (matrix.shape[0]/2)
    # thêm viền cho ảnh
    imagePad = addPadding(image, wide, hei)

    #lấy kích thước cao và ngang của ảnh (0 la chieu ngang, 1 la chieu cao)
    width = imagePad.size[0]
    height = imagePad.size[1]

    for x in range(0 + wide, width - wide): # 2 -> 257
        for y in range(0 + hei, height - hei):
            mang = []
            # Quét trong khoanh vùng ma trận
            for i in range(x - wide, x + wide +1): 
                for j in range(y - hei, y + hei +1):
                    pixel, _, _ = imagePad.getpixel((i,j))

                    # loại ma trận 1 dạng cột
                    if wide == 0:    value = pixel * matrix[j - y + hei , 0] # j - y + hei = 0 -> 2

                    # loại ma trận 2 dạng hàng
                    if (hei == 0) and (i != x):   value = pixel * matrix[0, i - x + wide]
                    elif (hei == 0) and (i == x):  continue # điểm gốc không  quan tâm

                    # loại ma trận 3 dạng vuông
                    if (wide != 0) and (hei !=0) and (matrix[j - y + hei, i - x + wide] == 1):    
                        value = pixel * matrix[j - y + hei, i - x + wide]

                    elif (wide != 0) and (hei !=0) and (matrix[j - y + hei, i - x + wide] == 0):  
                        continue

                    mang.append(value)

            if min(mang) == 0: ketqua.putpixel((x - wide,y - hei),(0,0,0)) # chỉ cần có 1 pixel  = 0
            else: ketqua.putpixel((x - wide,y - hei),(255,255,255))

    return ketqua

# giãn ảnh
def dilateImage(image, matrix):
    ketqua = Image.new(ketquaMode, ketquaSize)
    # do cột của ma trận sẽ là trục x của hình ảnh, tương tự với hàng

    wide = int (matrix.shape[1]/2) 
    hei = int (matrix.shape[0]/2)

    # thêm viền cho ảnh
    imagePad = addPadding(image, wide, hei)

    #lấy kích thước cao và ngang của ảnh (0 la chieu ngang, 1 la chieu cao)
    width = imagePad.size[0]
    height = imagePad.size[1]

    for x in range(0 + wide, width - wide):
        for y in range(0 + hei, height - hei):
            
            mang = []
            # Quét trong khoanh vùng ma trận
            for i in range(x - wide, x + wide +1):
                for j in range(y - hei, y + hei +1):
                    pixel, _, _ = imagePad.getpixel((i,j))

                    # loại ma trận 1 dạng cột
                    if wide == 0:    value = pixel * matrix[j - y + hei, 0]

                    # loại ma trận 2 dạng hàng
                    if (hei == 0) and (i != x):   value = pixel * matrix[0, i - x + wide]
                    elif (hei == 0) and (i == x):  continue

                    # loại ma trận 3 dạng vuông
                    if (wide != 0) and (hei !=0) and (matrix[j - y + hei, i - x + wide] == 1):    
                        value = pixel * matrix[j - y + hei, i - x + wide]

                    elif (wide != 0) and (hei !=0) and (matrix[j - y + hei, i - x + wide] == 0):  
                        continue

                    mang.append(value)

            if max(mang) == 255: ketqua.putpixel((x - wide,y - hei),(255,255,255)) # chỉ cần có 1 pixel  = 255
            else: ketqua.putpixel((x - wide,y - hei),(0,0,0)) # 0 -> 255

    return ketqua

# Opening là co xong giãn
def openingImage(imagePad, matrix):
    a = erosionImage(imagePad, matrix)
    final = dilateImage(a, matrix)
    return final

# Closing là giãn xong co
def closingImage(imagePad, matrix):
    a = dilateImage(imagePad, matrix)
    final = erosionImage(a, matrix)

    return final

anhXam = binaryImage(imageGoc)

matrix_use = matrix_3

final1 = erosionImage(anhXam, matrix_use)
final2 = dilateImage(anhXam, matrix_use)
final3 = openingImage(anhXam, matrix_use)
final4 = closingImage(anhXam, matrix_use)

# Convert the image to a numpy array
a = np.array(anhXam)
b = np.array(final1)
c = np.array(final2)
d = np.array(final3)
e = np.array(final4)
# Display the image
cv2.imshow('anh goc', a)
cv2.imshow('erosion', b)
cv2.imshow('dilate', c)
cv2.imshow('opening', d)
cv2.imshow('closing', e)
cv2.waitKey(0)
cv2.destroyAllWindows()