using System.Runtime.InteropServices;

namespace RPI.WiringPiWrapper.Base.WiringPi
{
    /// <summary>
    /// Provides access to the Thread priority and interrupts for IO
    /// </summary>
    public class PiThreadInterrupts
    {
        [DllImport("libwiringPi.so", EntryPoint = "PiHiPri")]
        public static extern int PiHiPri(int priority);

        [DllImport("libwiringPi.so", EntryPoint = "WaitForInterrupt")]
        public static extern int WaitForInterrupt(int pin, int timeout);

        //This is the C# equivelant to "void (*function)(void))" required by wiringPi to define a callback method
        public delegate void ISRCallback();

        [DllImport("libwiringPi.so", EntryPoint = "WiringPiISR")]
        public static extern int WiringPiISR(int pin, int mode, ISRCallback method);

        public enum InterruptLevels
        {
            INT_EDGE_SETUP = 0,
            INT_EDGE_FALLING = 1,
            INT_EDGE_RISING = 2,
            INT_EDGE_BOTH = 3
        }

        //static extern int piThreadCreate(string name);
    }
}