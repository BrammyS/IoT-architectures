from network import LoRa
from dotenv import load_dotenv
import os
import ubinascii
import socket
import time

load_dotenv()

lora = LoRa(mode=LoRa.LORAWAN, region=LoRa.EU868)
print(ubinascii.hexlify(lora.mac()).upper().decode('utf-8'))
dev_eui = ubinascii.unhexlify(os.getenv('DevEUI'))
app_eui = ubinascii.unhexlify(os.getenv('AppEUI'))
app_key = ubinascii.unhexlify(os.getenv('AppKey'))

def send_message(socket_connection, byte_array):
    # make the socket blocking
    # (waits for the data to be sent and for the 2 receive windows to expire)
    socket_connection.setblocking(True)
    # send some data
    socket_connection.send(bytes(byte_array))
    # make the socket non-blocking
    # (because if there's no data received it will block forever...)
    socket_connection.setblocking(False)


# join a network using OTAA
lora.join(activation=LoRa.OTAA, auth=(dev_eui, app_eui, app_key), timeout=0)

# wait until the module has joined the network
while not lora.has_joined():
    time.sleep(2.5)
    print('Not yet joined...')

# Setup socket connection
socket_connection = socket.socket(socket.AF_LORA, socket.SOCK_RAW)
socket_connection.setsockopt(socket.SOL_LORA, socket.SO_DR, 5)

send_message(socket_connection, [0x01, 0x02, 0x03])

# get any data received (if any...)
data = socket_connection.recv(64)
print(data)
print("Done")