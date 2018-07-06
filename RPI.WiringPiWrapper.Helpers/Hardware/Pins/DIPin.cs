using RPI.WiringPiWrapper.Base.WiringPi;

namespace RPI.WiringPiWrapper.Base.Hardware.Pins
{
    public class DIPin : PinBase
    {
        public DIPin(int number) : base(number, GPIO.GPIOpinmode.Input)
        {
        }

        public double Value => GPIO.DigitalRead(Number);
    }
}