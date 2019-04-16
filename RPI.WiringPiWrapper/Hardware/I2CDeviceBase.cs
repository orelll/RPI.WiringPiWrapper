using System;
using FluentGuard;
using RPI.WiringPiWrapper.Exceptions;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi;
using System.Collections.Generic;

namespace RPI.WiringPiWrapper.Hardware
{
    public class I2CDeviceBase : DeviceBase, II2CDevice
    {
        private int _addr;
        protected ITimer _timer;
        public int DeviceHandler { get; }

        //TODO: address can break container configuration. Include it into some kind of config provider
        public I2CDeviceBase(int addr, ILogger logger, ITimer timer) : base(logger)
        {
            _timer = FluentGuard<ITimer>.On(timer).WhenNull().ThrowOnErrors();

            _addr = addr > 0 ? addr : throw new ArgumentException(nameof(addr));
            DeviceHandler = I2C.WiringPiI2CSetup(_addr);

            if (DeviceHandler < 1) throw new I2CInitializationException(_addr);

            LogDeviceStartup();
        }

        public override void LogDeviceStartup()
        {
            _log.WriteMessage($"Obtained device handler: {DeviceHandler:X4}");
        }

        //# Write a single command

        public void WriteCommand(int cmd)
        {
            I2C.WiringPiI2CWrite(DeviceHandler, cmd);
            _timer.SleepMiliseconds(1);
        }

        //# Read a single byte

        public int Read()
        {
            return I2C.WiringPiI2CRead(DeviceHandler);
        }

        /// <summary>
        /// return SDA && SCL pins
        /// </summary>
        public new IEnumerable<int> ListUsedPins => new List<int> {8, 9};
    }
}