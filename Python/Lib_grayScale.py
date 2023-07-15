# Import necessary libraries
import numpy as np      # Using Numpy for mathematic matrix calculations

###### Average Grayscaling ######
def to_gray_average(origin_rgb, image_size):

    ## Parameters:
    # Original RGB image array: origin_rgb (Numpy array)
    # Image size: image_size (tuple)

    # Define the array for grayscaled
    gray_img = np.zeros(image_size, np.uint8)     # Must have the same dimension to the original

    ### Columns scan ###
    # Loop starts
    for x in range(image_size[1]):
        for y in range(image_size[0]):

            # Convert to unsigned int 8 bits
            gray_val = (origin_rgb[y, x , 2]  # Red channel
                            + origin_rgb[y, x , 1]  # Green channel
                                + origin_rgb[y, x , 0])/3  # Blue channel 

            # Set the grayscaled value to the array
            gray_img[y, x, :] = np.uint8(gray_val)
    # Loop ends

    # Return the grayscaled
    return np.uint8(gray_img)

###### Lightness Grayscaling ######
def to_gray_lightness(origin_rgb, image_size):

    ## Parameters:
    # Original RGB image array: origin_rgb (Numpy array)
    # Image size: image_size (tuple)

    # Define the array for grayscaled
    gray_img = np.zeros(image_size, np.uint8)     # Must have the same dimension to the original

    ### Columns scan ###
    # Loop starts
    for x in range(image_size[1]):
        for y in range(image_size[0]):

            # Find maximum and minimum among channels
            max_val  = max( origin_rgb[y, x, 2]  # Red channel
                                , origin_rgb[y, x, 1]  # Green channel
                                    , origin_rgb[y, x, 0] )  # Blue channel 
            
            min_val  = min( origin_rgb[y, x, 2]  # Red channel
                                , origin_rgb[y, x, 1]  # Green channel
                                    , origin_rgb[y, x, 0] )  # Blue channel  
            
            # Compute the grayscaled value implementing Lightness method
            gray_val = max_val/2 + min_val/2 # Convert into 8-bit unsigned integer value

            # Set the grayscaled value to the array
            gray_img[y, x, :] = np.uint8(gray_val)
    # Loop ends

    # Return the grayscaled
    return np.uint8(gray_img)

###### Luminance Grayscaling ######
def to_gray_luminance(origin_rgb, image_size):

    ## Parameters:
    # Original RGB image array: origin_rgb (Numpy array)
    # Image size: image_size (tuple)

    # Define the array for grayscaled
    gray_img = np.zeros(image_size, np.uint8)     # Must have the same dimension to the original

    ### Columns scan ###
    # Loop starts
    for x in range(image_size[1]):
        for y in range(image_size[0]):

            # Convert to unsigned int 8 bits
            gray_val = (0.2126 * origin_rgb[y, x, 2]  # Red channel
                                    + 0.7152 * origin_rgb[y, x, 1]  # Green channel
                                        + 0.0722 * origin_rgb[y, x, 0])  # Blue channel 

            # Set the grayscaled value to the array
            gray_img[y, x, :] = np.uint8(gray_val)
    # Loop ends

    # Return the grayscaled
    return np.uint8(gray_img)
