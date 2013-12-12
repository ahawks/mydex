import serial
from serial.tools import list_ports
import struct
import crc16
import glob
import receiver_commands as c

class DexcomG4():
    portname = None
    port = None
    baud_rates = {
        'unknown': 0,
        'firmware': 115200
    }

    def __init__(self):
        self.connect()

    def connect(self, mode='firmware'):
        self.portname = self.find_receiver()
        self.port = serial.Serial(self.portname, self.baud_rates.get(mode), timeout=2)
        if not self.port.isOpen():
            raise Exception("Receiver not connected!")

    def find_receiver(self):
        """
        Returns the serial device name for the attached receiver 
        (/dev/* for unix systems, COM* for Windows)
        """
        ports = list_ports.comports()
        for p in ports:
            if 'DexCom' in p[1]:
                return p[0]


    def store_bytes(self, value, targetData, targetOffset):
        """
        This is weird and I want to rewrite it. The packet is passed in (targetData), and this stores <value>
        in it, in the specific dexcom way. I would rather pass in the value, and return the packet. 
        """
        targetData[targetOffset] = value & 255
        targetData[targetOffset + 1] = value >> 8 & 255
        targetData[targetOffset + 2] = value >> 16 & 255
        targetData[targetOffset + 3] = value >> 24 & 255
        targetData[targetOffset + 4] = value >> 32 & 255
        targetData[targetOffset + 5] = value >> 40 & 255
        targetData[targetOffset + 6] = value >> 48 & 255
        targetData[targetOffset + 7] = value >> 56 & 255
        return 8

    def build_packet(self, command, payload=None):
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
        self.store_bytes(crc, packet, num-2)
        return packet

    def send(self, value):
        packet = self.build_packet(value)
        byte_str = ''.join([chr(b) for b in packet])
        crc = crc16.crc16xmodem(byte_str)
        byte_str += struct.pack('H', crc)
        r = self.port.write(byte_str)
        self.port.flush()
        print 'Sent %s as %s bytes' % (value, r)


    def read(self):
        print 'Reading receiver response...'
        r = self.port.read(100)
        return [ord(x) for x in r]


    def disconnect(self):
        self.port.close()