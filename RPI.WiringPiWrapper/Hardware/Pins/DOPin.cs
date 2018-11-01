using RPI.WiringPiWrapper.WiringPi;

namespace RPI.WiringPiWrapper.Hardware.Pins
{
    public class DOPin : PinBase
    {
        public DOPin(int number) : base(number, GPIO.GPIOpinmode.Output)
        {
        }

        public void SetValue(GPIO.GPIOpinvalue value)
        {
            GPIO.DigitalWrite(Number, (int) value);
        }
    }
}