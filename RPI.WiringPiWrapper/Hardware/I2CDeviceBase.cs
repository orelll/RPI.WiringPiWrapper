using RPI.WiringPiWrapper.Exceptions;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi;
using System;
using System.Collections.Generic;

namespace RPI.WiringPiWrapper.Hardware
{
    public class I2CDeviceBase : DeviceBase, II2CDevice
    {
        private int _addr;
        protected ITimer _timer;
        public int _deviceHandler;

        public I2CDeviceBase(int addr, ILogger logger, ITimer timer) : base(logger)
        {
            _timer = timer ?? throw new ArgumentException(nameof(timer));

            _addr = addr;
            _deviceHandler = I2C.WiringPiI2CSetup(_addr);

            if (_deviceHandler < 1) throw new I2CInitializationException(_addr);

            _log.WriteMessage($"Obtained device handler: {_deviceHandler:X4}");
        }

        //# Write a single command
        public void WriteCommand(int cmd)
        {
            I2C.WiringPiI2CWrite(_deviceHandler, cmd);
            _timer.SleepMiliseconds(1);
        }

        //# Read a single byte
        public int Read()
        {
            return I2C.WiringPiI2CRead(_deviceHandler);
        }

        /// <summary>
        /// return SDA && SCL pins
        /// </summary>
        public new IEnumerable<int> ListUsedPins => new List<int> { 8, 9 };
    }
}