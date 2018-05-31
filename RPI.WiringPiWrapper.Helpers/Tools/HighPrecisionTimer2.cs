using RPI.WiringPiWrapper.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace RPI.WiringPiWrapper.Helpers.Tools
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

            while (GPIO.digitalRead(pin) != stateToWaitFor) { };

            stopWatch.Start();

            while (GPIO.digitalRead(pin) == stateToWaitFor) { };

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
