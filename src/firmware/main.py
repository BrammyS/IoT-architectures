from network import LoRa
import env, time, pycom
import socket, ubinascii

pycom.rgbled(0x7f0000)
time.sleep(1)

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

# Define a function to send SenML data
def send_data(data):
    s.setblocking(True)
    s.send(data)
    s.setblocking(False)

# Example of generating and sending fake sensor data
while True:
    send_data("86836461616161636c6174fb4049fa364388ebcc8366626262626262636c6f6efb401b003ff69014b68366636363636363616dfb409cbe261fe21d9783656464646464612519270f8366656565656565612519270fa2006766666666666666036d31363937353537323133353436")
    time.sleep(60)  # Adjust the interval as needed