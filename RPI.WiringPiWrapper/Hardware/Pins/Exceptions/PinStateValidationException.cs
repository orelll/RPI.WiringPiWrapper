using System;

namespace RPI.WiringPiWrapper.Hardware.Pins.Exceptions
{
    public class PinStateValidationException : Exception
    {
        public PinStateValidationException(string message) : base(message)
        {
        }
    }
}