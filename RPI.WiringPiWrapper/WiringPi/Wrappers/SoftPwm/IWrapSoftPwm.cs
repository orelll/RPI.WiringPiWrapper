namespace RPI.WiringPiWrapper.WiringPi.Wrappers.SoftPwm
{
    public interface IWrapSoftPwm
    {
        int Create(int pin, int initialValue, int pwmRange);

        void Write(int pin, int value);

        void Stop(int pin);
    }
}