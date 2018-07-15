using RPI.WiringPiWrapper.Interfaces;
using static RPI.WiringPiWrapper.WiringPi.GPIO;

namespace RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO
{
    public interface IWrapGPIO
    {
        //Uses Gpio pin numbers
        void PinMode(IPin pin);

        //Uses Gpio pin numbers
        void DigitalWrite(IPin pin, GPIOpinvalue value);

        //Uses Gpio pin numbers
        void DigitalWriteByte(int value);

        //Uses Gpio pin numbers
        GPIOpinvalue DigitalRead(IPin pin);

        //Uses Gpio pin numbers
        void PullUpDnControl(IPin pin, int pud);

        //This pwm mode cannot be used when using GpioSys mode!!
        //Uses Gpio pin numbers
        void PWMWrite(IPin pin, int value);

        //Uses Gpio pin numbers
        void PWMSetMode(int mode);

        //Uses Gpio pin numbers
        void PWMSetRange(uint range);

        //Uses Gpio pin numbers
        void PWMSetClock(int divisor);

        //Uses Gpio pin numbers
        void ClockSetGpio(IPin pin, int freq);
    }
}