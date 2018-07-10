namespace RPI.WiringPiWrapper.WiringPi.Wrappers.Timing
{
    public class TimingWrapper : IWrapTiming
    {
        public void Delay(uint howLong)
        {
            WiringPi.Timing.Delay(howLong);
        }

        public void DelayMicroseconds(uint howLong)
        {
            WiringPi.Timing.DelayMicroseconds(howLong);
        }

        public uint Micros()
        {
            return WiringPi.Timing.Micros();
        }

        public uint Millis()
        {
            return WiringPi.Timing.Millis();
        }
    }
}