namespace RPI.WiringPiWrapper.WiringPi.Wrappers.SPI
{
    public interface IWrapSPI
    {
        int WiringPiSPISetup(int channel, int speed);

        unsafe int WiringPiSPIDataRW(int channel, byte* data, int len);
    }
}