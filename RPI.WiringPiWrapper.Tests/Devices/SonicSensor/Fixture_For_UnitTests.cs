using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using RPI.WiringPiWrapper.Devices.SonicSensor;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Timing;

namespace RPI.WiringPiWrapper.Tests.Devices.SonicSensor
{
    public class Fixture_For_UnitTests : AutoDataAttribute
    {
        public Fixture_For_UnitTests() : base(() => new Fixture().Customize(new AutoNSubstituteCustomization { ConfigureMembers = true }))
        {
            Fixture.Customize<SonicSensorDriver>(c => c.FromFactory(
                new MethodInvoker(
                    new ModestConstructorQuery())));

            Fixture.Freeze<IWrapGPIO>();
            Fixture.Freeze<IWrapTiming>();

            //Fixture.Customize<SonicSensorDriver>(c => c.FromFactory(
            //    new MethodInvoker(
            //        new GreedyConstructorQuery())));
        }
    }
}