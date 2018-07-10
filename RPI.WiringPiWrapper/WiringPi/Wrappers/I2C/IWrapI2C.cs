namespace RPI.WiringPiWrapper.WiringPi.Wrappers.I2C
{
    public interface IWrapI2C
    {
        int WiringPiI2CSetup(int devId);

        int WiringPiI2CRead(int fd);

        int WiringPiI2CWrite(int fd, int data);

        int WiringPiI2CWriteReg8(int fd, int reg, int data);

        int WiringPiI2CWriteReg16(int fd, int reg, int data);

        int WiringPiI2CReadReg8(int fd, int reg);

        int WiringPiI2CReadReg16(int fd, int reg);
    }
}