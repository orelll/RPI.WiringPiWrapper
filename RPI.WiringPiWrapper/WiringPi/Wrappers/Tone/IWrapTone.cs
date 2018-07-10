namespace RPI.WiringPiWrapper.WiringPi.Wrappers.Tone
{
    public interface IWrapTone
    {
        int SoftToneCreate(int pin);

        void SoftToneWrite(int pin, int freq);
    }
}