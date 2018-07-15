namespace RPI.WiringPiWrapper.WiringPi.Wrappers.SoftPwm
{
    public class SoftPwmWrapper : IWrapSoftPwm
    {
        public int Create(int pin, int initialValue, int pwmRange)
        {
            return WiringPi.SoftPwm.Create(pin, initialValue, pwmRange);
        }

        public void Stop(int pin)
        {
            WiringPi.SoftPwm.Stop(pin);
        }

        public void Write(int pin, int value)
        {
            WiringPi.SoftPwm.Write(pin, value);
        }
    }
}