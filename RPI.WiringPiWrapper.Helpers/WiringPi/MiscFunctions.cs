using System.Runtime.InteropServices;

namespace RPI.WiringPiWrapper.Base.WiringPi
{
    public class MiscFunctions
    {
        [DllImport("libwiringPi.so", EntryPoint = "PiBoardRev")]
        public static extern int PiBoardRev();

        [DllImport("libwiringPi.so", EntryPoint = "WpiPinToGpio")]
        public static extern int WpiPinToGpio(int wPiPin);

        [DllImport("libwiringPi.so", EntryPoint = "PhysPinToGpio")]
        public static extern int PhysPinToGpio(int physPin);

        [DllImport("libwiringPi.so", EntryPoint = "SetPadDrive")]
        public static extern int SetPadDrive(int group, int value);
    }
}