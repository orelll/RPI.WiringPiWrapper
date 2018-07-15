namespace RPI.WiringPiWrapper.WiringPi.Wrappers.MiscFunctions
{
    public interface IWrapMiscFunctions
    {
        int PiBoardRev();

        int WpiPinToGpio(int wPiPin);

        int PhysPinToGpio(int physPin);

        int SetPadDrive(int group, int value);
    }
}