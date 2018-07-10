namespace RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO
{
    public class GPIOWrapper : IWrapGPIO
    {
        public void ClockSetGpio(int pin, int freq)
        {
            WiringPi.GPIO.ClockSetGpio(pin, freq);
        }

        public int DigitalRead(int pin)
        {
            return WiringPi.GPIO.DigitalRead(pin);
        }

        public void DigitalWrite(int pin, WiringPi.GPIO.GPIOpinvalue value)
        {
            WiringPi.GPIO.DigitalWrite(pin, (int)value);
        }

        public void DigitalWriteByte(int value)
        {
            WiringPi.GPIO.DigitalWriteByte(value);
        }

        public void PinMode(int pin, int mode)
        {
            WiringPi.GPIO.PinMode(pin, mode);
        }

        public void PullUpDnControl(int pin, int pud)
        {
            WiringPi.GPIO.PullUpDnControl(pin, pud);
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

        public void PWMWrite(int pin, int value)
        {
            WiringPi.GPIO.PWMWrite(pin, value);
        }
    }
}