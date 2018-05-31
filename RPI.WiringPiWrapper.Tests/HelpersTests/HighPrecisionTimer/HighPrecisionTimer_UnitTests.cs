using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
using tools = RPI.WiringPiWrapper.Helpers.Tools;

namespace RPI.WiringPiWrapper.Tests.HelpersTests.HighPrecisionTimer
{
    public class HighPrecisionTimer_UnitTests
    {
        [Fact]
        public void When_Constructor_IsCalled_Then_NoExceptions_AreThrown()
        {
            //a
            var timer = new tools.HighPrecisionTimer2();

            //aa
            //aaa
        }

        [Fact]
        public void When_Sleep_IsCalled_For_NusThen_Sleep_IsProperLong()
        {
            //a
            var highPrecisionTimer = new tools.HighPrecisionTimer2();
            var usToWait = new Random().Next(1, 150);
            
            //aa
            highPrecisionTimer.SleepMicroseconds(usToWait);
            
            //aaa
            Assert.Equal(usToWait, 0);
        }

        [Fact]
        public void When_TicksToDistanceConverter_IsCalled_For_NTicks_Then_ReturnValue_IsProperLong()
        {
            //a
            var highPrecisionTimer = new tools.HighPrecisionTimer2();
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
