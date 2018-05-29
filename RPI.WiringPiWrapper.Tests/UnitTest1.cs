using System;
using Xunit;

namespace RPI.WiringPiWrapper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //a
            var ticksPerMilisecond = TimeSpan.TicksPerMillisecond;
            var ticksPerMicroSecond = ticksPerMilisecond / 1000;
        }
    }
}
