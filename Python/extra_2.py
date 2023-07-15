import numpy as np
from PIL import Image
import cv2

#Hàm chuyển ảnh xám
def tinhMucXam(image):
    #ví dụ hình 591x377 thì heght = 377, width = 591, channel = 3
    height, width, _ = image.shape
    # Khai báo biến để chứa hình xám
    ketqua = np.zeros((height, width, 3), np.uint8)
    
    for x in range (0, width):
        for y in  range(0, height):
            # Kênh màu R là 2, G là 1 và B là 0
            pixelValue =image[y, x, 2]*0.2126 + image[y, x, 1]*0.7152 + image[y, x, 0]*0.0722 

            ketqua[y, x, :] = pixelValue
    
    return  np.uint8(ketqua)

#Hàm chuyển miền giá trị
def reScale(mang, min_moi, max_moi):
    min_ban_dau = min(mang.flatten())
    max_ban_dau = max(mang.flatten())

    mang_chuan_hoa =( (mang - min_ban_dau)  / (max_ban_dau - min_ban_dau) ) * (max_moi - min_moi) + min_moi
    return  np.uint8 (mang_chuan_hoa)

#Hàm tính toán theo columns
def columnsFunction(image, matrix):

    height, width, _ = image.shape

    ketqua = np.zeros((height, width // 2, 3)).astype('float32')

    for x in range (0, width, 2):  # x từ 0 2 4 6 8 tới 254
        for y in  range(0, height): # y từ 0 1 2 tới 255

            finalValue = image[y, x, 2]*matrix[0] + image[y, x + 1, 2]*matrix[1]

            ketqua[y, x // 2, :] = finalValue
    
    return  reScale(ketqua, 0, 255)

#Hàm tính toán theo rows
def rowsFunction(image, matrix):

    height, width, _ = image.shape
    ketqua = np.zeros((height // 2, width, 3)).astype('float32')

    for x in range (0, width):  # x từ 0 1 2 tới 255
        for y in  range(0, height, 2): # y từ 0 2 4 tới 254

            finalValue = image[y, x, 2]*matrix[0] + image[y + 1, x, 2]*matrix[1]

            ketqua[y // 2, x, :] = finalValue
    
    return  reScale(ketqua, 0, 255)

#Hàm tính Wavelet trong 2 chiều
def twoDimensionWavelet(image):
    # B1: TÍNH THEO CỘT
    firstStep1 = columnsFunction(image, matrixWavelet)
    firstStep2 = columnsFunction(image, matrixScaling)
    # B2: TÍNH THEO HÀNG

    diagonal = rowsFunction(firstStep1, matrixWavelet)
 
    vectical = rowsFunction(firstStep1, matrixScaling)
    
    horizontal = rowsFunction(firstStep2, matrixWavelet)
    
    scale = rowsFunction(firstStep2, matrixScaling)

    return diagonal, vectical, horizontal, scale

#========================Chương trình chính===============================

#Tạo 2 ma trận cho Scale và Wavelet
matrixScaling = np.array([1/np.sqrt(2), 1/np.sqrt(2)]).astype('float32')
matrixWavelet = np.array([1/np.sqrt(2), -1/np.sqrt(2)]).astype('float32')

# Đọc ảnh màu dùng thư viện OpenCV và chuyển miền xám
img = cv2.imread("lena.png", cv2.IMREAD_COLOR)
imageXam = tinhMucXam(img)

#Tính toán các hình 2-D với 3 mức scale FWT
dia_Lv1, vec_Lv1, Hori_Lv1, Scale_Lv1 = twoDimensionWavelet(imageXam)
dia_Lv2, vec_Lv2, Hori_Lv2, Scale_Lv2 = twoDimensionWavelet(Scale_Lv1)

#Ghép các hình lại cho giống trong sách

# two-scale FWT
ghep1 = np.concatenate((Scale_Lv2, Hori_Lv2), axis=1)
ghep2 = np.concatenate((vec_Lv2, dia_Lv2), axis=1)
final2 = np.concatenate((ghep1, ghep2), axis=0)

# two-scale FWT
ghep1 = np.concatenate((final2, Hori_Lv1), axis=1)
ghep2 = np.concatenate((vec_Lv1, dia_Lv1), axis=1)
final1 = np.concatenate((ghep1, ghep2), axis=0)

cv2.imshow('Anh Xam', imageXam)
cv2.imshow('final', final1)

cv2.waitKey(0)
cv2.destroyAllWindows()