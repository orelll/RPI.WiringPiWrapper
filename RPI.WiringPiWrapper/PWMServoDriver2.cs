using rpi.wiringpiwrapper;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RPI.WiringPiWrapper
{
    public class PWMServoDriver2
    {
        /*************************************************************************
 * pca9685.c
 *
 * This software is a devLib extension to wiringPi <http://wiringpi.com/>
 * and enables it to control the Adafruit PCA9685 16-Channel 12-bit
 * PWM/Servo Driver <http://www.adafruit.com/products/815> via I2C interface.
 *
 * Copyright (c) 2014 Reinhard Sprung
 *
 * If you have questions or improvements email me at
 * reinhard.sprung[at]gmail.com
 *
 * This software is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * The given code is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You can view the contents of the licence at <http://www.gnu.org/licenses/>.
 **************************************************************************
 */

        // Define first LED and all LED. We calculate the rest
        private int LEDALL_ON_L = 0xFA;

        private int PIN_ALL = 16;

        private const byte PCA9685_MODE1 = 0x0;
        private const byte PCA9685_PRESCALE = 0xFE;
        private const byte SUBADR1 = 0x02;
        private const byte SUBADR2 = 0x03;
        private const byte SUBADR3 = 0x04;
        private const byte MODE1 = 0x00;
        private const byte PRESCALE = 0xFE;
        private const byte LED0_ON_L = 0x06;
        private const byte LED0_ON_H = 0x07;
        private const byte LED0_OFF_L = 0x08;
        private const byte LED0_OFF_H = 0x09;
        private const byte ALLLED_ON_L = 0xFA;
        private const byte ALLLED_ON_H = 0xFB;
        private const byte ALLLED_OFF_L = 0xFC;
        private const byte ALLLED_OFF_H = 0xFD;

        //// Declare
        //static void myPwmWrite(struct wiringPiNodeStruct *node, int pin, int value);
        //static void myOnOffWrite(struct wiringPiNodeStruct *node, int pin, int value);
        //static int myOffRead(struct wiringPiNodeStruct *node, int pin);
        //static int myOnRead(struct wiringPiNodeStruct *node, int pin);
        //int baseReg(int pin);

        /**
         * Setup a PCA9685 device with wiringPi.
         *
         * pinBase: 	Use a pinBase > 64, eg. 300
         * i2cAddress:	The default address is 0x40
         * freq:		Frequency will be capped to range [40..1000] Hertz. Try 50 for servos
         */
        wiringPiNodeStruct node;
        int fd;

        public int pca9685Setup(int pinBase, int i2cAddress, float freq)
        {
            // Create a node with 16 pins [0..15] + [16] for all
            node = new wiringPiNodeStruct(pinBase, PIN_ALL + 1);//wiringPiNewNode(pinBase, PIN_ALL + 1);

            // Check i2c address
            fd = I2C.wiringPiI2CSetup(i2cAddress);
            if (fd < 0)
                return fd;

            // Setup the chip. Enable auto-increment of registers.
            int settings = I2C.wiringPiI2CReadReg8(fd, PCA9685_MODE1) & 0x7F;
            int autoInc = settings | 0x20;

            I2C.wiringPiI2CWriteReg8(fd, PCA9685_MODE1, autoInc);

            // Set frequency of PWM signals. Also ends sleep mode and starts PWM output.
            if (freq > 0)
                pca9685PWMFreq(freq);

            node.fd = fd;
            node.pwmWrite = () => myPwmWrite(0, 180);
            node.digitalWrite = () => myOnOffWrite(0, 180);
            node.digitalRead = () => myOffRead(0);
            node.analogRead = () => myOnRead(0);

            return fd;
        }

        /**
         * Sets the frequency of PWM signals.
         * Frequency will be capped to range [40..1000] Hertz. Try 50 for servos.
         */

        public void pca9685PWMFreq(float freq)
        {
            // Cap at min and max
            freq = (freq > 1000 ? 1000 : (freq < 40 ? 40 : freq));

            // To set pwm frequency we have to set the prescale register. The formula is:
            // prescale = round(osc_clock / (4096 * frequency))) - 1 where osc_clock = 25 MHz
            // Further info here: http://www.nxp.com/documents/data_sheet/PCA9685.pdf Page 24
            int prescale = (int)(25000000.0f / (4096 * freq) - 0.5f);

            // Get settings and calc bytes for the different states.
            int settings = I2C.wiringPiI2CReadReg8(fd, PCA9685_MODE1) & 0x7F;   // Set restart bit to 0
            int sleep = settings | 0x10;                                    // Set sleep bit to 1
            int wake = settings & 0xEF;                                 // Set sleep bit to 0
            int restart = wake | 0x80;                                      // Set restart bit to 1

            // Go to sleep, set prescale and wake up again.
            I2C.wiringPiI2CWriteReg8(fd, PCA9685_MODE1, sleep);
            I2C.wiringPiI2CWriteReg8(fd, PCA9685_PRESCALE, prescale);
            I2C.wiringPiI2CWriteReg8(fd, PCA9685_MODE1, wake);

            // Now wait a millisecond until oscillator finished stabilizing and restart PWM.
            Thread.Sleep(1);
            I2C.wiringPiI2CWriteReg8(fd, PCA9685_MODE1, restart);
        }

        /**
         * Set all leds back to default values (: fullOff = 1)
         */

        public void pca9685PWMReset()
        {
            I2C.wiringPiI2CWriteReg16(fd, LEDALL_ON_L, 0x0);
            I2C.wiringPiI2CWriteReg16(fd, LEDALL_ON_L + 2, 0x1000);
        }

        /**
         * Write on and off ticks manually to a pin
         * (Deactivates any full-on and full-off)
         */

        public void pca9685PWMWrite(int pin, int on, int off)
        {
            int reg = baseReg(pin);

            // Write to on and off registers and mask the 12 lowest bits of data to overwrite full-on and off
            Console.WriteLine($"{GetMethodName()}: { I2C.wiringPiI2CWriteReg16(fd, reg, on & 0x0FFF)}");
            Console.WriteLine($"{GetMethodName()}: { I2C.wiringPiI2CWriteReg16(fd, reg + 2, off & 0x0FFF)}");
        }

        /**
         * Reads both on and off registers as 16 bit of data
         * To get PWM: mask each value with 0xFFF
         * To get full-on or off bit: mask with 0x1000
         * Note: ALL_LED pin will always return 0
         */

        public void pca9685PWMRead(int pin, ref int on, ref int off)
        {
            int reg = baseReg(pin);

            if (on == 1)
                on = I2C.wiringPiI2CReadReg16(fd, reg);
            if (off == 1)
                off = I2C.wiringPiI2CReadReg16(fd, reg + 2);
        }

        /**
         * Enables or deactivates full-on
         * tf = true: full-on
         * tf = false: according to PWM
         */

        public void pca9685FullOn(int pin, int tf)
        {
            int reg = baseReg(pin) + 1;     // LEDX_ON_H
            int state = I2C.wiringPiI2CReadReg8(fd, reg);

            // Set bit 4 to 1 or 0 accordingly
            state = tf == 1 ? (state | 0x10) : (state & 0xEF);

            Console.WriteLine($"{GetMethodName()}: { I2C.wiringPiI2CWriteReg8(fd, reg, state)}");

            // For simplicity, we set full-off to 0 because it has priority over full-on
            if (tf == 1)
                pca9685FullOff(pin, 0);
        }

        /**
         * Enables or deactivates full-off
         * tf = true: full-off
         * tf = false: according to PWM or full-on
         */

        public void pca9685FullOff(int pin, int tf)
        {
            int reg = baseReg(pin) + 3;     // LEDX_OFF_H
            int state = I2C.wiringPiI2CReadReg8(fd, reg);

            // Set bit 4 to 1 or 0 accordingly
            state = tf == 1 ? (state | 0x10) : (state & 0xEF);

            I2C.wiringPiI2CWriteReg8(fd, reg, state);
        }

        /**
         * Helper function to get to register
         */

        public int baseReg(int pin)
        {
            return (pin >= PIN_ALL ? LEDALL_ON_L : LED0_ON_L + 4 * pin);
        }

        //------------------------------------------------------------------------------------------------------------------
        //
        //	WiringPi functions
        //
        //------------------------------------------------------------------------------------------------------------------

        /**
         * Simple PWM control which sets on-tick to 0 and off-tick to value.
         * If value is <= 0, full-off will be enabled
         * If value is >= 4096, full-on will be enabled
         * Every value in between enables PWM output
         */

        public void myPwmWrite(int pin, int value)
        {
            int fd = node.fd;
            int ipin = pin - node.pinBase;

            if (value >= 4096)
                pca9685FullOn(ipin, 1);
            else if (value > 0)
                pca9685PWMWrite(ipin, 0, value);    // (Deactivates full-on and off by itself)
            else
                pca9685FullOff(ipin, 1);
        }

        /**
         * Simple full-on and full-off control
         * If value is 0, full-off will be enabled
         * If value is not 0, full-on will be enabled
         */

        public void myOnOffWrite(int pin, int value)
        {
            int fd = node.fd;
            int ipin = pin - node.pinBase;

            if (value != 0)
                pca9685FullOn(ipin, 1);
            else
                pca9685FullOff(ipin, 1);
        }

        /**
         * Reads off registers as 16 bit of data
         * To get PWM: mask with 0xFFF
         * To get full-off bit: mask with 0x1000
         * Note: ALL_LED pin will always return 0
         */

        public int myOffRead(int pin)
        {
            int fd = node.fd;
            int ipin = pin - node.pinBase;

            int off = 1;
            int on = 0;
            pca9685PWMRead(ipin, ref on, ref off);

            return off;
        }

        /**
         * Reads on registers as 16 bit of data
         * To get PWM: mask with 0xFFF
         * To get full-on bit: mask with 0x1000
         * Note: ALL_LED pin will always return 0
         */

        public int myOnRead(int pin)
        {
            int fd = node.fd;
            int ipin = pin - node.pinBase;

            int on = 1;
            int off = 0;
            pca9685PWMRead(ipin, ref on, ref off);

            return on;
        }

        private string GetMethodName([CallerMemberName] string caller = "")
        {
            return caller;
        }
    }
}