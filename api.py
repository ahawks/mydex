import receiver_commands as commands
import time

def ping(port):
    return _send_sleep_receive(port, commands.PING)

def read_firmware_header(port):
    return _send_sleep_receive(port, commands.READ_FIRMWARE_HEADER)

def read_database_parition_info(port):
    return _send_sleep_receive(port, commands.READ_DATABASE_PARITION_INFO)

def read_database_page_range(port):
    return _send_sleep_receive(port, commands.READ_DATABASE_PAGE_RANGE)

def read_database_pages(port):
    return _send_sleep_receive(port, commands.READ_DATABASE_PAGES)

def read_database_page_header(port):
    return _send_sleep_receive(port, commands.READ_DATABASE_PAGE_HEADER)

def read_transmitter_id(port):
    return _send_sleep_receive(port, commands.READ_TRANSMITTER_ID)

def read_language(port):
    return _send_sleep_receive(port, commands.READ_LANGUAGE)

def read_display_time_offset(port):
    return _send_sleep_receive(port, commands.READ_DISPLAY_TIME_OFFSET)

def read_rtc(port):
    return _send_sleep_receive(port, commands.READ_RTC)

def read_battery_level(port):
    return _send_sleep_receive(port, commands.READ_BATTERY_LEVEL)[4]

def read_system_time(port):
    return _send_sleep_receive(port, commands.READ_SYSTEM_TIME)

def read_system_time_offset(port):
    return _send_sleep_receive(port, commands.READ_SYSTEM_TIME_OFFSET)

def read_glucose_unit(port):
    return _send_sleep_receive(port, commands.READ_GLUCOSE_UNIT)

def read_blinded_mode(port):
    return _send_sleep_receive(port, commands.READ_BLINDED_MODE)

def read_clock_mode(port):
    return _send_sleep_receive(port, commands.READ_CLOCK_MODE)

def read_device_mode(port):
    return _send_sleep_receive(port, commands.READ_DEVICE_MODE)

def read_battery_state(port):
    return _send_sleep_receive(port, commands.READ_BATTERY_STATE)

def read_hardware_board_id(port):
    return _send_sleep_receive(port, commands.READ_HARDWARE_BOARD_ID)

def read_flash_page(port):
    return _send_sleep_receive(port, commands.READ_FLASH_PAGE)

def read_firmware_settings(port):
    return _send_sleep_receive(port, commands.READ_FIRMWARE_SETTINGS)

def read_enable_set_up_wizard_flag(port):
    return _send_sleep_receive(port, commands.READ_ENABLE_SET_UP_WIZARD_FLAG)

def read_set_up_wizard_state(port):
    return _send_sleep_receive(port, commands.READ_SET_UP_WIZARD_STATE)

def write_transmitter_id(port):
    pass #return _send_sleep_receive(port, commands.WRITE_TRANSMITTER_ID)

def write_language(port):
    pass #return _send_sleep_receive(port, commands.WRITE_LANGUAGE)

def write_display_time_offset(port):
    pass #return _send_sleep_receive(port, commands.WRITE_DISPLAY_TIME_OFFSET)

def write_system_time(port):
    pass #return _send_sleep_receive(port, commands.WRITE_SYSTEM_TIME)

def write_glucose_unit(port):
    pass #return _send_sleep_receive(port, commands.WRITE_GLUCOSE_UNIT)

def write_blinded_mode(port):
    pass #return _send_sleep_receive(port, commands.WRITE_BLINDED_MODE)

def write_clock_mode(port):
    pass #return _send_sleep_receive(port, commands.WRITE_CLOCK_MODE)

def _send_sleep_receive(port, command):
    port.send(command)
    time.sleep(1)
    ret = port.read()
    return ret

