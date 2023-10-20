from lib.cbor import dumps_array
from machine import idle, RTC, rng
import utime
import env
import pycom
from network import WLAN
import socket, ubinascii
from network import LoRa
from time import sleep

def __generate_random_number(max_value):
    return __generate_random_number(0, max_value)

def __generate_random_number(min_value, max_value):
    random_int = rng() & 0x7FFFFFFF  # Ensure it's a positive integer
    range_size = max_value - min_value + 1
    random_number = min_value + (random_int % range_size)

    return random_number

STARTING_TEMPERATURE = __generate_random_number(5, 30)

def generate_senML(lat, lon):
    LATITUDE = "lat"
    LONGITUDE = "lon"
    TEMPERATURE = "Temperature"
    return dumps_array([{}, {"n": LATITUDE,"v": lat}, {"n": LONGITUDE,"v": lon}, {"n": TEMPERATURE, "v": generate_random_temperature()}])

def change_led(color):
    if env.SHOW_LED_STATUS:
        pycom.rgbled(color)

def __setup_lora_connection():
    lora = LoRa(mode=LoRa.LORAWAN, region=LoRa.EU868)
    
    dev_eui = ubinascii.unhexlify(env.DEV_EUI)
    app_eui = ubinascii.unhexlify(env.APP_EUI)
    app_key = ubinascii.unhexlify(env.APP_KEY)

    # join a network using OTAA
    lora.join(activation=LoRa.OTAA, auth=(dev_eui, app_eui, app_key), timeout=0)
    return lora

def create_lora_socket(debug=False):
    # setup lora
    lora = __setup_lora_connection()

    # Loop until joined
    while not lora.has_joined():
        sleep(2.5)
        if debug:
            print('Not yet joined...')

    if debug:    
        print('Joined!')

    # Create a LoRa socket
    s = socket.socket(socket.AF_LORA, socket.SOCK_RAW)

    # Set the LoRaWAN data rate
    s.setsockopt(socket.SOL_LORA, socket.SO_DR, 5)

    # Make the socket non-blocking
    s.setblocking(False)
    return s

def update_status_led(was_last_lora_send_success, has_gps):
    color = int("0x{}{}{}".format("ff" if was_last_lora_send_success and has_gps else "00", "ff" if was_last_lora_send_success else "00", "ff" if has_gps else "00"))
    change_led(color)

def generate_random_temperature():
    return int(STARTING_TEMPERATURE - (__generate_random_number(5) / 2))