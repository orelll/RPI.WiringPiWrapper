using RPI.WiringPiWrapper.WiringPi;

namespace RPI.WiringPiWrapper.Hardware.Pins
{
    public class DIPin : PinBase
    {
        public DIPin(int number) : base(number, GPIO.GPIOpinmode.Input)
        {
        }

        public double Value => GPIO.DigitalRead(Number);
    }
}