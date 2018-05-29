using rpi.wiringpiwrapper;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RPI.WiringPiWrapper
{
    public class PWMServoDriver
    {
        private int _i2caddr;
        private int _deviceHandler;
        private bool _debugMode;

        // Setup registers
        private int PCA9685_MODE1 = 0x0;
        private int PCA9685_PRESCALE = 0xFE;

        // Define first LED and all LED. We calculate the rest
        private int LED0_ON_L = 0x6;
        private int LEDALL_ON_L = 0xFA;

        private int PIN_ALL = 16;

        public PWMServoDriver(int addr, bool debbugMode = true)
        {
            _i2caddr = addr;
            _debugMode = debbugMode;
        }

        /// <summary>
        /// Setups the I2C interface and hardware
        /// </summary>
        /// <param name=""></param>
        public void begin()
        {
            var _deviceHandler = I2C.wiringPiI2CSetup(_i2caddr);// _i2c->begin();
            Console.WriteLine($"Obtained device handler: {_deviceHandler}");
            reset();
            setPWMFreq(1000);
        }

        /// <summary>
        /// Sends a reset command to the PCA9685 chip over I2C
        /// </summary>
        /// <param name=""></param>
        public void reset()
        {
            Console.WriteLine("Resetting device");
            write8(PCA9685_MODE1, 0x80);
            Thread.Sleep(1);
        }

        /// <summary>
        /// @brief  Sets the PWM frequency for the entire chip, up to ~1.6 KHz
        /// @param freq Floating point frequency that we will attempt to match
        /// </summary>
        /// <param name="freq"></param>
        public void setPWMFreq(float freq)
        {
            if (_debugMode)
            {
                Console.WriteLine($"Attempting to set freq {freq}");
            }

            freq *= 0.9F;  // Correct for overshoot in the frequency setting (see issue #11).
            float prescaleval = 25000000;
            prescaleval /= 4096;
            prescaleval /= freq;
            prescaleval -= 1;

            if (_debugMode)
            {
                Console.WriteLine($"Estimated pre-scale:  { prescaleval }");
            }

            int prescale = (int)Math.Floor(prescaleval + 0.5);

            if (_debugMode)
            {
                Console.WriteLine($"Final pre-scale: { prescale }");
            }

            int oldmode = read8(PCA9685_MODE1);
            int newmode = (oldmode & 0x7F) | 0x10; // sleep
            write8(PCA9685_MODE1, newmode); // go to sleep
            write8(PCA9685_PRESCALE, prescale); // set the prescaler
            write8(PCA9685_MODE1, oldmode);
            Thread.Sleep(5);
            write8(PCA9685_MODE1, oldmode | 0xa0);  //  This sets the MODE1 register to turn on auto increment.

            if (_debugMode)
            {
                Console.WriteLine($"Mode now 0x{ I2C.wiringPiI2CRead(PCA9685_MODE1).ToString("X")}");
            }
        }

        /// <summary>
        /// @brief  Sets the PWM output of one of the PCA9685 pins
        /// @param num One of the PWM output pins, from 0 to 15
        /// @param on At what point in the 4096-part cycle to turn the PWM output ON
        /// @param off At what point in the 4096-part cycle to turn the PWM output OFF
        /// </summary>
        /// <param name="num"></param>
        /// <param name="on"></param>
        /// <param name="off"></param>
        public void setPWM(int num, int on, int off)
        {
            if (_debugMode)
            {
                Console.WriteLine($"Setting PWM { num }: { on } -> { off }");
            }

            Console.WriteLine($"Response:  {I2C.wiringPiI2CWrite(_deviceHandler ,LED0_ON_L + 4 * num)}");
            Console.WriteLine($"Response:  {I2C.wiringPiI2CWrite(_deviceHandler, on)}");
            Console.WriteLine($"Response:  {I2C.wiringPiI2CWrite(_deviceHandler ,on >> 8)}");
            Console.WriteLine($"Response:  {I2C.wiringPiI2CWrite(_deviceHandler ,off)}");
            Console.WriteLine($"Response:  {I2C.wiringPiI2CWrite(_deviceHandler ,off >> 8)}");
        }

        /// <summary>
        /// @brief  Helper to set pin PWM output. Sets pin without having to deal with on/off tick placement and properly handles a zero value as completely off and 4095 as completely on.  Optional invert parameter supports inverting the pulse for sinking to ground.
        /// @param num One of the PWM output pins, from 0 to 15
        /// @param val The number of ticks out of 4096 to be active, should be a value from 0 to 4095 inclusive.
        /// @param invert If true, inverts the output, defaults to 'false'
        /// </summary>
        /// <param name="num"></param>
        /// <param name="val"></param>
        /// <param name="invert"></param>
        public void setPin(int num, int val, bool invert)
        {
            // Clamp value between 0 and 4095 inclusive.
            val = Math.Min(val, (int)4095);
            if (invert)
            {
                if (val == 0)
                {
                    // Special value for signal fully on.
                    setPWM(num, 4096, 0);
                }
                else if (val == 4095)
                {
                    // Special value for signal fully off.
                    setPWM(num, 0, 4096);
                }
                else
                {
                    setPWM(num, 0, 4095 - val);
                }
            }
            else
            {
                if (val == 4095)
                {
                    // Special value for signal fully on.
                    setPWM(num, 4096, 0);
                }
                else if (val == 0)
                {
                    // Special value for signal fully off.
                    setPWM(num, 0, 4096);
                }
                else
                {
                    setPWM(num, 0, val);
                }
            }
        }

        public int read8(int addr)
        {
            I2C.wiringPiI2CWrite(_deviceHandler, addr);

            var response = I2C.wiringPiI2CReadReg8(_deviceHandler, (int)1);
            Console.WriteLine($"{GetMethodName()}: {response}");

            return response;
        }

        public void write8(int addr, int d)
        {
            Console.WriteLine($"{GetMethodName()} (add: {addr}, d: {d}): { I2C.wiringPiI2CWriteReg8(_deviceHandler, addr, d)}");
        }

        private string GetMethodName([CallerMemberName]string caller= "")
        {
            return caller;
        }
    }
}