using System;
using System.Collections.Generic;
using System.Linq;
using RPI.WiringPiWrapper.Hardware.GPIOBoard.Exceptions;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Init;

namespace RPI.WiringPiWrapper.Hardware.GPIOBoard
{
    public class GPIOBoard
    {
        public IList<IDevice> DevicesList => _devicesList;
        private IList<IDevice> _devicesList;

        public IList<IPin> PinsList => _pinsList;
        private IList<IPin> _pinsList;

        private ILogger _log;
        private IWrapInit _init;
        private IWrapGPIO _gpio;

        public GPIOBoard(ILogger log, IWrapInit init)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _init = init ?? throw new ArgumentNullException(nameof(init));

            Initialize();
        }

        private void Initialize()
        {
            if (_init.WiringPiSetup() == -1)
            {
                throw new GPIOInitializationException();
            }

            _devicesList = new List<IDevice>();
            _pinsList = new List<IPin>();

            _log.WriteMessage("GPIO initialized");
        }

        public void Validate()
        {
            throw new NotImplementedException();
        }

        public void AddDevice(IDevice device) => DevicesList.Add(device);

        public IDevice GetDevice(Func<IDevice, bool> searchFunc) => DevicesList.FirstOrDefault(searchFunc);
        public IEnumerable<IDevice> SearchDevices(Func<IDevice, bool> searchFunc) => DevicesList.Where(searchFunc);

        public IPin GetPin(Func<IPin, bool> searchFunc) => PinsList.FirstOrDefault(searchFunc);
        public IEnumerable<IPin> SearchPins(Func<IPin, bool> searchFunc) => PinsList.Where(searchFunc);

    }
}