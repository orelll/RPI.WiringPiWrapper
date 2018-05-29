using rpi.wiringpiwrapper;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RPI.WiringPiWrapper
{
    public class PWMServoDriver3
    {
        private const byte PCA9685_MODE1 = 0x0;
        private const byte PCA9685_PRESCALE = 0xFE;
        private const byte SUBADR1 = 0x02;
        private const byte SUBADR2 = 0x03;
        private const byte SUBADR3 = 0x04;
        private const byte MODE1 = 0x00;
        private const byte PRESCALE = 0xFE;
        private const byte LED0_ON_L = 0x06;
        private const byte LED0_ON_H = 0x07;
        private const byte LED0_OFF_L = 0x08;
        private const byte LED0_OFF_H = 0x09;
        private const byte ALLLED_ON_L = 0xFA;
        private const byte ALLLED_ON_H = 0xFB;
        private const byte ALLLED_OFF_L = 0xFC;
        private const byte ALLLED_OFF_H = 0xFD;

        private int _deviceHandle;

        public PWMServoDriver3(int deviceAddress)
        {
            var response = I2C.wiringPiI2CSetup(deviceAddress);

            if (response < 1)
            {
                Console.WriteLine("There is a problem with PWM driver initialization");
                return;
            }

            _deviceHandle = response;
            Console.WriteLine($"Obtained device handle: {_deviceHandle}");
        }

        public void SetPWMFreq(float freq)
        {
            float prescaleval = 25000000;                   // osc_clock
            prescaleval /= 4096;
            prescaleval /= freq;
            var prescale = (byte)(prescaleval - 1);

            Console.WriteLine($"{GetMethodName()}: response:  {I2C.wiringPiI2CWriteReg8(_deviceHandle, PCA9685_MODE1, 0x31)}");           // SLEEP on
            Console.WriteLine($"{GetMethodName()}: response:  {I2C.wiringPiI2CWriteReg8(_deviceHandle, PCA9685_PRESCALE, prescale)}");    // write PWM output frequency
            Console.WriteLine($"{GetMethodName()}: response:  {I2C.wiringPiI2CWriteReg8(_deviceHandle, PCA9685_MODE1, 0x21)}");           // SLEEP off
            Thread.Sleep(5);
            Console.WriteLine($"{GetMethodName()}: response:  {I2C.wiringPiI2CWriteReg8(_deviceHandle, PCA9685_MODE1, 0xA1)}");           // 連続書き込みで自動的にレジスタ位置移動
        }

        public void setPWM(byte num, ushort pwmOn, ushort pwmOff)
        {
            var bOn = BitConverter.GetBytes(pwmOn);
            var bOff = BitConverter.GetBytes(pwmOff);
            var data = new byte[] { (byte)(LED0_ON_L + 4 * num), bOn[0], bOn[1], bOff[0], bOff[1] };
            //var dataAsInt = BitConverter.ToInt32(data, 0);

            Console.WriteLine($"{GetMethodName()}:writing ({ (LED0_ON_L + 4 * num) }) response:  {I2C.wiringPiI2CWrite(_deviceHandle, (LED0_ON_L + 4 * num))}");
            Console.WriteLine($"{GetMethodName()}:writing ({ bOn[0] }) response:  {I2C.wiringPiI2CWrite(_deviceHandle, bOn[0])}");
            Console.WriteLine($"{GetMethodName()}:writing ({ bOn[1] }) response:  {I2C.wiringPiI2CWrite(_deviceHandle, bOn[1])}");
            Console.WriteLine($"{GetMethodName()}:writing ({ bOff[0] }) response:  {I2C.wiringPiI2CWrite(_deviceHandle, bOff[0])}");
            Console.WriteLine($"{GetMethodName()}:writing ({ bOff[1] }) response:  {I2C.wiringPiI2CWrite(_deviceHandle, bOff[1])}");
        }

        private string GetMethodName([CallerMemberName] string caller = "")
        {
            return caller;
        }
    }
}