import serial
import struct
import crc16
import glob
import receiver_commands as c

baud_rates = {
    'unknown': 0,
    'firmware': 38400,
    'bios': 115200
}


def connect(mode='firmware'):
    port = find_receiver()
    ser = serial.Serial(port, baud_rates.get(mode), timeout=1)
    if not ser.isOpen():
        raise Exception("Receiver not connected!")
    return ser


def find_receiver():
    """
    Returns the /dev/tty.usbmodem* filename for the attached receiver
    """
    matches = glob.glob('/dev/tty.usbmodem*')
    if matches:
        return matches[0]
    else:
        raise Exception('No receiver connected')


def send(ser, value):
    packet = [1, value, 0, 0]
    byte_str = ''.join([chr(b) for b in packet])
    crc = crc16.crc16xmodem(byte_str)
    byte_str += struct.pack('H', crc)
    print 'Sending %s. (len %s)' % (
        [ord(x) for x in byte_str],
        len(byte_str))
    r = ser.write(byte_str)
    print 'Sent %s bytes' % r


def read(ser):
    print 'Trying to read....'
    r = ser.read(10)
    return [ord(x) for x in r]


def close(ser):
    ser.close()


if __name__ == '__main__':
    ser = connect()
    send(ser, c.READ_TRANSMITTER_ID)

    print read(ser)

# references:
# http://eli.thegreenplace.net/2009/08/20/frames-and-protocols-for-the-serial-port-in-python/
# https://pypi.python.org/pypi/crc16/0.1.1
#ser.write(msg)

