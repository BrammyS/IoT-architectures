from network import LoRa
from lib.cbor import dumps_array
import env, time, pycom
import socket, ubinascii
from machine import I2C

# STARTING_TEMPERATURE = urandom.randint(5, 25)
GEO_LOCATION = "GeoLocation"
TEMPERATURE = "Temperature"

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

def generate_senML(geo_location_value, temperature_value):
    return dumps_array([{}, {"n": GEO_LOCATION,"v": geo_location_value}, {"n": TEMPERATURE,"v": temperature_value}])

# Define a function to send SenML data
def send_data():
    s.setblocking(True)
    s.send(generate_senML(420, 10))
    s.setblocking(False)

# Example of generating and sending fake sensor data
send_data()
