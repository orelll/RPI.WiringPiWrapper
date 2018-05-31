using RPI.WiringPiWrapper.Helpers;
using System;
using System.Threading;
using static RPI.WiringPiWrapper.Helpers.GPIO;

namespace rpi.wiringpiwrapper
{

    class I2CBasics
    {
        private int _vccPin = 1;
        private int _deviceToConnect = 0x3f;
        private int _deviceHandle;

        #region  variables

        //# commands
        int LCD_CLEARDISPLAY = 0x01;
        int LCD_RETURNHOME = 0x02;
        int LCD_ENTRYMODESET = 0x04;
        int LCD_DISPLAYCONTROL = 0x08;
        int LCD_CURSORSHIFT = 0x10;
        int LCD_FUNCTIONSET = 0x20;
        int LCD_SETCGRAMADDR = 0x40;
        int LCD_SETDDRAMADDR = 0x80;

        //# flags for display entry mode
        int LCD_ENTRYRIGHT = 0x00;
        int LCD_ENTRYLEFT = 0x02;
        int LCD_ENTRYSHIFTINCREMENT = 0x01;
        int LCD_ENTRYSHIFTDECREMENT = 0x00;

        //# flags for display on/off control
        int LCD_DISPLAYON = 0x04;
        int LCD_DISPLAYOFF = 0x00;
        int LCD_CURSORON = 0x02;
        int LCD_CURSOROFF = 0x00;
        int LCD_BLINKON = 0x01;
        int LCD_BLINKOFF = 0x00;

        //# flags for display/cursor shift
        int LCD_DISPLAYMOVE = 0x08;
        int LCD_CURSORMOVE = 0x00;
        int LCD_MOVERIGHT = 0x04;
        int LCD_MOVELEFT = 0x00;

        //# flags for function set
        int LCD_8BITMODE = 0x10;
        int LCD_4BITMODE = 0x00;
        int LCD_2LINE = 0x08;
        int LCD_1LINE = 0x00;
        int LCD_5x10DOTS = 0x04;
        int LCD_5x8DOTS = 0x00;

        //# flags for backlight control
        int LCD_BACKLIGHT = 0x08;
        int LCD_NOBACKLIGHT = 0x00;

        int En = 0b00000100;// # Enable bit
        int Rw = 0b00000010;// # Read/Write bit
        int Rs = 0b00000001;// # Register select bit


        #endregion

        public void Connect()
        {
            if (Init.WiringPiSetup() == -1)
            {
                Console.WriteLine($"Gpio initialization failed");
                return;
            }

            Console.WriteLine($"Trying to connect with device... { _deviceToConnect }");
            _deviceHandle = I2C.wiringPiI2CSetup(_deviceToConnect);
            //_deviceHandle = _deviceToConnect;

            TurnOnPower();

            //fd =  lcdInit (2, 16, 4,  11,10 , 0,1,2,3,0,0,0,0) ;
            InitializeDisplay();

            Console.WriteLine($"Response: {_deviceHandle}");


        }

        public void TurnOnPower()
        {
            Console.WriteLine("Turning on display power....");
            GPIO.pinMode(_vccPin, (int)GPIOpinmode.Output);
            GPIO.digitalWrite(_vccPin, (int)GPIOpinvalue.High);
            Thread.Sleep(1000);
            Console.WriteLine("LCD turned on");
        }

        public void InitializeDisplay()
        {
            Console.WriteLine("Initializing display...");
            Thread.Sleep(150);

            ///pure screen initialization
            WriteData(0x03);
            Thread.Sleep(1);
            WriteData(0x03);
            Thread.Sleep(1);
            WriteData(0x03);
            Thread.Sleep(1);

            WriteData(LCD_FUNCTIONSET | LCD_2LINE | LCD_5x8DOTS | LCD_4BITMODE);
            WriteData(LCD_DISPLAYCONTROL | LCD_DISPLAYON);
            WriteData(LCD_CLEARDISPLAY);
            WriteData(LCD_ENTRYMODESET | LCD_ENTRYLEFT);
            Thread.Sleep(2);

            Console.WriteLine("dupsko");
            Console.WriteLine("Display initialized");

        }

        private void WriteData(int data)
        {
            Console.WriteLine($"Array as int: { GetIntBinaryString(data) }");
            Console.WriteLine($"Response: { I2C.wiringPiI2CWrite(_deviceHandle, data)}");
        }

        static string GetIntBinaryString(int n)
        {
            char[] b = new char[32];
            int pos = 31;
            int i = 0;

            while (i < 32)
            {
                if ((n & (1 << i)) != 0)
                {
                    b[pos] = '1';
                }
                else
                {
                    b[pos] = '0';
                }
                pos--;
                i++;
            }
            return new string(b);
        }
    }

}