//using RPI.WiringPiWrapper.Helpers.Interfaces;
//using System.Threading;

//namespace RPI.WiringPiWrapper
//{
//    public class LcdClass
//    {
//        //# commands
//        private int LCD_CLEARDISPLAY = 0x01;

//        private int LCD_RETURNHOME = 0x02;
//        private int LCD_ENTRYMODESET = 0x04;
//        private int LCD_DISPLAYCONTROL = 0x08;
//        private int LCD_CURSORSHIFT = 0x10;
//        private int LCD_FUNCTIONSET = 0x20;
//        private int LCD_SETCGRAMADDR = 0x40;
//        private int LCD_SETDDRAMADDR = 0x80;

//        //# flags for display entry mode
//        private int LCD_ENTRYRIGHT = 0x00;

//        private int LCD_ENTRYLEFT = 0x02;
//        private int LCD_ENTRYSHIFTINCREMENT = 0x01;
//        private int LCD_ENTRYSHIFTDECREMENT = 0x00;

//        //# flags for display on/off control
//        private int LCD_DISPLAYON = 0x04;

//        private int LCD_DISPLAYOFF = 0x00;
//        private int LCD_CURSORON = 0x02;
//        private int LCD_CURSOROFF = 0x00;
//        private int LCD_BLINKON = 0x01;
//        private int LCD_BLINKOFF = 0x00;

//        //# flags for display/cursor shift
//        private int LCD_DISPLAYMOVE = 0x08;

//        private int LCD_CURSORMOVE = 0x00;
//        private int LCD_MOVERIGHT = 0x04;
//        private int LCD_MOVELEFT = 0x00;

//        //# flags for function set
//        private int LCD_8BITMODE = 0x10;

//        private int LCD_4BITMODE = 0x00;
//        private int LCD_2LINE = 0x08;
//        private int LCD_1LINE = 0x00;
//        private int LCD_5x10DOTS = 0x04;
//        private int LCD_5x8DOTS = 0x00;

//        //# flags for backlight control
//        private int LCD_BACKLIGHT = 0x08;

//        private int LCD_NOBACKLIGHT = 0x00;

//        private int En = 0b00000100;// # Enable bit;
//        private int Rw = 0b00000010;// # Read/Write bit
//        private int Rs = 0b00000001;// # Register select bit

//        private I2CDevice lcd_device;

//        //# initializes objects and lcd
//        public LcdClass()
//        {
//            lcd_device = new I2CDevice(0x3f);

//            lcd_device.WriteCommand(0x03);
//            lcd_device.WriteCommand(0x03);
//            lcd_device.WriteCommand(0x03);
//            lcd_device.WriteCommand(0x02);

//            lcd_device.WriteCommand(LCD_FUNCTIONSET | LCD_2LINE | LCD_5x8DOTS | LCD_4BITMODE);
//            lcd_device.WriteCommand(LCD_DISPLAYCONTROL | LCD_DISPLAYON);
//            lcd_device.WriteCommand(LCD_CLEARDISPLAY);
//            lcd_device.WriteCommand(LCD_ENTRYMODESET | LCD_ENTRYLEFT);
//            Thread.Sleep(1);
//        }

//        //# clocks EN to latch command
//        public void lcd_strobe(int data)
//        {
//            lcd_device.WriteCommand(data | En | LCD_BACKLIGHT);
//            Thread.Sleep(1);
//            lcd_device.WriteCommand(((data & ~En) | LCD_BACKLIGHT));
//            Thread.Sleep(1);
//        }

//        public void lcd_write_four_bits(int data)
//        {
//            lcd_device.WriteCommand(data | LCD_BACKLIGHT);
//            lcd_strobe(data);
//        }

//        //# write a command to lcd
//        public void lcd_write(int cmd, int mode = 0)
//        {
//            lcd_write_four_bits(mode | (cmd & 0xF0));
//            lcd_write_four_bits(mode | ((cmd << 4) & 0xF0));
//        }

//        //# write a character to lcd (or character rom) 0x09: backlight | RS=DR<
//        //# works!
//        public void lcd_write_char(int charvalue, int mode = 1)
//        {
//            lcd_write_four_bits(mode | (charvalue & 0xF0));
//            lcd_write_four_bits(mode | ((charvalue << 4) & 0xF0));
//        }

//        //# put string function with optional char positioning
//        public void lcd_display_string(string text, int line = 1, int pos = 0)
//        {
//            int pos_new = 0;
//            if (line == 1)
//            {
//                pos_new = pos;
//            }
//            else if (line == 2)
//            {
//                pos_new = 0x40 + pos;
//            }

//            lcd_write(0x80 + pos_new);

//            foreach (var c in text)
//            {
//                lcd_write((int)c, Rs);
//            }
//        }

//        //# clear lcd and set to home
//        public void lcd_clear()
//        {
//            lcd_write(LCD_CLEARDISPLAY);
//            lcd_write(LCD_RETURNHOME);
//        }

//        //# clear lcd and set to home
//        public void lcd_returnHome()
//        {
//            lcd_write(LCD_RETURNHOME);
//        }

//        //#define backlight on/off (lcd.backlight(1); off= lcd.backlight(0)
//        public void backlight(int state)// # for state, 1 = on, 0 = off
//        {
//            if (state == 1)
//            {
//                lcd_device.WriteCommand(LCD_BACKLIGHT);
//            }
//            else if (state == 0)
//            {
//                lcd_device.WriteCommand(LCD_NOBACKLIGHT);
//            }
//        }

//        //# add custom characters (0 - 7)
//        //   def lcd_load_custom_chars(self, fontdata) :
//        //      self.lcd_write(0x40);
//        //      for char in fontdata:
//        //         for line in char:
//        //            self.lcd_write_char(line)
//    }
//}