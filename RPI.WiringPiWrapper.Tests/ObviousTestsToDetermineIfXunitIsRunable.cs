using System;
using Xunit;

namespace RPI.WiringPiWrapper.Tests
{
    public class ObviousTestsToDetermineIfXunitIsRunable
    {
        [Fact]
        public void AlwaysGreenTest()
        {
            Assert.Equal(1, 1);
        }

         [Fact]
        public void AlwaysRedTest()
        {
            Assert.Equal(1, 2);
        }
    }
}
