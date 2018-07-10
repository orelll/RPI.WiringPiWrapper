using System;
using System.Diagnostics;
using System.Threading;
using RPI.WiringPiWrapper.WiringPi;

namespace RPI.WiringPiWrapper.Helpers
{
    public class HighPrecisionTimer2
    {
        private static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        public double ConvertTicksToDistance(long ticks)
        {
            throw new NotImplementedException();
        }

        private static Stopwatch stopWatch = new Stopwatch();

        public static double GetTimeUntilNextEdge(int pin, int stateToWaitFor)
        {
            stopWatch.Reset();

            while (GPIO.DigitalRead(pin) != stateToWaitFor) { };

            stopWatch.Start();

            while (GPIO.DigitalRead(pin) == stateToWaitFor) { };

            stopWatch.Stop();

            return stopWatch.Elapsed.TotalSeconds;
        }

        public void SleepMicroseconds(int delayMicroseconds)
        {
            manualResetEvent.WaitOne(
                TimeSpan.FromMilliseconds((double)delayMicroseconds / 1000d));
        }
    }
}