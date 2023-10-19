import env, time, pycom
import socket
import gc
import machine
from lib.pytrack.L76GNSS import L76GNSS
from lib.pytrack.pycoproc_1 import Pycoproc
from helpers import *

change_led(0x7f0000) # red
time.sleep(1)

# STARTING_TEMPERATURE = urandom.randint(5, 25)

time.sleep(2)
gc.enable()

# setup rtc
setup_rtc_time()

if env.SHOW_LED_STATUS:
    pycom.rgbled(0x7f7f00) # yellow
time.sleep(1)

py = Pycoproc(Pycoproc.PYTRACK)
l76 = L76GNSS(py, timeout=30)

# setup lora
lora = setup_lora()

# Loop until joined
while not lora.has_joined():
    time.sleep(2.5)
    print('Not yet joined...')

print('Joined!')

# Create a LoRa socket
s = socket.socket(socket.AF_LORA, socket.SOCK_RAW)

# Set the LoRaWAN data rate
s.setsockopt(socket.SOL_LORA, socket.SO_DR, 5)

# Make the socket non-blocking
s.setblocking(False)

# Define a function to send SenML data
def send_data(senML):
    s.setblocking(True)
    s.send(senML)
    s.setblocking(False)

reqs = 0
max_reqs=200
while reqs <= max_reqs:
    (lat, lon) = l76.coordinates(debug=True)
    if lat is not None:
        change_led(0x007f00) # green
        send_data(generate_senML(lat, lon))
    else:
        change_led(0x00007f) # blue
    time.sleep(10)
    reqs += 1