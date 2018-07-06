using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RPI.WiringPiWrapper.Base.WiringPi
{
    /// <summary>
    /// Provides use of the Timing functions such as delays
    /// </summary>
    public class Timing
    {
        [DllImport("libwiringPi.so", EntryPoint = "Millis")]
        public static extern uint Millis();

        [DllImport("libwiringPi.so", EntryPoint = "Micros")]
        public static extern uint Micros();

        [DllImport("libwiringPi.so", EntryPoint = "Delay")]
        public static extern void Delay(uint howLong);

        [DllImport("libwiringPi.so", EntryPoint = "DelayMicroseconds")]
        public static extern void DelayMicroseconds(uint howLong);
    }
}
