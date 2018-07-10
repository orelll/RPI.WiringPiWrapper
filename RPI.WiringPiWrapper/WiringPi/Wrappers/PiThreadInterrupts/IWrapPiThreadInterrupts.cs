namespace RPI.WiringPiWrapper.WiringPi.Wrappers.PiThreadInterrupts
{
    public interface IWrapPiThreadInterrupts
    {
        int PiHiPri(int priority);

        int WaitForInterrupt(int pin, int timeout);

        int WiringPiISR(int pin, int mode, WiringPi.PiThreadInterrupts.ISRCallback method);
    }
}