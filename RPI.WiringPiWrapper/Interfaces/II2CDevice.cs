namespace RPI.WiringPiWrapper.Interfaces
{
    public interface II2CDevice
    {
        void WriteCommand(int cmd);

        int Read();
    }
}