import cv2 # thư viện xử lý ảnh
import numpy as np # thư viện tính toán
import matplotlib.pyplot as plt # vẽ đồ thị

width, height = 280, 560 # kích thước (sẽ sử dụng nhiều lần)

def create_table():  
        # tạo nền xanh
        img = np.zeros((height,width,3), dtype=np.uint8) # tạo mảng đại diện cho bàn
        img[:, :] = [255, 153, 51] # đặt màu nền là màu bàn bida
        img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB) # chuyển BGR sang RGB
            
        # vẽ 4 viền bàn
        cv2.line(img,(0,0),(width,0),(255,255,255)) 

        cv2.line(img,(0,0),(0,height),(255,255,255)) 

        cv2.line(img,(0,height),(width,height),(255,255,255)) 

        cv2.line(img,(width,0),(width,height),(255,255,255)) 

        return img # trả về hình ảnh bàn

# vẽ bóng, input hàm gồm ctrs: contours, bàn, bán kính, size = -1 để lấp đầy, img: hình để vẽ ctrs
def draw_balls(ctrs, background = create_table(), radius=7, size = -1, img = 0):

    K = np.ones((3,3),np.uint8) # ma trận filter (kenel bên AI)

    final = background.copy() # tạo hình ảnh trống dụa trên hình nền
    mask = np.zeros((560, 280),np.uint8) # tạo một mask(ảnh đen) có cùng kích thước với final
    
    for x in range(len(ctrs)): # x quét all contours có được
        
        # tìm tâm đường tròn bằng cách tính momment
        M = cv2.moments(ctrs[x]) # hàm tính các moment của đường viền x 
        cX = int(M['m10']/M['m00']) # tìm tâm theo trục X = cách chia tổng các moment theo trục X (m10) cho moment không gian (m00)
        cY = int(M['m01']/M['m00']) # trục Y tương tự
        
        # tìm màu trung bình trong contours
        mask[...]=0 # reset ma trận mask về 0 cho mỗi quả bóng
        cv2.drawContours(mask,ctrs,x,255,-1) # vẽ đường viền của contour thứ x lên hình ảnh mask với màu trắng và kín
        mask =  cv2.erode(mask,K,iterations = 3) # co mask 3 lần để giảm bớt viền xanh của bóng
             
        # thiết kế bóng
              
        # hình tròn đại diện bóng
        final = cv2.circle(final, # nơi vẽ bóng
                           (cX,cY), # vị trí bóng
                           radius, # kích thước
                           cv2.mean(img,mask), # color mean of each contour-color of each ball (src_img=transformed img)
                           size) # -1 lấp đầy bóng

        # thêm 1 vòng đen bên ngoài bóng (for cosmetics)
        final = cv2.circle(final, (cX,cY), radius, 0, 2) 
                 
    return final

# hàm lọc các contours tìm ra contours xài được
def filter_ctrs(ctrs, min_s = 100, max_s = 500, alpha = 3.75):  
    
    filtered_ctrs = [] # danh sách các contours lọc
    
    for x in range(len(ctrs)): # quét all contours
        
        rot_rect = cv2.minAreaRect(ctrs[x]) # tính kích thước hcm bao quanh contours
        w = rot_rect[1][0] # width
        h = rot_rect[1][1] # height
        area = cv2.contourArea(ctrs[x]) # diện tích contours

        if (h*alpha<w) or (w*alpha<h): # height . alpha < width và ngược lại suy ra không phải bóng, alpha tìm ra sau nhiều lần thử
            print('\nh',h,   'w',w)
            continue # do nothing

        if (area < min_s) or (area > max_s): # nếu diện tích quá lớn hoặc quá bé
            print('\area',area)
            continue # do nothing 
          
        # nếu không phải 2 điều kiện trên thì contours có thể là bóng
        filtered_ctrs.append(ctrs[x]) # add contours
    
    return filtered_ctrs

# vẽ hình chữ nhật cho bóng
def draw_rectangles(ctrs, img):
    
    output = img.copy()
    
    for i in range(len(ctrs)):
    
        rot_rect = cv2.minAreaRect(ctrs[i]) # tìm hcn nhỏ nhất bao quanh được bóng
        box = np.int64(cv2.boxPoints(rot_rect)) # lấy tọa độ góc (4 góc)
        cv2.drawContours(output,[box],0,(255,100,0),2) # draws box
        
    return output

#Code starts here =========================================================================================================================================

name = 'test3.mp4'

# first frame from the original video
cap = cv2.VideoCapture(name)
ret, frame = cap.read() # ret k cần xài, nó là cờ hiệu có đọc thành công hay không
frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB) # setting để hiển thị khung hình đầu tiên

# điểm đỏ cho ảnh gốc (sử dụng plot rồi mò)
table = frame.copy() # add points to pool table corners

cv2.circle(table, (461,195), 8,(255,0,0), -1) # top left
cv2.circle(table, (744,180), 8, (255,0,0), -1) # top right
cv2.circle(table, (304,603), 8, (255,0,0), -1) # bot left
cv2.circle(table, (926,603), 8, (255,0,0), -1) # bot right

# tạo 1 img màu đen
img = np.zeros((height,width,3), dtype=np.uint8)
new_img = img.copy() # add points to edges of img
cv2.circle(new_img, (0,0), 8, (255,0,0), -1) # top left
cv2.circle(new_img, (width,0), 8, (255,0,0), -1) # top right
cv2.circle(new_img, (0,height), 8, (255,0,0), -1) # bot left
cv2.circle(new_img, (width,height), 8, (255,0,0), -1) # bot right

plt.figure(figsize=(20,10))
plt.subplot(1,2,1)
plt.imshow(table)
plt.title('FROM')
plt.axis('off')

plt.subplot(1,2,2)
plt.imshow(new_img)
plt.title('TO')
plt.axis('off')
plt.show() 

# Tạo 2 mảng NumPy chứa tọa độ của 4 điểm góc.
pts1 = np.float32([ [461,195],[744,180],[304,603],[926,603] ]) # 4 corners points of ORIGINAL image
pts2 = np.float32([ [0,0],[width,0],[0,height],[width,height] ]) # 4 corners points of OUTPUT image

matrix = cv2.getPerspectiveTransform(pts1,pts2) # tìm ma trận chuyển đổi phối cảnh từ 2 tập hợp điểm 
transformed = cv2.warpPerspective(frame, matrix, (width,height)) # Chuyển đổi góc nhìn bằng warpPerspective (biến đổi phối cảnh)

plt.figure(figsize=(20,10))
plt.subplot(1,2,1)
plt.imshow(frame)
plt.title('first frame')
plt.axis('off')

plt.subplot(1,2,2)
plt.imshow(transformed)
plt.title('result of transformation')
plt.axis('off')
plt.show()

# làm mờ ảnh giúp loại bỏ nhiễu
transformed_blur = cv2.GaussianBlur(transformed,(5,5),2) # blur applied
# chuyển sang RGB để hiển thị với plt
blur_RGB = cv2.cvtColor(transformed_blur, cv2.COLOR_BGR2RGB) # rgb version

# miền màu HSV
lower = np.array([0, 60, 60]) 
upper = np.array([50, 255, 255])
# chuyển đổi hsv
hsv = cv2.cvtColor(blur_RGB, cv2.COLOR_RGB2HSV) 

# tìm các 
"""x = 43  # Tọa độ x của pixel
y = 191  # Tọa độ y của pixel
h = hsv[y, x, 0]  # Giá trị H của pixel
s = hsv[y, x, 1]  # Giá trị S của pixel
v = hsv[y, x, 2]  # Giá trị V của pixel
print('\n',h)
print('\n',s)
print('\n',v)"""

# tạo mask chứa các giá trị pixel tương ứng với các vùng màu giữa ngưỡng thấp và ngưỡng cao
mask = cv2.inRange(hsv, lower, upper) # table's mask

# áp dụng phép biến đổi đóng kết hợp khuếch đại và co rút để điền các lỗ hở
kernel = np.ones((5,5),np.uint8)
mask_closing = cv2.morphologyEx(mask, cv2.MORPH_CLOSE, kernel) 
mask_closing_show = cv2.cvtColor(mask_closing, cv2.COLOR_RGB2BGR) 
# đảo ngược mask để giữ lại vật, loại bỏ bàn
_,mask_inv = cv2.threshold(mask_closing,5,255,cv2.THRESH_BINARY_INV) # mask inv
mask_inv_show = cv2.cvtColor(mask_inv, cv2.COLOR_RGB2BGR) 

# plot edges, threshold, filter
plt.figure(figsize=(20,10))
plt.subplot(1,3,1)
plt.imshow(hsv)
plt.title('hsv')
plt.axis('off')

plt.subplot(1,3,2)
plt.imshow(mask_closing_show)
plt.title('table mask')
plt.axis('off')

plt.subplot(1,3,3)
plt.imshow(mask_inv_show) 
plt.title('masked objects')
plt.axis('off')
plt.show()

# tìm contours và lọc
ctrs, _ = cv2.findContours(mask_inv, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE) # create contours in filtered img
# draw contours before filter
detected_objects = draw_rectangles(ctrs, transformed) # đóng khung các vật thể

# draw contours after filter
ctrs_filtered = filter_ctrs(ctrs)
detected_objects_filtered = draw_rectangles(ctrs_filtered, transformed)

# kết quả
ctrs_color = draw_balls(ctrs_filtered, background=transformed ,img = transformed)

# plot results
plt.figure(figsize=(20,10))
plt.subplot(1,3,1)
plt.imshow(detected_objects)
plt.title('detected objects on table')
plt.axis('off')

plt.subplot(1,3,2)
plt.imshow(detected_objects_filtered)
plt.title('filtered detected objects on table')
plt.axis('off')

plt.subplot(1,3,3)
plt.imshow(ctrs_color)
plt.title('result')
plt.axis('off')
plt.show()

# tạo bàn chứa kết quả
final = draw_balls(ctrs_filtered, img = transformed) # gets contours and draws balls in their centers
 
plt.figure(figsize=(20,10))
plt.subplot(1,2,1)
plt.imshow(frame)
plt.title('original frame')
plt.axis('off')

plt.subplot(1,2,2)
plt.imshow(final)
plt.title('proceeding image')
plt.axis('off')
plt.show()