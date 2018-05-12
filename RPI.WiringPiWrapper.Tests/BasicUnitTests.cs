using System;
using Xunit;

namespace RPI.WiringPiWrapper.Tests
{
    public class BasicUnitTest
    {
        [Fact]
        public void CheckIfInstanceCanBeInitiated()
        {
            //a
            // Action creationAction = () => Init.WiringPiSetup();

            //aa

            //aaa
            try{
            Init.WiringPiSetup();

            }
            catch(Exception a)
            {

            }
        }

        [Fact]
        public void CheckIf2InstanceCanBeInitiated()
        {
            //a
            var testObject = new Init();

            //aa

            //aaa
            // testObject.
        }
    }
}
