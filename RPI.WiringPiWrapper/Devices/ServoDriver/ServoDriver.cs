using System;
using System.Threading;
using RPI.WiringPiWrapper.WiringPi;

namespace RPI.WiringPiWrapper.Devices.ServoDriver
{
    /// <summary>
    /// Used to initialise Gordon's library, there's 4 different ways to initialise and we're going to support all 4
    /// </summary>
    public class ServoDriver
    {
        private const byte REGISTER1 = 0x00;
        private const byte REGISTER2 = 0x01;
        private const byte PCA9685_PRESCALE = 0xFE;
        private const byte SUBADR1 = 0x02;
        private const byte SUBADR2 = 0x03;
        private const byte SUBADR3 = 0x04;
        private const byte MODE1 = 0x01;
        private const byte MODE2 = 0x04;
        private const byte PRESCALE = 0xFE;
        private const byte LED0_ON_L = 0x06;
        private const byte LED0_ON_H = 0x07;
        private const byte LED0_OFF_L = 0x08;
        private const byte LED0_OFF_H = 0x09;
        private const byte ALLLED_ON_L = 0xFA;
        private const byte ALLLED_ON_H = 0xFB;
        private const byte ALLLED_OFF_L = 0xFC;
        private const byte ALLLED_OFF_H = 0xFD;

        private int _deviceHandler;

        public ServoDriver(int deviceAddress = 0x40)
        {
            Console.WriteLine($"Initializing servo driver using {deviceAddress} address");
            _deviceHandler = I2C.WiringPiI2CSetup(deviceAddress);

            if (_deviceHandler < 1)
            {
                Console.WriteLine($"Error during obtaining device handler");
                return;
            }

            Console.WriteLine($"End of constructor. Device handler: ({ _deviceHandler:X4})");
        }

        public void Configure()
        {
            Console.WriteLine($"Setting register1 {MODE1:X4}, resp: {I2C.WiringPiI2CWriteReg8(_deviceHandler, REGISTER1, MODE1)}");

            Console.WriteLine($"Setting register2 {MODE2:X4}, resp: { I2C.WiringPiI2CWriteReg8(_deviceHandler, REGISTER2, MODE2)}");

            Console.WriteLine($"Validating registers...");
            var tempRegValue = I2C.WiringPiI2CReadReg8(_deviceHandler, REGISTER1);

            if (tempRegValue == MODE1)
            {
                Console.WriteLine("Register1 is ok");
            }
            else
            {
                Console.WriteLine($"Register1 is configured wrong {MODE1:X4} vs {tempRegValue:X4}");
                throw new Exception("Register1 is configured wrong");
            }

            tempRegValue = I2C.WiringPiI2CReadReg8(_deviceHandler, REGISTER2);

            if (tempRegValue == MODE2)
            {
                Console.WriteLine("Register2 is ok");
            }
            else
            {
                Console.WriteLine($"Register2 is configured wrong {MODE2:X4} vs {tempRegValue:X4}");
                throw new Exception("Register2 is configured wrong");
            }
        }

        public void Reset()
        {
            Console.WriteLine("Resetting device...");
            var reg1Value = I2C.WiringPiI2CReadReg8(_deviceHandler, REGISTER1);
            Console.WriteLine($"Obtained value: {reg1Value:X8}");

            if (IsBitSet(reg1Value, 7))
            {
                ClearBit(ref reg1Value, 4);
                Thread.Sleep(1);
            }

            SetBit(ref reg1Value, 7);
            I2C.WiringPiI2CWriteReg8(_deviceHandler, REGISTER1, 0x00);
            Console.WriteLine($"Setting register1... {I2C.WiringPiI2CWriteReg8(_deviceHandler, REGISTER1, reg1Value)}");
            Thread.Sleep(1);

            reg1Value = I2C.WiringPiI2CReadReg8(_deviceHandler, REGISTER1);
            Console.WriteLine($"Register1 after resetting: {reg1Value:X8}");
        }

        public void SetPWM(int value)
        {
            int on = 0;
            int off = value;
            var channel = 1;

            I2C.WiringPiI2CWriteReg8(_deviceHandler, LED0_ON_L + 4 * channel, on & 0xFF);
            I2C.WiringPiI2CWriteReg8(_deviceHandler, LED0_ON_H + 4 * channel, on >> 8);
            I2C.WiringPiI2CWriteReg8(_deviceHandler, LED0_OFF_L + 4 * channel, off & 0xFF);
            I2C.WiringPiI2CWriteReg8(_deviceHandler, LED0_OFF_H + 4 * channel, off >> 8);
        }

        #region helpers

        private bool IsBitSet(int b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private void SetBit(ref int b, int pos)
        {
            b |= 1 << pos;
        }

        private void ClearBit(ref int b, int pos)
        {
            b &= ~(1 << pos);
        }

        #endregion helpers
    }
}