namespace RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO
{
    public interface IWrapGPIO
    {
        //Uses Gpio pin numbers
        void PinMode(int pin, int mode);

        //Uses Gpio pin numbers
        void DigitalWrite(int pin, WiringPi.GPIO.GPIOpinvalue value);

        //Uses Gpio pin numbers
        void DigitalWriteByte(int value);

        //Uses Gpio pin numbers
        int DigitalRead(int pin);

        //Uses Gpio pin numbers
        void PullUpDnControl(int pin, int pud);

        //This pwm mode cannot be used when using GpioSys mode!!
        //Uses Gpio pin numbers
        void PWMWrite(int pin, int value);

        //Uses Gpio pin numbers
        void PWMSetMode(int mode);

        //Uses Gpio pin numbers
        void PWMSetRange(uint range);

        //Uses Gpio pin numbers
        void PWMSetClock(int divisor);

        //Uses Gpio pin numbers
        void ClockSetGpio(int pin, int freq);
    }
}