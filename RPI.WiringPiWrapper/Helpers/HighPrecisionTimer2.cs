using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using System;
using System.Diagnostics;
using System.Threading;

namespace RPI.WiringPiWrapper.Helpers
{
    public class HighPrecisionTimer2
    {
        private static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        private readonly IWrapGPIO _gpioWrapper;

        public HighPrecisionTimer2(IWrapGPIO gpioWrapper)
        {
            _gpioWrapper = gpioWrapper ?? throw new ArgumentNullException(nameof(gpioWrapper));
        }

        public double ConvertTicksToDistance(long ticks)
        {
            throw new NotImplementedException();
        }

        private static Stopwatch stopWatch = new Stopwatch();

        public static double GetTimeUntilNextEdge(int pin, int stateToWaitFor)
        {
            throw new NotImplementedException();
            //stopWatch.Reset();

            //while (_gpioWrapper.DigitalRead(pin) != stateToWaitFor) { };

            //stopWatch.Start();

            //while (_gpioWrapper.DigitalRead(pin) == stateToWaitFor) { };

            //stopWatch.Stop();

            //return stopWatch.Elapsed.TotalSeconds;
        }

        public void SleepMicroseconds(int delayMicroseconds)
        {
            manualResetEvent.WaitOne(
                TimeSpan.FromMilliseconds((double)delayMicroseconds / 1000d));
        }
    }
}