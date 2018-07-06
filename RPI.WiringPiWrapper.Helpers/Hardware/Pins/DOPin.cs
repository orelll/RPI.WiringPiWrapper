
using static RPI.WiringPiWrapper.Base.WiringPi.GPIO;

namespace RPI.WiringPiWrapper.Base.Hardware.Pins
{
    public class DOPin : PinBase
    {
        public DOPin(int number) : base(number, GPIOpinmode.Output)
        {
        }

        public void SetValue(GPIOpinvalue value) => DigitalWrite(Number, (int)value);
    }
}