using System;

namespace RPI.WiringPiWrapper.Devices.GPIO.Exceptions
{
    public class GPIOInitializationException : Exception
    {
        public GPIOInitializationException() : base("Error during GPIO initialization")
        {
        }
    }
}