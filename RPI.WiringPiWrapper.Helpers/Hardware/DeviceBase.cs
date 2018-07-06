using RPI.WiringPiWrapper.Helpers.Interfaces;
using System;

namespace RPI.WiringPiWrapper.Helpers.Bases
{
    public class DeviceBase : IDevice
    {
        internal readonly ILogger _log;

        protected DeviceBase(ILogger logger)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}