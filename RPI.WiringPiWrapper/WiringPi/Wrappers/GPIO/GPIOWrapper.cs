using RPI.WiringPiWrapper.Hardware.Pins;
using RPI.WiringPiWrapper.Interfaces;
using static RPI.WiringPiWrapper.WiringPi.GPIO;

namespace RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO
{
    public class GPIOWrapper : IWrapGPIO
    {
        public void ClockSetGpio(IPin pin, int freq)
        {
            WiringPi.GPIO.ClockSetGpio(pin.Number, freq);
        }

        public GPIOpinvalue DigitalRead(IPin pin)
        {
            PinValidator.Using(pin).ValidateMode(GPIOpinmode.Input);

            var readedData = WiringPi.GPIO.DigitalRead(pin.Number);

            switch(readedData)
            {
                case (int)GPIOpinvalue.High:
                    return GPIOpinvalue.High;
                case (int)GPIOpinvalue.Low:
                    return GPIOpinvalue.Low;
                default:
                    throw new System.Exception($"Readed value not applicable to GPIOpinvalue");
            }
        }

        public void DigitalWrite(IPin pin, GPIOpinvalue value)
        {
            PinValidator.Using(pin).ValidateMode(GPIOpinmode.Output);
            WiringPi.GPIO.DigitalWrite(pin.Number, (int)value);
        }

        public void DigitalWriteByte(int value)
        {
            WiringPi.GPIO.DigitalWriteByte(value);
        }

        public void PinMode(IPin pin)
        {
            WiringPi.GPIO.PinMode(pin.Number, (int)pin.PinMode);
        }

        public void PullUpDnControl(IPin pin, int pud)
        {
            WiringPi.GPIO.PullUpDnControl(pin.Number, pud);
        }

        public void PWMSetClock(int divisor)
        {
            WiringPi.GPIO.PWMSetClock(divisor);
        }

        public void PWMSetMode(int mode)
        {
            WiringPi.GPIO.PWMSetMode(mode);
        }

        public void PWMSetRange(uint range)
        {
            WiringPi.GPIO.PWMSetRange(range);
        }

        public void PWMWrite(IPin pin, int value)
        {
            WiringPi.GPIO.PWMWrite(pin.Number, value);
        }
    }
}