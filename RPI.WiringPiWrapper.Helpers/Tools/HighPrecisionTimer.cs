using RPI.WiringPiWrapper.Helpers.Interfaces;
using System;
using System.Diagnostics;

namespace RPI.WiringPiWrapper.Helpers.Tools
{
    public class HighPrecisionTimer: IHighPrecisionTimeMeasurement
    {
        private readonly Stopwatch _timer;
        private long _ticksPerMilisecond => TimeSpan.TicksPerMillisecond;
        private long _ticksPerMicrosecond => TimeSpan.TicksPerMillisecond / 1000;
        private double _cmPerMicrosecond => 0.034;

        public HighPrecisionTimer()
        {
            _timer = new Stopwatch();

            Console.WriteLine($"Precision check. Ticks per microsecond: {_ticksPerMicrosecond}");
        }

        /// <summary>
        /// Sleep microseconds. Returns elapsed time in us
        /// </summary>
        /// <param name="microseconds"></param>
        /// <returns></returns>
        public long SleepMicroseconds(long microseconds)
        {
            var ticksToWait = GetTicksAmount(microseconds);
            _timer.Reset();
            _timer.Start();

            while (_timer.ElapsedTicks < ticksToWait) { }

            _timer.Stop();
            return GetMicrosecondsFromTicks(_timer.ElapsedTicks);
        }

        public double ConvertTicksToDistance(long ticks)
        {
            var microseconds = GetMicrosecondsFromTicks(ticks);
            Console.WriteLine($"Calculating {ticks} ticks => {microseconds}[us]");

            // Calculating the distance
            var distance = microseconds * _cmPerMicrosecond / 2;

            return distance;
        }

        private long GetTicksAmount(long microseconds) => _ticksPerMicrosecond * microseconds;

        private long GetMicrosecondsFromTicks(long ticks) => ticks / _ticksPerMicrosecond;

        void IHighPrecisionTimeMeasurement.SleepMicroseconds(long microseconds)
        {
            throw new NotImplementedException();
        }
    }
}