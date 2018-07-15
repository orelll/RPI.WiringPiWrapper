namespace RPI.WiringPiWrapper.WiringPi.Wrappers.SPI
{
    public class SPIWrapper : IWrapSPI
    {
        public unsafe int WiringPiSPIDataRW(int channel, byte* data, int len)
        {
            return WiringPi.SPI.WiringPiSPIDataRW(channel, data, len);
        }

        public int WiringPiSPISetup(int channel, int speed)
        {
            return WiringPi.SPI.WiringPiSPISetup(channel, speed);
        }
    }
}