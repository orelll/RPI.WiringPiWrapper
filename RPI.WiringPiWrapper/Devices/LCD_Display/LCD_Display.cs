using RPI.WiringPiWrapper.Hardware;
using RPI.WiringPiWrapper.Interfaces;

namespace RPI.WiringPiWrapper.Devices.LCD_Display
{
    public class LCD_Display : I2CDeviceBase
    {
        #region commands

        private int LCD_CLEARDISPLAY = 0x01;

        private int LCD_RETURNHOME = 0x02;
        private int LCD_ENTRYMODESET = 0x04;
        private int LCD_DISPLAYCONTROL = 0x08;
        private int LCD_CURSORSHIFT = 0x10;
        private int LCD_FUNCTIONSET = 0x20;
        private int LCD_SETCGRAMADDR = 0x40;
        private int LCD_SETDDRAMADDR = 0x80;

        //# flags for display entry mode
        private int LCD_ENTRYRIGHT = 0x00;

        private int LCD_ENTRYLEFT = 0x02;
        private int LCD_ENTRYSHIFTINCREMENT = 0x01;
        private int LCD_ENTRYSHIFTDECREMENT = 0x00;

        //# flags for display on/off control
        private int LCD_DISPLAYON = 0x04;

        private int LCD_DISPLAYOFF = 0x00;
        private int LCD_CURSORON = 0x02;
        private int LCD_CURSOROFF = 0x00;
        private int LCD_BLINKON = 0x01;
        private int LCD_BLINKOFF = 0x00;

        //# flags for display/cursor shift
        private int LCD_DISPLAYMOVE = 0x08;

        private int LCD_CURSORMOVE = 0x00;
        private int LCD_MOVERIGHT = 0x04;
        private int LCD_MOVELEFT = 0x00;

        //# flags for function set
        private int LCD_8BITMODE = 0x10;

        private int LCD_4BITMODE = 0x00;
        private int LCD_2LINE = 0x08;
        private int LCD_1LINE = 0x00;
        private int LCD_5x10DOTS = 0x04;
        private int LCD_5x8DOTS = 0x00;

        //# flags for backlight control
        private int LCD_BACKLIGHT = 0x08;

        private int LCD_NOBACKLIGHT = 0x00;

        private int En = 0b00000100;// # Enable bit;
        private int Rw = 0b00000010;// # Read/Write bit
        private int Rs = 0b00000001;// # Register select bit

        #endregion commands

        public LCD_Display(ITimer timer, ILogger logger, int address = 0x3f) : base(address, logger, timer)
        {
            Initialize();
        }

        private void Initialize()
        {
            WriteCommand(0x03);
            WriteCommand(0x03);
            WriteCommand(0x03);
            WriteCommand(0x02);

            WriteCommand(LCD_FUNCTIONSET | LCD_2LINE | LCD_5x8DOTS | LCD_4BITMODE);
            WriteCommand(LCD_DISPLAYCONTROL | LCD_DISPLAYON);
            WriteCommand(LCD_CLEARDISPLAY);
            WriteCommand(LCD_ENTRYMODESET | LCD_ENTRYLEFT);
            _timer.SleepMiliseconds(1);
        }

        /// <summary>
        /// clocks EN to latch command
        /// </summary>
        /// <param name="data"></param>
        private void Strobe(int data)
        {
            WriteCommand(data | En | LCD_BACKLIGHT);
            _timer.SleepMiliseconds(1);
            WriteCommand(((data & ~En) | LCD_BACKLIGHT));
            _timer.SleepMiliseconds(1);
        }

        private void WriteFourBits(int data)
        {
            WriteCommand(data | LCD_BACKLIGHT);
            Strobe(data);
        }

        /// <summary>
        /// write a command to lcd
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="mode"></param>
        public void Write(int cmd, int mode = 0)
        {
            WriteFourBits(mode | (cmd & 0xF0));
            WriteFourBits(mode | ((cmd << 4) & 0xF0));
        }

        /// <summary>
        /// write a character to lcd (or character rom) 0x09: backlight | RS=DR<
        /// </summary>
        /// <param name="charvalue"></param>
        /// <param name="mode"></param>
        public void WriteChar(int charvalue, int mode = 1)
        {
            WriteFourBits(mode | (charvalue & 0xF0));
            WriteFourBits(mode | ((charvalue << 4) & 0xF0));
        }

        /// <summary>
        /// put string function with optional char positioning
        /// </summary>
        /// <param name="text"></param>
        /// <param name="line"></param>
        /// <param name="pos"></param>
        public void DisplayString(string text, int line = 1, int pos = 0)
        {
            int pos_new = 0;
            if (line == 1)
            {
                pos_new = pos;
            }
            else if (line == 2)
            {
                pos_new = 0x40 + pos;
            }

            Write(0x80 + pos_new);

            foreach (var c in text)
            {
                Write((int)c, Rs);
            }
        }

        /// <summary>
        /// clear lcd and set to home
        /// </summary>
        public void Clear()
        {
            Write(LCD_CLEARDISPLAY);
            Write(LCD_RETURNHOME);
        }

        /// <summary>
        /// clear lcd and set to home
        /// </summary>
        public void ReturnHome()
        {
            Write(LCD_RETURNHOME);
        }

        /// <summary>
        /// define backlight on/off (lcd.backlight(1); off= lcd.backlight(0)
        /// </summary>
        /// <param name="state"></param>
        public void SetBacklight(int state)
        {
            if (state == 1)
            {
                WriteCommand(LCD_BACKLIGHT);
            }
            else if (state == 0)
            {
                WriteCommand(LCD_NOBACKLIGHT);
            }
        }
    }
}