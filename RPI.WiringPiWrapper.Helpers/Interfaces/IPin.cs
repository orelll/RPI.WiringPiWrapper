using RPI.WiringPiWrapper.Base.WiringPi;

namespace RPI.WiringPiWrapper.Helpers.Interfaces
{
    public interface IPin
    {
        int Address { get; }
        GPIO.GPIOpinmode PinMode { get; }

        GPIO.GPIOpinvalue GetValue();

        void SetValue(GPIO.GPIOpinvalue stateToSet);
    }
}