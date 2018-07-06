using System.Runtime.InteropServices;

namespace RPI.WiringPiWrapper.Base.WiringPi
{
    /// <summary>
    /// Used to configure a GPIO pin's direction and provide read & write functions to a GPIO pin
    /// </summary>
    public class GPIO
    {
        [DllImport("libwiringPi.so", EntryPoint = "PinMode")]           //Uses Gpio pin numbers
        public static extern void PinMode(int pin, int mode);

        [DllImport("libwiringPi.so", EntryPoint = "DigitalWrite")]      //Uses Gpio pin numbers
        public static extern void DigitalWrite(int pin, int value);

        [DllImport("libwiringPi.so", EntryPoint = "DigitalWriteByte")]      //Uses Gpio pin numbers
        public static extern void DigitalWriteByte(int value);

        [DllImport("libwiringPi.so", EntryPoint = "DigitalRead")]           //Uses Gpio pin numbers
        public static extern int DigitalRead(int pin);

        [DllImport("libwiringPi.so", EntryPoint = "PullUpDnControl")]         //Uses Gpio pin numbers
        public static extern void PullUpDnControl(int pin, int pud);

        //This pwm mode cannot be used when using GpioSys mode!!
        [DllImport("libwiringPi.so", EntryPoint = "PWMWrite")]              //Uses Gpio pin numbers
        public static extern void PWMWrite(int pin, int value);

        [DllImport("libwiringPi.so", EntryPoint = "PWMSetMode")]             //Uses Gpio pin numbers
        public static extern void PWMSetMode(int mode);

        [DllImport("libwiringPi.so", EntryPoint = "PWMSetRange")]             //Uses Gpio pin numbers
        public static extern void PWMSetRange(uint range);

        [DllImport("libwiringPi.so", EntryPoint = "PWMSetClock")]             //Uses Gpio pin numbers
        public static extern void PWMSetClock(int divisor);

        [DllImport("libwiringPi.so", EntryPoint = "gpioClockSet")]              //Uses Gpio pin numbers
        public static extern void ClockSetGpio(int pin, int freq);

        public enum GPIOpinmode
        {
            Input = 0,
            Output = 1,
            PWMOutput = 2,
            GPIOClock = 3,
            SoftPWMOutput = 4,
            SoftToneOutput = 5,
            PWMToneOutput = 6
        }

        public enum GPIOpinvalue
        {
            High = 1,
            Low = 0
        }

        public enum PullUpDnValue
        {
            Off = 0,
            Down = 1,
            Up = 2
        }
    }
}