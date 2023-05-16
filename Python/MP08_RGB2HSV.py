import cv2            # sử dụng thư viện xử lý ảnh OpenV cho Python
from PIL import Image # Thư viện xử lý ảnh Pillow hỗ trợ nhiều định dạng ảnh
import numpy as np    # Thư viện toán học, đặc biệt là các tính toán ma trận

# Khai báo đường dẫn file hình
filehinh = r'lena.png'

# Đọc ảnh màu dùng thư viện OpenCV
img = cv2.imread(filehinh, cv2.IMREAD_COLOR)

# Đọc ảnh dùng thư viện Pillow. Ảnh PIL này chúng ta sẽ dùng để thực hiện các tác vụ xử lý & tính toán thay vì dùng OpenCV
imgPIL = Image.open(filehinh)

# Tạo một ảnh có cùng kích thước và mode với ảnh imgPIL
# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Hue
Hue = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Saturation
Saturation = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang Intensity
Value = Image.new(imgPIL.mode, imgPIL.size)

# Ảnh này dùng để chứa kết quả chuyển đổi RGB sang hình HSI
HSV = Image.new(imgPIL.mode, imgPIL.size)

# lấy kích thước của ảnh
height = imgPIL.size[1]
width = imgPIL.size[0]

# Mỗi hình là 1 ma trận 2 chiều nên sẽ dùng 2 vòng for
# để đọc hết tất cả điểm ảnh (pixel) có trong hình
for x in range(width):
    for y in range(height):
        # lấy giá trị điểm ảnh tại vị trí (x,y)
        #R = img[y,x,2]
        #G = img[y,x,1]
        #B = img[y,x,0]
        R, G, B = imgPIL.getpixel((x, y))

        # Dựa theo công thức trong sách để viết
        # t1 là phần tử số của công thức
        t1 = ((R - G) + (R - B)) * 0.5

        # t2 là phần mẫu số của công thức tính theta
        t2 = np.sqrt((R - G)*(R - G) +(G - B)*(R - B))

        # Chú ý kết quả trả về của hàm Acos trong Python là radian
        theta = np.arccos(t1 / t2 )

        #================================================================================================
        # Công thức tính giá trị Hue
        H = 0

        # Nếu mà Blue <= Green thì Hue  = theta
        if (B <= G):
            H = theta
        else: # Còn nếu Blue > Green thì Hue tính theo công thức này
            H = 2 * np.pi - theta; # Do theta là radian tính ở trên nên thay vì dùng 360 thì ta dùng PI

        # Chuyển đổi H từ radian sang degree
        H = np.uint8(H * 180 / np.pi)

        #================================================================================================
        # Công thức tính giá trị kênh Saturation
        S = (1 - 3 * min(R, G, B) / (R + G + B))
        S = np.uint8(S * 255)
        # Do giá trị tính ra của S nằm trong khoảng [0, 1]
        # Để mà bitmap có thể hiện thị được thì phải convert S sang khoảng giá trị [0, 255] 
        # Công thức dưới đây giúp chuyển đổi từ rank [0, 1] sang rank [0, 255]
        # S = S * 255

        #===============================================================================================
        #Công thức tính kênh Intensity
        #Giống chuyển đổi RGB sang Grayscale
        V = np.uint8(max(R, G, B))

        # Cho hiển thị giá trị các kênh H-S-I tại các pictureBox
        # Ép kiểu dữ liệu của các giá trị H-S-I về kiểu byte để hình bitmap hiểu và hiện thị được
        Hue.putpixel((x, y ), (H, H, H))

        # Riêng giá trị S, hoặc mình có thể normalized như trên hoặc mình chỉ normalized lúc hiển thị này
        Saturation.putpixel((x, y ), (S, S, S))
        Value.putpixel((x, y ), (V, V, V))
        #Giá trị S chưa normalized dùng để kết hợp với các kênh H & I để tạo hình HSI
        # Thực ra hình HSI để hiển thị nhìn thấy, khi tính toán xử lý thì vẫn dùng 
        # từng kênh H-S-I riêng lẻ tùy mục đích khác nhau
        HSV.putpixel((x, y ), (V, S, H))
        
# Chuyển ảnh từ PIL sang OpenCV để hiển thị bằng OpenCV
KenhmauHue = np.array(Hue)
KenhmauSaturation = np.array(Saturation)
KenhmauValue = np.array(Value)
HinhHSV= np.array(HSV)

# Hiển thị hình dùng thư viện OpenCV
cv2.imshow("Hinh mau RGB goc co gai lena", img)
cv2.imshow("Kenh mau Hue", KenhmauHue)
cv2.imshow("Kenh mau Saturation", KenhmauSaturation)
cv2.imshow("Kenh mau Value", KenhmauValue)
cv2.imshow("Hinh HSV", HinhHSV)

# Bấm phím bất kì để đóng cửa sổ hiển thị hình
cv2.waitKey(0)

# Giải phóng bộ nhớ đã cập nhật cho các cửa sổ hiển thị hình
cv2.destroyAllWindows()