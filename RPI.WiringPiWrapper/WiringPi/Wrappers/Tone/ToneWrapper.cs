namespace RPI.WiringPiWrapper.WiringPi.Wrappers.Tone
{
    public class ToneWrapper : IWrapTone
    {
        public int SoftToneCreate(int pin)
        {
            return WiringPi.Tone.SoftToneCreate(pin);
        }

        public void SoftToneWrite(int pin, int freq)
        {
            WiringPi.Tone.SoftToneWrite(pin, freq);
        }
    }
}