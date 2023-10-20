import time
import gc
from lib.pytrack.L76GNSS import L76GNSS
from lib.pytrack.pycoproc_1 import Pycoproc
from helpers import *
import pycom

pycom.heartbeat(False)

change_led(0x7f0000) # red

time.sleep(2)
gc.enable()

change_led(0x7f7f00) # yellow
time.sleep(1)

py = Pycoproc(Pycoproc.PYTRACK)
l76 = L76GNSS(py, timeout=30)

s = create_lora_socket()

# Define a function to send SenML data
def send_data(senML):
    s.setblocking(True)
    try:
        s.send(senML)
    except:
        return False # we lost the connection :(
    s.setblocking(False)
    return True

wasLastSendSuccessFull = True
while True:
    (lat, lon) = l76.coordinates(debug=True)

    update_status_led(wasLastSendSuccessFull, lat is not None and lon is not None)

    if lat is not None:
        wasLastSendSuccessFull = send_data(generate_senML(lat, lon))
        if not wasLastSendSuccessFull:
            s = create_lora_socket()
        time.sleep(50)

    time.sleep(10)