using AutoFixture;
using RPI.WiringPiWrapper.Devices.SonicSensor;
using RPI.WiringPiWrapper.Hardware.Pins;
using RPI.WiringPiWrapper.Hardware.Pins.Exceptions;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Timing;
using System;
using Xunit;

namespace RPI.WiringPiWrapper.Tests.Devices.SonicSensor
{
    public class UnitTests_Of_ConicSensorClass
    {
        [Fact]
        [Trait("Category", "Unit Tests")]
        public void When_Ctor_IsCalledWithProperParameters_Then_NoExceptionIsThrown()
        {
            //a
            var fixture = FixturePreparer.GetParametrizedFixture;
            var gpioWrapperStub = fixture.Create<IWrapGPIO>();
            var timingWrapperStub = fixture.Create<IWrapTiming>();

            //aa, aaa
            new SonicSensorDriver(gpioWrapperStub, timingWrapperStub);
        }

        [Fact]
        [Trait("Category", "Unit Tests")]
        public void When_Ctor_IsCalledWithWrongPinType_Then_ValidationExceptionIsThrown()
        {
            //a
            var fixture = FixturePreparer.GetParametrizedFixture;
            var gpioWrapperStub = fixture.Create<IWrapGPIO>();
            var timingWrapperStub = fixture.Create<IWrapTiming>();
            var echoPinStub = fixture.Create<DIPin>();
            var triggerPinStub = fixture.Create<DIPin>();

            //aa
            Action action = () => new SonicSensorDriver(echoPinStub, triggerPinStub, gpioWrapperStub, timingWrapperStub);

            //aaa
            Assert.Throws<PinStateValidationException>(action);
        }
    }
}