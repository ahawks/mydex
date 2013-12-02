import serial
from serial.tools import list_ports
import struct
import crc16
import glob
import receiver_commands as c

baud_rates = {
    'unknown': 0,
    'firmware': 115200
}


def connect(mode='firmware'):
    port = find_receiver()
    ser = serial.Serial(port, baud_rates.get(mode), timeout=2)
    if not ser.isOpen():
        raise Exception("Receiver not connected!")
    return ser


def find_receiver():
    """
    Returns the /dev/tty.usbmodem* filename for the attached receiver
    """
    ports = list_ports.comports()
    for p in ports:
        if 'DexCom' in p[1]:
            return p[0]


def store_bytes(value, targetData, targetOffset):
    targetData[targetOffset] = value & 255
    targetData[targetOffset + 1] = value >> 8 & 255
    targetData[targetOffset + 2] = value >> 16 & 255
    targetData[targetOffset + 3] = value >> 24 & 255
    targetData[targetOffset + 4] = value >> 32 & 255
    targetData[targetOffset + 5] = value >> 40 & 255
    targetData[targetOffset + 6] = value >> 48 & 255
    targetData[targetOffset + 7] = value >> 56 & 255
    return 8

def build_packet(command, payload=None):
    #todo: In C#, they overload this function based on payload type. 
    # no payload: num=6
    # byte: num=7
    # short/ushort: num=8
    # int/uint: num=10
    # long/ulong: num=14
    # there's even a case for payload, payload2
    MaxPayloadLength = 1584
    MinPacketLength = 6
    MaxPacketLength = 1590

    packet = bytearray(1590)
    num = 6;
    packet[0] = 1
    packet[1] = num
    packet[3] = command
    
    crc = crc16.crc16xmodem(str(packet)[0:num-2])
    store_bytes(crc, packet, num-2)

    return packet

    
def send(ser, value):
    
    byte_str = ''.join([chr(b) for b in packet])
    crc = crc16.crc16xmodem(byte_str)
    byte_str += struct.pack('H', crc)
    print 'Sending %s. (len %s)' % (
        [ord(x) for x in byte_str],
        len(byte_str))
    r = ser.write(byte_str)
    ser.flush()
    print 'Sent %s bytes' % r


def read(ser):
    print 'Trying to read....'
    r = ser.read(100)
    return [ord(x) for x in r]


def close(ser):
    ser.close()

def full_test():
    ser = connect()
    packet = build_packet(17)
    ser.write(packet)
    print read(ser)

if __name__ == '__main__':
    full_test()
# references:
# http://eli.thegreenplace.net/2009/08/20/frames-and-protocols-for-the-serial-port-in-python/
# https://pypi.python.org/pypi/crc16/0.1.1
#ser.write(msg)

