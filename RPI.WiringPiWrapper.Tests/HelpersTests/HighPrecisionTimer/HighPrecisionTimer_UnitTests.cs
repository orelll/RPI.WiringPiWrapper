using RPI.WiringPiWrapper.Helpers;
using System;
using Xunit;

namespace RPI.WiringPiWrapper.Tests.HelpersTests.HighPrecisionTimer
{
    public class HighPrecisionTimer_UnitTests
    {
        [Fact]
        public void When_Constructor_IsCalled_Then_NoExceptions_AreThrown()
        {
            //a
            var timer = new HighPrecisionTimer2();

            //aa
            //aaa
        }

        [Fact]
        public void When_Sleep_IsCalled_For_NusThen_Sleep_IsProperLong()
        {
            HiResTimer timer = new HiResTimer();

            // This example shows how to use the high-resolution counter to
            // time an operation.

            // Get counter value before the operation starts.
            Int64 counterAtStart = timer.Value;

            // Perform an operation that takes a measureable amount of time.
            for (int count = 0; count < 10000; count++)
            {
                count++;
                count--;
            }

            // Get counter value when the operation ends.
            Int64 counterAtEnd = timer.Value;

            // Get time elapsed in tenths of a millisecond.
            Int64 timeElapsedInTicks = counterAtEnd - counterAtStart;
            Int64 timeElapseInTenthsOfMilliseconds = (timeElapsedInTicks * 10000) / timer.Frequency;
        }

        [Fact]
        public void When_TicksToDistanceConverter_IsCalled_For_NTicks_Then_ReturnValue_IsProperLong()
        {
            //a
            var highPrecisionTimer = new HighPrecisionTimer2();
            var sleptTicks = new Random().Next(1, 100000);

            //aa
            var calculatedDistance = highPrecisionTimer.ConvertTicksToDistance(sleptTicks);

            //aaa
            var ticksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000;
            var calculatedBaseDistance = sleptTicks / ticksPerMicrosecond * 0.034 / 2;

            Assert.Equal(calculatedBaseDistance, calculatedDistance);
        }
    }
}