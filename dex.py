from port import DexcomG4
import api

def full_test():
    g4 = DexcomG4()
    #g4.send(c.READ_BATTERY_LEVEL)
    print api.read_battery_level(g4)
    print api.read_battery_state(g4)

if __name__ == '__main__':
    full_test()
    # references:
    # http://eli.thegreenplace.net/2009/08/20/frames-and-protocols-for-the-serial-port-in-python/
    # https://pypi.python.org/pypi/crc16/0.1.1
    #ser.write(msg)

