using System;

namespace RPI.WiringPiWrapper.Hardware.GPIOBoard.Exceptions
{
    public class GPIOInitializationException : Exception
    {
        public GPIOInitializationException() : base("Error during GPIO initialization")
        {
        }
    }
}