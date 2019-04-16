using FluentGuard;
using RPI.WiringPiWrapper.Hardware.GPIOBoard.Exceptions;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Init;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPI.WiringPiWrapper.Hardware.GPIOBoard
{
    public class GPIOBoard
    {
        public IList<IDevice> DevicesList
        {
            get { return _devicesList; }
        }
        private IList<IDevice> _devicesList;

        public IList<IPin> PinsList
        {
            get
            {
               return _pinsList;
            }
        }
        private IList<IPin> _pinsList;

        private readonly ILogger _log;
        private readonly IWrapInit _init;
        private IWrapGPIO _gpio;

        public GPIOBoard(ILogger log, IWrapInit init)
        {
            _log = FluentGuard<ILogger>.On(log).WhenNull().ThrowOnErrors();
            _init = FluentGuard<IWrapInit>.On(init).WhenNull().ThrowOnErrors();

            Initialize();
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

        private void Initialize()
        {
            if (_init.WiringPiSetup() == -1)
                throw new GPIOInitializationException();

            _devicesList = new List<IDevice>();
            _pinsList = new List<IPin>();

            _log.WriteMessage("GPIO initialized");
        }
    }
}