using System;
using Xunit;

namespace RPI.WiringPiWrapper.Tests
{
    public class TestIfProjectIsTestable
    {
        [Fact]
        public void ObviousTestToDetermineIfXunitIsWorking()
        {
            Assert.Equal(4, 4);
        }
    }
}
