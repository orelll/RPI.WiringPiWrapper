using RPI.WiringPiWrapper.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPI.WiringPiWrapper.Helpers.Bases
{
    public class DeviceBase:IDevice
    {
        internal ILogger _log;
        
        public DeviceBase(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _log = logger;
        }
    }
}
