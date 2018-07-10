namespace RPI.WiringPiWrapper.WiringPi.Wrappers.Timing
{
    public interface IWrapTiming
    {
        uint Millis();

        uint Micros();

        void Delay(uint howLong);

        void DelayMicroseconds(uint howLong);
    }
}