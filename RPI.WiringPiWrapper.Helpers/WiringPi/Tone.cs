using System.Runtime.InteropServices;

namespace RPI.WiringPiWrapper.Base.WiringPi
{
    /// <summary>
    ///  Provides the ability to use the Software Tone functions in WiringPi
    /// </summary>
    public class Tone
    {
        [DllImport("libwiringPi.so", EntryPoint = "SoftToneCreate")]
        public static extern int SoftToneCreate(int pin);

        [DllImport("libwiringPi.so", EntryPoint = "SoftToneWrite")]
        public static extern void SoftToneWrite(int pin, int freq);
    }
}