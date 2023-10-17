from network import LoRa
import l
import env
import os
import ubinascii
import socket
import time

lora = LoRa(mode=LoRa.LORAWAN, region=LoRa.EU868)
print(ubinascii.hexlify(lora.mac()).upper().decode('utf-8'))
dev_eui = ubinascii.unhexlify(env.DEV_EUI)
app_eui = ubinascii.unhexlify(env.APP_EUI)
app_key = ubinascii.unhexlify(env.APP_KEY)

def send_message(socket_connection, byte_array):
    # make the socket blocking
    # (waits for the data to be sent and for the 2 receive windows to expire)
    socket_connection.setblocking(True)
    # send some data
    socket_connection.send(bytearray(byte_array))
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

send_message(socket_connection, [0x41, 0x02, 0x03])

# get any data received (if any...)
data = socket_connection.recv(64)
if data:
    received_integer = int.from_bytes(data, byteorder='big', signed=False)
    print(received_integer)
else:
    print("No data received")

print("Done")
# hex_data = ubinascii.hexlify(data)
# received_integer = int(hex_data, 16)  # Convert the hexadecimal string to an integer
# print(received_integer)
print("Done")