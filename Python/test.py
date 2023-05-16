import matplotlib.pyplot as plt
from mtcnn import MTCNN

filename = r"lena.png"
pixels = plt.imread(filename)
detector = MTCNN()
faces = detector.detect_faces(pixels)
for face in faces:
    print(face)