from lib.cbor import dumps_array
from machine import idle, RTC
import utime
import env
import pycom
from network import WLAN
import socket, ubinascii
from network import LoRa

def generate_senML(lat, lon):
    LATITUDE = "lat"
    LONGITUDE = "lon"
    TEMPERATURE = "Temperature"
    return dumps_array([{}, {"n": LATITUDE,"v": lat}, {"n": LONGITUDE,"v": lon}])

def change_led(color):
    if env.SHOW_LED_STATUS:
        pycom.rgbled(color)

def setup_rtc_time():
    # Setup Wi-Fi
    wlan = WLAN(mode=WLAN.STA)
    wlan.connect(ssid=env.WIFI_SSID, auth=(WLAN.WPA2, env.WIFI_PASSWORD))
    while not wlan.isconnected():
        idle()

    rtc = RTC()
    rtc.ntp_sync("pool.ntp.org")
    utime.sleep_ms(750)
    utime.timezone(7200)

def setup_lora():
    lora = LoRa(mode=LoRa.LORAWAN, region=LoRa.EU868)
    
    dev_eui = ubinascii.unhexlify(env.DEV_EUI)
    app_eui = ubinascii.unhexlify(env.APP_EUI)
    app_key = ubinascii.unhexlify(env.APP_KEY)

    # join a network using OTAA
    lora.join(activation=LoRa.OTAA, auth=(dev_eui, app_eui, app_key), timeout=0)
    return lora