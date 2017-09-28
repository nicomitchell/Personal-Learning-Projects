import numpy as np
import cv2
import tensorflow as tf

cap = cv2.VideoCapture(0)

while(True):
  # Take each frame
    _, frame = cap.read()

    # Convert BGR to HSV
    hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)

    # define range of red color in HSV
    lower_red = np.array([0,90,20])
    upper_red = np.array([10,255,255])

    # Threshold the HSV image to get only red colors
    mask = cv2.inRange(hsv, lower_red, upper_red)

    # Bitwise-AND mask and original image
    res = cv2.bitwise_and(frame,frame, mask= mask)

    cv2.imshow('frame',frame)
    # cv2.imshow('mask',mask)
    # cv2.imshow('res',res)
    if cv2.waitKey(1) & 0xFF == ord(' '):
        break
# When everything done, release the capture
cap.release()
cv2.destroyAllWindows()