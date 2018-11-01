using RPI.WiringPiWrapper.Devices.SonicSensor;
using RPI.WiringPiWrapper.Hardware.Pins;
using RPI.WiringPiWrapper.Hardware.Pins.Exceptions;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Timing;
using System;
using System.Diagnostics;
using System.Threading;
using NSubstitute;
using RPI.WiringPiWrapper.WiringPi;
using Xunit;

namespace RPI.WiringPiWrapper.Tests.Devices.SonicSensor
{
    public class UnitTests_Of_ConicSensorClass : Fixture_For_UnitTests
    {
        [Theory, Fixture_For_UnitTests]
        [Trait("Category", "Unit Tests")]
        public void When_Ctor_IsCalledWithProperParameters_Then_NoExceptionIsThrown(IWrapGPIO gpioWrapperStub,
                                                                                    IWrapTiming timingWrapperStub)
        {
            //a

            //aa, aaa
            new SonicSensorDriver(gpioWrapperStub, timingWrapperStub);
        }

        [Theory, Fixture_For_UnitTests]
        [Trait("Category", "Unit Tests")]
        public void When_Ctor_IsCalledWithWrongPinType_Then_ValidationExceptionIsThrown(IWrapGPIO gpioWrapperDummy,
                                                                                        IWrapTiming timingWrapperDummy,
                                                                                        IPin triggerPinDummy,
                                                                                        DOPin echoPinStub)
        {
            //a
            Action action = () => new SonicSensorDriver(echoPinStub, triggerPinDummy, gpioWrapperDummy, timingWrapperDummy);

            //aa

            //aaa
            Assert.Throws<PinStateValidationException>(action);
        }

        [Theory, Fixture_For_UnitTests]
        [Trait("Category", "Unit Tests")]
        public void When_Configure_Then_PinModesAreReSetted(IWrapGPIO gpioSpy,
                                                            SonicSensorDriver sut)
        {
            //a
            
            //aa
            sut.Configure();

            //aaa
            gpioSpy.Received(1).PinMode(sut.EchoPin);
            gpioSpy.Received(1).PinMode(sut.TriggerPin);
        }

        [Theory, Fixture_For_UnitTests]
        [Trait("Category", "Unit Tests")]
        public void When_Configure_Then_TriggerPin_IsSetToLow(IWrapGPIO gpioSpy,
            SonicSensorDriver sut)
        {
            //a
            var triggerPin = sut.TriggerPin;

            //aa
            sut.Configure();

            //aaa
            gpioSpy.Received(1).DigitalWrite(triggerPin, GPIO.GPIOpinvalue.Low);
        }

        [Theory, Fixture_For_UnitTests]
        [Trait("Category", "Unit Tests")]
        public void When_GetDistanceIsCalled_Then_DelaysAreCalled(IWrapGPIO gpioStub,
                                                                    IWrapTiming timingSpy,
                                                                    SonicSensorDriver sut)
        {
            //a
            var echoPin = sut.EchoPin;
            var measuringThread =  new Thread(() => sut.GetDistance());

            //aa
            measuringThread.Start();

            gpioStub.DigitalRead(echoPin).Returns(GPIO.GPIOpinvalue.High);
            Thread.Sleep(1);
            gpioStub.DigitalRead(echoPin).Returns(GPIO.GPIOpinvalue.Low);
           

            //aaa
            timingSpy.Received(1).DelayMicroseconds(5);
            timingSpy.Received(1).DelayMicroseconds(10);
        }

        [Theory, Fixture_For_UnitTests]
        [Trait("Category", "Unit Tests")]
        public void When_GetDistanceIsCalled_Then_DistanceIsReturned(IWrapGPIO gpioStub,
                                                                     SonicSensorDriver sut)
        {
            //a
            var echoPin = sut.EchoPin;
            double readedDistance = 0;
            double milisToSleep = 3;

            var measuringThread = new Thread(() =>
            {
                //Thread.Sleep(1000);
                //gpioStub.DigitalRead(echoPin).Returns(GPIO.GPIOpinvalue.Low);
                //Thread.Sleep(1000);
                gpioStub.DigitalRead(echoPin).Returns(GPIO.GPIOpinvalue.High);
                Thread.Sleep((int)milisToSleep);
                gpioStub.DigitalRead(echoPin).Returns(GPIO.GPIOpinvalue.Low);
            });

            //aa
            measuringThread.Start();
            readedDistance = sut.GetDistance();
            
            //aaa
            //var measuredTime = sut.elapsedTime.TotalMilliseconds;

            Assert.Equal(milisToSleep, measuredTime);
        }
    }
}