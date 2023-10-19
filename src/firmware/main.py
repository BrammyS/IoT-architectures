from network import LoRa
from lib.cbor import dumps_array
import env, time, pycom
import socket, ubinascii
import gc
import utime
import machine
from machine import RTC
from machine import SD
from lib.pytrack.L76GNSS import L76GNSS
from lib.pytrack.pycoproc_1 import Pycoproc
from network import WLAN

pycom.rgbled(0x7f0000) # red
time.sleep(1)

# STARTING_TEMPERATURE = urandom.randint(5, 25)
LATITUDE = "lat"
LONGITUDE = "lon"
TEMPERATURE = "Temperature"

time.sleep(2)
gc.enable()

# Setup Wi-Fi
wlan = WLAN(mode=WLAN.STA)
wlan.connect(ssid='xxxxxxxx', auth=(WLAN.WPA2, 'xxxxxxxx'))
while not wlan.isconnected():
    machine.idle()
print("WiFi connected succesfully")
print(wlan.ifconfig())

# setup rtc
rtc = machine.RTC()
rtc.ntp_sync("pool.ntp.org")
utime.sleep_ms(750)
print('\nRTC Set from NTP to UTC:', rtc.now())
utime.timezone(7200)
print('Adjusted from UTC to EST timezone', utime.localtime(), '\n')

pycom.rgbled(0x7f7f00) # yellow
time.sleep(1)

py = Pycoproc(Pycoproc.PYTRACK)
l76 = L76GNSS(py, timeout=30)

lora = LoRa(mode=LoRa.LORAWAN, region=LoRa.EU868)
print(ubinascii.hexlify(lora.mac()).upper().decode('utf-8'))
dev_eui = ubinascii.unhexlify(env.DEV_EUI)
app_eui = ubinascii.unhexlify(env.APP_EUI)
app_key = ubinascii.unhexlify(env.APP_KEY)

# join a network using OTAA
lora.join(activation=LoRa.OTAA, auth=(dev_eui, app_eui, app_key), timeout=0)

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

def generate_senML(lat, lon):
    return dumps_array([{}, {"n": LATITUDE,"v": lat}, {"n": LONGITUDE,"v": lon}])

# Define a function to send SenML data
def send_data(senML):
    s.setblocking(True)
    s.send(senML)
    s.setblocking(False)

# Example of generating and sending fake sensor data
reqs = 0
max_reqs=200
while reqs <= max_reqs:
    (lat, lon) = l76.coordinates(debug=True)
    print("{} --- {},{} - {} - {}".format(reqs, lat, lon, rtc.now(), gc.mem_free()))
    if lat is not None:
        pycom.rgbled(0x007f00) # green
        send_data(generate_senML(lat, lon))
    else:
        pycom.rgbled(0x00007f) # blue
    time.sleep(10)
    reqs += 1