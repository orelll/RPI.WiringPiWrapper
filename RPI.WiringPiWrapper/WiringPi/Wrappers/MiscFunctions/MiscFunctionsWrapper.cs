namespace RPI.WiringPiWrapper.WiringPi.Wrappers.MiscFunctions
{
    public class MiscFunctionsWrapper : IWrapMiscFunctions
    {
        public int PhysPinToGpio(int physPin)
        {
            return WiringPi.MiscFunctions.PhysPinToGpio(physPin);
        }

        public int PiBoardRev()
        {
            return WiringPi.MiscFunctions.PiBoardRev();
        }

        public int SetPadDrive(int group, int value)
        {
            return WiringPi.MiscFunctions.SetPadDrive(group, value);
        }

        public int WpiPinToGpio(int wPiPin)
        {
            return WiringPi.MiscFunctions.WpiPinToGpio(wPiPin);
        }
    }
}