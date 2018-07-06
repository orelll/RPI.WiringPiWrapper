using RPI.WiringPiWrapper.Base.WiringPi;

namespace RPI.WiringPiWrapper.Base.Hardware.Pins
{
    public abstract class PinBase
    {
        public int Number { get; }
        public GPIO.GPIOpinmode Mode { get; }

        public PinBase(int number, GPIO.GPIOpinmode mode) => (Number, Mode) = (number, mode);
    }
}