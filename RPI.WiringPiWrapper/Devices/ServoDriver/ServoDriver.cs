using FluentGuard;
using RPI.WiringPiWrapper.Hardware;
using RPI.WiringPiWrapper.Helpers;
using RPI.WiringPiWrapper.Helpers.Loggers;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi;
using RPI.WiringPiWrapper.WiringPi.Wrappers.I2C;
using System;
using System.Threading;

namespace RPI.WiringPiWrapper.Devices.ServoDriver
{
    /// <summary>
    /// Used to initialise Gordon's library, there's 4 different ways to initialise and we're going to support all 4
    /// </summary>
    public class ServoDriver : I2CDeviceBase
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

        private readonly IWrapI2C _i2cWrapper;

        public ServoDriver(IWrapI2C i2cWrapper, int deviceAddress, ILogger logger, ITimer timer) : base(deviceAddress,
            logger, timer)
        {
            _i2cWrapper = FluentGuard<IWrapI2C>.On(i2cWrapper).WhenNull().ThrowOnErrors();
        }

        public ServoDriver() : this(new I2CWrapper(), 0x40, new DummyLogger(), new TimerClass())
        {
        }

        public void Configure()
        {
            _log.WriteMessage(
                $"Setting register1 {MODE1:X4}, resp: {I2C.WiringPiI2CWriteReg8(DeviceHandler, REGISTER1, MODE1)}");

            _log.WriteMessage(
                $"Setting register2 {MODE2:X4}, resp: {I2C.WiringPiI2CWriteReg8(DeviceHandler, REGISTER2, MODE2)}");

            _log.WriteMessage($"Validating registers...");
            var tempRegValue = I2C.WiringPiI2CReadReg8(DeviceHandler, REGISTER1);

            if (tempRegValue == MODE1)
            {
                _log.WriteMessage("Register1 is ok");
            }
            else
            {
                _log.WriteMessage($"Register1 is configured wrong {MODE1:X4} vs {tempRegValue:X4}");
                throw new Exception("Register1 is configured wrong");
            }

            tempRegValue = I2C.WiringPiI2CReadReg8(DeviceHandler, REGISTER2);

            if (tempRegValue == MODE2)
            {
                _log.WriteMessage("Register2 is ok");
            }
            else
            {
                _log.WriteMessage($"Register2 is configured wrong {MODE2:X4} vs {tempRegValue:X4}");
                throw new Exception("Register2 is configured wrong");
            }
        }

        public void Reset()
        {
            _log.WriteMessage("Resetting device...");
            var reg1Value = I2C.WiringPiI2CReadReg8(DeviceHandler, REGISTER1);
            _log.WriteMessage($"Obtained value: {reg1Value:X8}");

            if (IsBitSet(reg1Value, 7))
            {
                ClearBit(ref reg1Value, 4);
                Thread.Sleep(1);
            }

            SetBit(ref reg1Value, 7);
            I2C.WiringPiI2CWriteReg8(DeviceHandler, REGISTER1, 0x00);
            _log.WriteMessage($"Setting register1... {I2C.WiringPiI2CWriteReg8(DeviceHandler, REGISTER1, reg1Value)}");
            Thread.Sleep(1);

            reg1Value = I2C.WiringPiI2CReadReg8(DeviceHandler, REGISTER1);
            _log.WriteMessage($"Register1 after resetting: {reg1Value:X8}");
        }

        public void SetPWM(int channel, int value)
        {
            var on = 0;
            var off = value;
            
            I2C.WiringPiI2CWriteReg8(DeviceHandler, LED0_ON_L + 4 * channel, on & 0xFF);
            I2C.WiringPiI2CWriteReg8(DeviceHandler, LED0_ON_H + 4 * channel, on >> 8);
            I2C.WiringPiI2CWriteReg8(DeviceHandler, LED0_OFF_L + 4 * channel, off & 0xFF);
            I2C.WiringPiI2CWriteReg8(DeviceHandler, LED0_OFF_H + 4 * channel, off >> 8);
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