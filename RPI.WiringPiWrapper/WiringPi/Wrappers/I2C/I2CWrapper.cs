namespace RPI.WiringPiWrapper.WiringPi.Wrappers.I2C
{
    public class I2CWrapper : IWrapI2C
    {
        public int WiringPiI2CRead(int fd)
        {
            return WiringPi.I2C.WiringPiI2CRead(fd);
        }

        public int WiringPiI2CReadReg16(int fd, int reg)
        {
            return WiringPi.I2C.WiringPiI2CReadReg16(fd, reg);
        }

        public int WiringPiI2CReadReg8(int fd, int reg)
        {
            return WiringPi.I2C.WiringPiI2CReadReg8(fd, reg);
        }

        public int WiringPiI2CSetup(int devId)
        {
            return WiringPi.I2C.WiringPiI2CSetup(devId);
        }

        public int WiringPiI2CWrite(int fd, int data)
        {
            return WiringPi.I2C.WiringPiI2CWrite(fd, data);
        }

        public int WiringPiI2CWriteReg16(int fd, int reg, int data)
        {
            return WiringPi.I2C.WiringPiI2CWriteReg16(fd, reg, data);
        }

        public int WiringPiI2CWriteReg8(int fd, int reg, int data)
        {
            return WiringPi.I2C.WiringPiI2CWriteReg8(fd, reg, data);
        }
    }
}