using System;

namespace RPI.WiringPiWrapper.Helpers.Exceptions
{
    public class I2CInitializationException : Exception
    {
        public I2CInitializationException(int deviceAddress) : base($"Error during initialization exception (address: {deviceAddress})")
        {
        }
    }
}