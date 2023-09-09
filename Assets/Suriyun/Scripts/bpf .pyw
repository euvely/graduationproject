from scipy.signal import butter, filtfilt
import warnings
warnings.filterwarnings('ignore')

# rarwdata from Player.cs
rawdata = input()

# filtering
def butter_bandpass(lowcut, highcut, fs, order=3): # 3 ten sonra lfilter NaN degerler vermeye basliyor
    nyq = 0.5 * fs
    low = lowcut / nyq
    high = highcut / nyq
    b, a = butter(order, [low, high], btype='band', analog=False)   
    return b, a

# band-pass filter between two frequency     
def butter_bandpass_filter(data, lowcut, highcut, fs, order=3):
    b, a = butter_bandpass(lowcut, highcut, fs, order=order)
    #y = lfilter(b, a, data)
    y = filtfilt(b, a, data)
    return y

#print("rawdata: ", rawdata)

