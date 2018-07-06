using RPI.WiringPiWrapper.Helpers.Exceptions;
using RPI.WiringPiWrapper.Helpers.Interfaces;
using System;
using RPI.WiringPiWrapper.Base.WiringPi;

namespace RPI.WiringPiWrapper.Helpers.Bases
{
    public class I2CDeviceBase : DeviceBase, II2CDevice
    {
        private int _addr;
        protected ITimer _timer;
        public int _deviceHandler;

        public I2CDeviceBase(int addr, ILogger logger, ITimer timer) : base(logger)
        {
            if (timer == null) throw new ArgumentException(nameof(timer));
            _timer = timer;

            _addr = addr;
            _deviceHandler = I2C.WiringPiI2CSetup(_addr);

            if (_deviceHandler < 1) throw new I2CInitializationException(_addr);

            _log.WriteMessage($"Obrained device handler: {_deviceHandler}");
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
    }
}