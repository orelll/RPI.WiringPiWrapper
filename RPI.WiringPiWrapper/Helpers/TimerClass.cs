using RPI.WiringPiWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace RPI.WiringPiWrapper.Helpers
{
    public class TimerClass : ITimer
    {
        private static ManualResetEvent _manualResetEvent;
        private static Stopwatch _stopWatch;

        public TimerClass()
        {
            _manualResetEvent = new ManualResetEvent(false);
            _stopWatch = new Stopwatch();
        }

        public void SleepMicroseconds(int microsecondsToSleep)
        {
            _manualResetEvent.WaitOne(
                  TimeSpan.FromMilliseconds((double)microsecondsToSleep / 1000d));
        }

        public void SleepMiliseconds(int milisecondsToSleep)
        {
            _manualResetEvent.WaitOne(
                 TimeSpan.FromMilliseconds((double)milisecondsToSleep));
        }

        public void SleepSeconds(int secondsToSleep)
        {
            _manualResetEvent.WaitOne(
                 TimeSpan.FromSeconds((double)secondsToSleep));
        }

        public double GetMilisecondsUntilNextEdge(IPin pin, GPIO.GPIOpinvalue stateToWaitFor)
        {
            _stopWatch.Reset();

            while (pin.Value != stateToWaitFor) { };

            _stopWatch.Start();

            while (pin.Value == stateToWaitFor) { };

            _stopWatch.Stop();

            return _stopWatch.Elapsed.TotalMilliseconds;
        }
    }
}
