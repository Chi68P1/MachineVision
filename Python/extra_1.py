import cv2
from PIL import Image #thư viện PIllow hỗ trợ nhiều định dạng ảnh
import numpy as np 
import matplotlib.pyplot as plt

#tinh muc xam
def tinhmucxam(imgPIL):
    #phuong phap luminance
    luminance = Image.new(imgPIL.mode, imgPIL.size)

    #lấy kích thước cao và ngang của ảnh (0 la chieu ngang, 1 la chieu cao)
    width = luminance.size[0]
    height = luminance.size[1]

    for x in range(width):
        for y in range(height):
            r, g, b = imgPIL.getpixel((x,y))
            #chuyen sang muc xam
            gray_luminance = np.uint8(r*0.2126 + g*0.7152 + 0.0722*b)

            #nhét giá trị pixel vừa tính ra vào ảnh xám
            luminance.putpixel((x,y),(gray_luminance,gray_luminance,gray_luminance))
    return luminance


# Tính histogram của ảnh RGB
# =========================================================================================
def tinh_histogram(hinhxampil1):
    # Mỗi pixel có giá trị từ 0 - 255, nên phải khai báo một mảng có 
    # 256 phần tử để chứa số đếm cảu các pixels có cùng giá trị
    his = np.zeros(256)

    #kich thuoc anh //hinhxampil.size[0] 
    w = hinhxampil1.size[0]
    h = hinhxampil1.size[1]
    
    for x in range(w):
        for y in range(h):
            #lay giá trị điểm xám tại x, y
            gR, gG, gB = hinhxampil1.getpixel((x,y))
            his[gR]+= 1

    return his

# Vẽ biểu đồ Histogram dùng thư viên matplotlib
# =========================================================================================
def VeBieuDoHistogram(his):
    w = 5
    h = 4
    plt.figure('bieu do histogram anh xam', figsize=(((w,h))),dpi=100)
    trucX = np.zeros(256)
    trucX = np.linspace(0,255,256) #từ 0-255; tất cả 256 giá trị
    plt.plot(trucX,his,color = 'orange')
    plt.title('bieu do histogram')
    plt.xlabel('gia tri muc xam')
    plt.ylabel('so diem cung gia tri muc xam')
    plt.show()
# Tinh diem nguong (threshold)
#=========================================================================================
def optimal_threshold(h,t1,t2):
    #cut distribution in 3
    h1 = h[:t1]
    h2 = h[t1:t2]
    h3 = h[t2:]
    #compute the 2 centroids 
    if(np.sum(h1)==0 or np.sum(h2)==0 or np.sum(h3)==0):
        print("Trong 3 khoảng này có vùng không tồn tại điểm nào có giá trị pixel tương ứng với vùng đó. Chọn lại ngưỡng khác")

    else:
        #sẽ nhân từng phần tử trong "h1" với từng phần tử trong mảng "np.arange(0,t1)" để tìm ra tọa độ trung tâm (theo trục x)
        #theo công thức: Từng giá trị điểm ảnh xám nhân với từng tọa độ của x (0,t1) (1)
        #vì sao lại là (1)
        #vì theo toán học, muốn tìm ra tọa độ trung tâm ví dụ như trung tâm trục X, thì lấy giá trị của Y nhân cho từng tọa độ của X
        # sau đó chia cho tổng giá trị trong khoảng tọa độ đó, tương tụ với Y nhưng ngược lại
        #trong xử lí ảnh, thì tọa độ trung tâm này gọi là "average (mean) intensity values" tương ứng với khoảng được chia ra trong 
        #histogram (trang 742 chương 10 mục Basic global threshold)
        m1 = np.sum(  h1*np.arange(0,t1)   )  /  np.sum(h1) 
        m2 = np.sum(  h2*np.arange(t1,t2)  )  /  np.sum(h2)
        m3 = np.sum(  h3*np.arange(t2, len(h))  )  /  np.sum(h3) 

        #compute the new threshold
        t1_new = np.round((m1+m2)/2).astype(int)
        t2_new = np.round((m2+m3)/2).astype(int)

        print(t1_new,t2_new)

        if((t1_new != t1) and (t2_new != t2)): return optimal_threshold(h,t1_new,t2_new)  #điều kiện so sánh deltaT
    return t1_new, t2_new

#segmentation
def Segmentation(imgPIL,nguong1, nguong2):
    #tạo biến chứa ảnh 
    seg_Image = Image.new(imgPIL.mode,imgPIL.size)
    
    # Lấy kích thước ảnh
    width = seg_Image.size[0]
    height = seg_Image.size[1]

    # Tiến hành quét lấy các điểm ảnh
    for x in range(0 , width ):
        for y in range(0 , height ):
            Zr, Zb, Zg = imgPIL.getpixel((x,y))
            if (Zr <= nguong1):
                seg_Image.putpixel ((x,y),(0,0,0))
            elif (nguong1<Zr<=nguong2):
                seg_Image.putpixel ((x,y),(125,125,125))
            elif (Zr>nguong2):
                seg_Image.putpixel ((x,y),(255,255,255))
    return seg_Image


# --------------------------------Begin: CHƯƠNG TRÌNH CHÍNH--------------------------------
# Lưu ý các hàm con phải khai báo trước khi chương trình chính gọi
# =========================================================================================

#khai báo đường dẫn file hình
filehinh = r'lena.png'

#đọc ảnh màu sử dụng thư viện opencv
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)

#sử dụng PILLOW để xử lý tính toán thay cho opencv
imgPIL = Image.open(filehinh)

#tinh muc xam
luminance = tinhmucxam(imgPIL)
#tinh histogram
his = tinh_histogram(luminance)
#tinh threshold
t1, t2 = optimal_threshold(his,120,150) #14 to 227
#phan doan anh
seg = Segmentation(luminance,t1,t2)

# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
show_seg = np.array(seg)
anhmucxam3_luminance = np.array(luminance)

#hien thi bang OpenCV
cv2.imshow('luminance_gray', anhmucxam3_luminance)
cv2.imshow('Segmentation', show_seg)

#bieu do histogram
VeBieuDoHistogram(his)

#bam phim bat ky de tat cua so
cv2.waitKey(0)
#giai phong bo nho sau khi tat cua so
cv2.destroyAllWindows()