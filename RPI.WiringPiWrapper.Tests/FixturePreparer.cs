using AutoFixture;
using AutoFixture.AutoMoq;

namespace RPI.WiringPiWrapper.Tests
{
    public static class FixturePreparer
    {
        public static Fixture GetParametrizedFixture => (Fixture)new Fixture().Customize(new AutoMoqCustomization());
    }
}