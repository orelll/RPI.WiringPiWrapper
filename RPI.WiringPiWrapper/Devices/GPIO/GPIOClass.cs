using RPI.WiringPiWrapper.Devices.GPIO.Exceptions;
using RPI.WiringPiWrapper.Helpers;
using RPI.WiringPiWrapper.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPI.WiringPiWrapper.Devices.GPIO
{
    public class GPIOClass
    {
        private ILogger _log;

        private List<IDevice> _devicesCollection;
        public IEnumerable<IDevice> DevicesCollection => _devicesCollection;
        
        public GPIOClass(ILogger log)
        {
            if (log == null) throw new ArgumentNullException(nameof(log));

            _log = log;

            Initialize();
        }

        private void Initialize()
        {
            if (Init.WiringPiSetup() == -1)
            {
                throw new GPIOInitializationException();
            }

            _devicesCollection = new List<IDevice>();

            _log.WriteMessage("GPIO initialized");
        }

        public void AddDevice(IDevice deviceToAdd)
        {
            _devicesCollection.Add(deviceToAdd);
            _log.WriteMessage($"Device '{ deviceToAdd.ToString()}' added");
        }

        public bool RemoveDevice(IDevice deviceToRemove)
        {
            var foundDevice = _devicesCollection.FirstOrDefault(dev => dev.Equals(deviceToRemove));

            if (foundDevice != null) _devicesCollection.Remove(foundDevice);

            _log.WriteMessage($"device '{deviceToRemove.ToString()}' removed");

            return foundDevice != null;
        }
    }
}