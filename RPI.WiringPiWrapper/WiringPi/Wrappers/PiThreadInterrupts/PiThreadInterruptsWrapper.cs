namespace RPI.WiringPiWrapper.WiringPi.Wrappers.PiThreadInterrupts
{
    public class PiThreadInterruptsWrapper : IWrapPiThreadInterrupts
    {
        public int PiHiPri(int priority)
        {
            return WiringPi.PiThreadInterrupts.PiHiPri(priority);
        }

        public int WaitForInterrupt(int pin, int timeout)
        {
            return WiringPi.PiThreadInterrupts.WaitForInterrupt(pin, timeout);
        }

        public int WiringPiISR(int pin, int mode, WiringPi.PiThreadInterrupts.ISRCallback method)
        {
            return WiringPi.PiThreadInterrupts.WiringPiISR(pin, mode, method);
        }
    }
}