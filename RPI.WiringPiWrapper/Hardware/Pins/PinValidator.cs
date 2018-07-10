using RPI.WiringPiWrapper.Hardware.Pins.Exceptions;
using RPI.WiringPiWrapper.Interfaces;
using System;
using static RPI.WiringPiWrapper.WiringPi.GPIO;

namespace RPI.WiringPiWrapper.Hardware.Pins
{
    public class PinValidator
    {
        private IPin _target;

        public PinValidator(IPin target) => (_target) = (target);

        public static PinValidator Using(IPin pinToValidate)
        {
            return new PinValidator(pinToValidate);
        }

        public PinValidator ValidateMode(GPIOpinmode mode)
        {
            return _target.PinMode == mode ? this : throw new PinStateValidationException($"Given pin state: { _target.PinMode.ToString()} vs { mode.ToString()}");
        }

        public PinValidator AgainstNull()
        {
            return _target != null ? this : throw new ArgumentNullException(nameof(_target));
        }
    }
}