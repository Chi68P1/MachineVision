import numpy as np
from PIL import Image
import cv2


#Đọc ảnh với Image của PIL
imageGoc = Image.open('lena.png')
ketquaMode = imageGoc.mode
ketquaSize = imageGoc.size

matrix_1 = np.array([[1],
                    [1],
                    [1]])

matrix_2 = np.array([[1, 0.5, 1]])    

matrix_3 = np.array([[0, 1, 0],
                      [1, 1, 1],
                      [0, 1, 0]])                      


#tinh nhi phan
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

def addPadding(image, matrix):

    # Calculate the new dimensions for the image including the white border
    new_width = image.width + 2 * matrix.shape[1]
    new_height = image.height + 2 * matrix.shape[0]

    # Create a new image with the new dimensions
    new_image = Image.new(image.mode, (new_width, new_height), (255, 255, 255))

    # Paste the original image into the center of the new image to create the white border
    new_image.paste(image, (matrix.shape[1], matrix.shape[0]))

    return new_image

def erosionImage(imagePad, matrix):

    ketqua = Image.new(ketquaMode, ketquaSize)

    wide = int (matrix.shape[1]/2) # 0
    len = int (matrix.shape[0]/2) # 1

    #lấy kích thước cao và ngang của ảnh (0 la chieu ngang, 1 la chieu cao)
    width = ketqua.size[0]
    height = ketqua.size[1]

    for x in range(width):
        for y in range(height):
            mang = []
            # Quét trong khoanh vùng ma trận
            for i in range(x - wide, x + wide +1):
                for j in range(y - len, y+ len +1):
                    pixel, _, _ = imagePad.getpixel((i,j))
                    mang.append(pixel)
            if min(mang) == 0: ketqua.putpixel((x,y),(0,0,0))
            else: ketqua.putpixel((x,y),(255,255,255))

    return ketqua


def dilationImage(imagePad, matrix):

    ketqua = Image.new(ketquaMode, ketquaSize)

    wide = int (matrix.shape[1]/2) # 0
    len = int (matrix.shape[0]/2) # 1

    #lấy kích thước cao và ngang của ảnh (0 la chieu ngang, 1 la chieu cao)
    width = ketqua.size[0]
    height = ketqua.size[1]

    for x in range(width):
        for y in range(height):
            mang = []
            # Quét trong khoanh vùng ma trận
            for i in range(x - wide, x + wide +1):
                for j in range(y - len, y+ len +1):
                    pixel, _, _ = imagePad.getpixel((i,j))
                    mang.append(pixel)
            if max(mang) == 255: ketqua.putpixel((x,y),(255,255,255))
            else: ketqua.putpixel((x,y),(0,0,0))

    return ketqua
#=====================================================

anhXam = binaryImage(imageGoc)
padding = addPadding(anhXam,matrix_2)
final = erosionImage(padding, matrix_2)
final_2 = dilationImage(padding, matrix_2)

# Convert the image to a numpy array
a = np.array(final)
c = np.array(final_2)

b = np.array(padding)
# Display the image
cv2.imshow('a', a)
cv2.imshow('b', b)
cv2.imshow('c', c)
cv2.waitKey(0)
cv2.destroyAllWindows()