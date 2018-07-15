using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi;

namespace RPI.WiringPiWrapper.Hardware.Pins
{
    public abstract class PinBase:IPin
    {
        public int Number { get; }
        public GPIO.GPIOpinmode PinMode { get; }

        public PinBase(int number, GPIO.GPIOpinmode mode) => (Number, PinMode) = (number, mode);

        public override string ToString()
        {
            return $"nr.: {Number}, mode: {PinMode}";
        }
    }
}