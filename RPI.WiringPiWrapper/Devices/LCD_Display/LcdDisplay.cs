using RPI.WiringPiWrapper.Hardware;
using RPI.WiringPiWrapper.Interfaces;

namespace RPI.WiringPiWrapper.Devices.LCD_Display
{
    public class LcdDisplay : I2CDeviceBase
    {
        #region commands

        private int _lcdCleardisplay = 0x01;

        private int _lcdReturnhome = 0x02;
        private int _lcdEntrymodeset = 0x04;
        private int _lcdDisplaycontrol = 0x08;
        private int _lcdCursorshift = 0x10;
        private int _lcdFunctionset = 0x20;
        private int _lcdSetcgramaddr = 0x40;
        private int _lcdSetddramaddr = 0x80;

        //# flags for display entry mode
        private int _lcdEntryright = 0x00;

        private int _lcdEntryleft = 0x02;
        private int _lcdEntryshiftincrement = 0x01;
        private int _lcdEntryshiftdecrement = 0x00;

        //# flags for display on/off control
        private int _lcdDisplayon = 0x04;

        private int _lcdDisplayoff = 0x00;
        private int _lcdCursoron = 0x02;
        private int _lcdCursoroff = 0x00;
        private int _lcdBlinkon = 0x01;
        private int _lcdBlinkoff = 0x00;

        //# flags for display/cursor shift
        private int _lcdDisplaymove = 0x08;

        private int _lcdCursormove = 0x00;
        private int _lcdMoveright = 0x04;
        private int _lcdMoveleft = 0x00;

        //# flags for function set
        private int _lcd_8Bitmode = 0x10;

        private int _lcd_4Bitmode = 0x00;
        private int _lcd_2Line = 0x08;
        private int _lcd_1Line = 0x00;
        private int _lcd_5X10Dots = 0x04;
        private int _lcd_5X8Dots = 0x00;

        //# flags for backlight control
        private int _lcdBacklight = 0x08;

        private int _lcdNobacklight = 0x00;

        private int _en = 0b00000100;// # Enable bit;
        private int _rw = 0b00000010;// # Read/Write bit
        private int _rs = 0b00000001;// # Register select bit

        #endregion commands

        //TODO: address should be hidden above some abstraction
        public LcdDisplay(ITimer timer, ILogger logger, int address = 0x3f) : base(address, logger, timer)
        {
            Initialize();
        }

        private void Initialize()
        {
            _log.WriteMessage($"Initializing device {DeviceHandler} ({this.GetType()})");

            WriteCommand(0x03);
            WriteCommand(0x03);
            WriteCommand(0x03);
            WriteCommand(0x02);

            WriteCommand(_lcdFunctionset | _lcd_2Line | _lcd_5X8Dots | _lcd_4Bitmode);
            WriteCommand(_lcdDisplaycontrol | _lcdDisplayon);
            WriteCommand(_lcdCleardisplay);
            WriteCommand(_lcdEntrymodeset | _lcdEntryleft);
            _timer.SleepMiliseconds(2);

            _log.WriteMessage("Initialization finished");
        }

        /// <summary>
        /// clocks EN to latch command
        /// </summary>
        /// <param name="data"></param>
        private void Strobe(int data)
        {
            WriteCommand(data | _en | _lcdBacklight);
            _timer.SleepMiliseconds(1);
            WriteCommand(((data & ~_en) | _lcdBacklight));
            _timer.SleepMiliseconds(1);
        }

        private void WriteFourBits(int data)
        {
            WriteCommand(data | _lcdBacklight);
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
            int posNew = 0;
            if (line == 1)
            {
                posNew = pos;
            }
            else if (line == 2)
            {
                posNew = 0x40 + pos;
            }

            Write(0x80 + posNew);

            foreach (var c in text)
            {
                Write((int)c, _rs);
            }
        }

        /// <summary>
        /// clear lcd and set to home
        /// </summary>
        public void Clear()
        {
            Write(_lcdCleardisplay);
            Write(_lcdReturnhome);
        }

        /// <summary>
        /// clear lcd and set to home
        /// </summary>
        public void ReturnHome()
        {
            Write(_lcdReturnhome);
        }

        /// <summary>
        /// define backlight on/off (lcd.backlight(1); off= lcd.backlight(0)
        /// </summary>
        /// <param name="state"></param>
        public void SetBacklight(int state)
        {
            if (state == 1)
            {
                WriteCommand(_lcdBacklight);
            }
            else if (state == 0)
            {
                WriteCommand(_lcdNobacklight);
            }
        }
    }
}