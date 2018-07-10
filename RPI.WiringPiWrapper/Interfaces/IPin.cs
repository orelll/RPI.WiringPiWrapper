using RPI.WiringPiWrapper.WiringPi;

namespace RPI.WiringPiWrapper.Interfaces
{
    public interface IPin
    {
        int Number { get; }
        GPIO.GPIOpinmode PinMode { get; }
    }
}