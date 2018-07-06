using System;
using System.Collections.Generic;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.Tools.Loggers;

namespace RPI.WiringPiWrapper.Hardware
{
    public abstract class DeviceBase : IDevice
    {
        internal readonly ILogger _log;

        public IEnumerable<int> ListUsedPins => new List<int>();

        protected DeviceBase(ILogger logger)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected DeviceBase() : this(new DummyLogger())
        {
        }
    }
}