import numpy as np
import tkinter as tk
from tkinter import *
from tkinter import messagebox
from tkinter import filedialog
from PIL import ImageTk, Image
import cv2
import matplotlib.pyplot as plt
import webbrowser
from random import randint

#==============Hàm cho cửa sổ chào mừng========================

# Đóng cửa sổ khi nhấn phím "Q" hoặc "q"
def close_top(event):
    if event.keysym.lower() == 'q' or event.keysym.lower() == 'Q':
        top.destroy()

#==============Hàm cho cửa sổ chính========================

def newWindow():
    # khởi tạo các hàm sẽ xài
    # ==========================Cho nút hiển thị hướng dẫn và tắt cửa sổ===================================
    def closeMain(event):
        if event.keysym.lower() == 'q' or event.keysym.lower() == 'Q':
            mainWin.destroy()
    
    def show_instructions():
        instructions = [
            "Bước 1: Ấn nút 'Select Video' chọn loại video",
            "Bước 2: Từ hình ảnh gốc hiện ra, nhập tọa độ 4 góc bàn (x,y)",
            "Bước 3: Đóng hình ảnh gốc, ấn nút 'Test Image' kiểm tra các tọa độ",
            "Bước 4: Đóng hình ảnh test, nhập tỷ lệ scale hình ảnh",
            "Bước 5: Ấn nút 'Proceed Video' để tiến hành xử lý",
            "Bước 5: Khi màn hình xuất hiện chữ 'Done' bạn có thể ấn 'Open Video để kiểm tra kết quả'"
        ]
        messagebox.showinfo("Hướng Dẫn", "\n".join(instructions))
    
    # ==========================Cho nút show frame đầu và nhập giá trị===================================
    def openFile():
        label.configure(text='')
        try:
            realTime = messagebox.askquestion("REALTIME", "Bạn có chọn sử dụng video từ camera không?")
            if realTime == "yes":
                filePath = 0
            else:
                filePath = filedialog.askopenfilename()

            filePathCopy = filePath
            cap = cv2.VideoCapture(filePath)
            _, frame = cap.read()
            cap.release()
            # Chuyển đổi frame thành hình ảnh RGB
            frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)     
            plt.imshow(frame_rgb)
            plt.title('Hình ảnh gốc')
            plt.show()
            showTestButton(filePathCopy, frame) # do cv2 sử dụng BGR nên ta sẽ lấy BGR

        except:         
            label.configure(text='Lỗi khi mở tệp vừa chọn, thử lại', foreground='#FF0000')

    def get_values(filePathCopy, frame):

        frame_test = frame
        input_str = entry1.get() + ',' + entry2.get() + ',' + entry3.get() + ',' + entry4.get()
        values = input_str.split(',')
        
        if (len(values) < 8): 
                label.configure(text='Bạn mới nhập ' + str (len(values)) + ' giá trị', foreground='#FF0000')

        try:
            int_values = [int(value) for value in values]
            for i in range(0,8,2):
                cv2.circle(frame_test, (int_values[i], int_values[i+1]), 8, (0,0,255), -1) # red
                center = (int_values[i], int_values[i+1])

                cv2.putText(frame_test, f"{center}", (int_values[i] - 20, int_values[i+1] - 20),
                            cv2.FONT_HERSHEY_SIMPLEX, 0.6, (0, 255, 0), 2)

            frame_test = cv2.resize(frame_test, (840,560))
            cv2.imshow('Hinh anh test toa do', frame_test)
            cv2.waitKey(0)
            cv2.destroyAllWindows()
            showProceedingButton(filePathCopy, int_values)

        except ValueError:
            label.configure(text='Vui lòng nhập các giá trị nguyên hợp lệ, phân tách bằng dấu phẩy', foreground='#FF0000')

    def showTestButton(filePathCopy, frame):
        test_b=Button(mainWin,text="Test Image",command=lambda: get_values(filePathCopy, frame),padx=10,pady=5)
        test_b.configure(background='#364156', foreground='white',font=('arial',10,'bold'))
        test_b.place(relx=0.2, rely=0.3, anchor=NW)

    # ==========================Các hàm cho việc xử lý trong video===================================
    def create_table():  
        width, height = 280, 560

        # tạo nền xanh
        img = np.zeros((height,width,3), dtype=np.uint8) 
        img[:, :] = [51, 153, 255] # đặt màu nền là màu bàn bida RGB
        img = cv2.cvtColor(img, cv2.COLOR_RGB2BGR) # chuyển sang BGR
            
        # vẽ 4 viền bàn
        cv2.line(img,(0,0),(width,0),(255,255,255)) 

        cv2.line(img,(0,0),(0,height),(255,255,255)) 

        cv2.line(img,(0,height),(width,height),(255,255,255)) 

        cv2.line(img,(width,0),(width,height),(255,255,255)) 

        return img

    # vẽ bóng, input hàm gồm ctrs: contours, bàn, bkinh, img: hình để vẽ ctrs
    def draw_balls(ctrs, background = create_table(), radius=7, img = 0):

        K = np.ones((3,3),np.uint8) # ma trận filter
        final = background.copy() # canvas
        mask = np.zeros((560, 280),np.uint8) # tạo một mask với ban đầu là màu đen    
        
        for x in range(len(ctrs)): # x quét all contours có được          
            # tìm tâm đường tròn 
            M = cv2.moments(ctrs[x]) # hàm tính các moment của đường viền x 
            cX = int(M['m10']/M['m00']) # tìm tâm theo trục X = cách chia tổng các moment theo trục X (m10) cho moment không gian (m00)
            cY = int(M['m01']/M['m00']) # trục Y tương tự
            
            # tạo mặt nạ
            mask[...]=0 # reset ma trận mask về 0 cho mỗi quả bóng
            cv2.drawContours(mask,ctrs,x,255,-1) # vẽ đường viền của contour thứ x lên hình ảnh mask với màu trắng và kín
            mask =  cv2.erode(mask,K,iterations = 3) # co mask 3 lần để loại bỏ nhễu
               
            # thiết kế bóng        
            # hình tròn đại diện bóng
            final = cv2.circle(final, # nơi vẽ bóng
                            (cX,cY), # vị trí bóng
                            radius, # kích thước
                            cv2.mean(img,mask), # mask dùng để chỉ định phần ảnh cần tính máu trung bình
                            -1) # -1 to fill ball with color

            # thêm 1 vòng đen bên ngoài bóng
            final = cv2.circle(final, (cX,cY), radius, 0, 2)             
        return final

    # hàm lọc các contours tìm ra contours xài được
    def filter_ctrs(ctrs, min_s = 90, max_s = 500, alpha = 3.75):  
        
        filtered_ctrs = [] # danh sách các contours lọc
        
        for x in range(len(ctrs)): # quét all contours
            
            rot_rect = cv2.minAreaRect(ctrs[x]) # tính kích thước hcm bao quanh contours
            w = rot_rect[1][0] # width
            h = rot_rect[1][1] # height
            area = cv2.contourArea(ctrs[x]) # diện tích contours

            if (h*alpha<w) or (w*alpha<h): # height . alpha < width và ngược lại suy ra không phải bóng, alpha tìm ra sau nhiều lần thử
                continue # do nothing
                   
            if (area < min_s) or (area > max_s): # nếu diện tích quá lớn hoặc quá bé
                continue # do nothing 
            
            # nếu không phải 2 điều kiện trên thì contours có thể là bóng
            filtered_ctrs.append(ctrs[x]) # add contours
        
        return filtered_ctrs
    
    def proceedVideo(filePathCopy, int_values):
        label.configure(text='')
        try: 
            scale = int( entry5.get())
        except:    label.configure(text='Bạn chưa nhập tỷ lệ scale', foreground='#FF0000')

        cap = cv2.VideoCapture(filePathCopy)

        width, height = 280, 560  

        W = int( 1080 * scale / 100) # final output height (1080 * scale)
        H = int( height * scale / 100 ) # final output height (560 * scale)

        # kích thước cuối cùng
        final_size = (W, H)

        frame_num = 0 # counting frames

        # tạo ma trận chuyển đổi Perspective
        pts1 = np.float32([ [int_values[0], int_values[1] ],[int_values[2], int_values[3]],[int_values[4], int_values[5]],[int_values[6], int_values[7]] ]) # 4 corners points of ORIGINAL image
        pts2 = np.float32([ [0,0],[width,0],[0,height],[width,height] ]) # 4 corners points of OUTPUT image
        #tạo 2 khoảng giá trị lower và upper
        lower = np.array([0, 60, 60]) 
        upper = np.array([50, 255, 255]) # HSV of snooker blue: (0-50, 80-255, 80-255) 

        # video     
        FPS = cap.get(cv2.CAP_PROP_FPS)

        fourcc = cv2.VideoWriter_fourcc(*'avc1') # characters: mã hóa 

        out = cv2.VideoWriter('final_output_video.mp4',fourcc, FPS, final_size)

        center_old = [(0,0), (0,0), (0,0)]
        point = -1
        while(1):
            ret ,frameNew = cap.read()
            if ret == True: 
                frameCopy = frameNew
          
                # Chuyển đổi góc nhìn bằng warpPerspective
                matrix = cv2.getPerspectiveTransform(pts1,pts2) # tìm ma trận chuyển đổi giữa 4 điểm 
                transformed = cv2.warpPerspective(frameNew, matrix, (width,height))
                    
                # làm mờ ảnh giúp loại bỏ nhiễu
                transformed_blur = cv2.GaussianBlur(transformed,(5,5),cv2.BORDER_DEFAULT)
                    
                # tạo mask chứa các giá trị pixel tương ứng với các vùng màu giữa ngưỡng thấp và ngưỡng cao
                hsv = cv2.cvtColor(transformed_blur, cv2.COLOR_RGB2HSV) # convert to hsv
                mask = cv2.inRange(hsv, lower, upper)
                    
                # áp dụng phép biến đổi đóng kết hợp khuếch đại và co rút để điền các lỗ hở
                kernel = np.ones((5,5),np.uint8)
                mask_closing = cv2.morphologyEx(mask, cv2.MORPH_CLOSE, kernel) # dilate->erode           
                
                # đảo ngược mask để giữ lại vật, loại bỏ bàn
                _, mask_inv = cv2.threshold(mask_closing,5,255,cv2.THRESH_BINARY_INV) # invert mask

                # tìm contours và lọc
                ctrs, _ = cv2.findContours(mask_inv, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE) # find contours
                ctrs = filter_ctrs(ctrs) # filter contours by sizes and shapes

                # vẽ bóng
                top_view = draw_balls(ctrs, img=transformed) # draw filtered contours (balls)  
  
                # hiển thị điểm
                cv2.putText(top_view, "Points: " + str(point), org=(20,20),fontFace=cv2.FONT_HERSHEY_DUPLEX, fontScale=0.5, color=(255,255,255),lineType=1)

                # chuyển ảnh sang ảnh xám
                gray = cv2.cvtColor(top_view, cv2.COLOR_BGR2GRAY)

                # làm mờ ảnh
                blur = cv2.GaussianBlur(gray, (5, 5), 0)

                # Sử dụng phép biến đổi Hough Circle để tìm các đường tròn trong hình ảnh
                circles = cv2.HoughCircles(blur, cv2.HOUGH_GRADIENT, dp=1, minDist=10,
                                        param1=30, param2=20, minRadius=0, maxRadius=20) # param1,2 ngưỡng trên, trung tâm phát hiện cạnh
                
                if circles is not None: # không rỗng
                    circles = np.uint16(np.around(circles))    
                    # circles sẽ có ma trận [1,2,3]  (1) là mỗi ma trận chứa chỉ số 1 hình tròn
                                                    #(2) là số thứ tự của hình tròn
                                                    #(3) chứa 3 thông tin gồm tọa độ x, y và bán kính

                    # Lấy 3 giá trị tâm đường tròn
                    center = []  # Khởi tạo danh sách center                
                    for i in range(circles.shape[1]): # quét các viên bi
                        center.append((circles[0, i, 0], circles[0, i, 1]))
                        cv2.putText(top_view, f"{center[i]}", org=(circles[0, i, 0] -20, circles[0, i, 1] - 20), # toa do tam hinh tron
                                    fontFace = cv2.FONT_HERSHEY_SIMPLEX, fontScale = 0.45, color = (255, 255, 255), lineType = 1)

                    # So sánh giá trị tâm để tính điểm
                    if frame_num % (30 * FPS) == 0: # chu kì 30 s
                        ngat = 0
                        for j in range (len(center)):
                            for h in range(3):
                                if(abs ( int(center_old[h][0]) - int(center[j][0]) ) < 2) and (abs (int(center_old[h][1]) - int(center[j][1]) ) < 2): 
                                    ngat = 1 
                                    j += 10    #thoát vòng for j
                                    break     #thoat vòng for h                   
                            
                        if ngat != 1:  # ngắt khác 1 chứng tỏ là cả 3 bi đều di chuyển, cập nhật tọa độ mới và point ++
                            point = point + 1
                            try:
                                for n in range(3):
                                    center_old[n] = center[n]
                            except: # chống báo lỗi khi chỉ có 2 bi đọc được
                                for n in range(len(center)):
                                    center_old[n] = center[n]

                # concat and resize output
                frameCopy = cv2.resize(frameCopy, (900, 560)) # 900 + 280 = 1080

                final = cv2.hconcat([frameCopy, top_view]) # ghép 2 khung hình
                final = cv2.resize(final, final_size, interpolation = cv2.INTER_AREA)

                cv2.imshow('final',final)

                out.write(final) # save final vid        
                frame_num += 1 # frame counter ++      

            else: 
                label.configure(text='Done', foreground='#33FF33')
                break

            flag = cv2.waitKey(1) & 0xFF
            if flag == ord('q'):
                    break

        cap.release() # release input video
        out.release() # release output video 

        cv2.destroyAllWindows() # delete output window
        cv2.waitKey(1)
        showOpenButton()

    def showProceedingButton(filePathCopy, int_values):
        proceed_b=Button(mainWin,text="Proceed Video",command=lambda: proceedVideo(filePathCopy, int_values),padx=10,pady=5)
        proceed_b.configure(background='#364156', foreground='white',font=('arial',10,'bold'))
        proceed_b.place(relx=0.2, rely=0.5, anchor=NW)
    
    def Open(): 
        label.configure(text='')
        try:
            fileVideo = filedialog.askopenfilename()
            webbrowser.open(fileVideo)
        except:         
            label.configure(text='Lỗi khi mở tệp vừa chọn, thử lại', foreground='#FF0000')

    def showOpenButton():
        test_b=Button(mainWin,text="Open Video",command=lambda: Open(), padx=10,pady=5)
        test_b.configure(background='#364156', foreground='white',font=('arial',10,'bold'))
        test_b.place(relx=0.2, rely=0.7, anchor=NW)

    # khởi tạo cửa sổ main
    mainWin = tk.Toplevel()
    mainWin.geometry('800x600')
    mainWin.title("Cửa sổ chính")
    mainWin.grab_set()

    # Tạo một canvas để vẽ
    canvas = Canvas(mainWin, width=800, height=600)
    canvas.pack()
    canvas.create_rectangle(450, 60, 700, 420, fill="#3399ff")
    colors = ["red", "white", "yellow"]
    for i in range(3):
        x = randint(500, 650)
        y = randint(100, 350)
        canvas.create_oval(x - 8, y - 8, x + 8, y + 8, fill=colors[i])

    # Thêm các thành phần của cửa sổ chính
    entry1 = Entry(mainWin)
    entry1.configure(font=('arial',10), width=8)
    entry1.place(x = 460, y = 70, anchor=NW) # góc trên bên trái
    
    entry2 = Entry(mainWin)
    entry2.configure(font=('arial',10), width=8)
    entry2.place(x = 690, y = 70, anchor=NE) # góc trên bên phải

    entry3 = Entry(mainWin)
    entry3.configure(font=('arial',10), width=8)
    entry3.place(x = 460, y = 410, anchor=SW) # góc dưới bên trái

    entry4 = Entry(mainWin)
    entry4.configure(font=('arial',10), width=8)
    entry4.place(x = 690, y = 410, anchor=SE) # góc dưới bên phải

    label=Label(mainWin, font=('arial',8,'bold'), text = 'Tỷ lệ Scale (%)')
    label.place(x = 575, y = 220, anchor = CENTER)

    entry5 = Entry(mainWin)
    entry5.configure(font=('arial',10), width=8)
    entry5.place(x = 575, y = 240, anchor= CENTER) # trung tâm

    button_guide=Button(mainWin,text="hướng dẫn",command= show_instructions, padx=5,pady=2)
    button_guide.configure(background='#364156', foreground='white',font=('arial',10,'bold'))
    button_guide.place(relx=0.0, rely=0.0, anchor = NW)

    label=Label(mainWin, background='#FFFFFF', font=('arial',15,'bold'))
    label.place(relx=0.5, rely=0.9, anchor=CENTER)

    openVideo=Button(mainWin,text="Select Video",command=openFile,padx=10,pady=5)
    openVideo.configure(background='#364156', foreground='#FFFFFF',font=('arial',10,'bold'))
    openVideo.place(relx=0.2, rely=0.1, anchor=NW)

    mainWin.bind('<Key>', closeMain)

#===========================Code chính=====================
# Tạo cửa sổ giao diện GUI
top=tk.Tk()
top.geometry('800x600')
top.title('PROJECT CUỐI KÌ NHÓM 7')

# Tạo ảnh nền
imageNen = Image.open('THUMNAIL.jpg')
resizedImage = imageNen.resize((800, 600), Image.ANTIALIAS)
backgroundImage = ImageTk.PhotoImage(resizedImage)
background_label = Label(top, image=backgroundImage)
background_label.place(relx=0, rely=0)

button_start=Button(top,text="START",command= newWindow, padx=10,pady=5)
button_start.configure(background='#364156', foreground='white',font=('arial',10,'bold'))
button_start.place(relx=0.5, rely=0.8, anchor = CENTER)

# Gắn sự kiện đóng cửa sổ
top.bind('<Key>', close_top)
top.mainloop()