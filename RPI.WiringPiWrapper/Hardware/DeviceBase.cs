using System;
using System.Collections.Generic;
using FluentGuard;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.Helpers.Loggers;

namespace RPI.WiringPiWrapper.Hardware
{
    public abstract class DeviceBase : IDevice
    {
        internal readonly ILogger _log;

        public IEnumerable<int> ListUsedPins => new List<int>();

        protected DeviceBase(ILogger logger)
        {
            _log = FluentGuard<ILogger>.On(logger).WhenNull().ThrowOnErrors();
        }

        protected DeviceBase() : this(new DummyLogger())
        {
        }

        public abstract void LogDeviceStartup();
    }
}