using System.Runtime.InteropServices;

namespace RPI.WiringPiWrapper.Base.WiringPi
{
    /// <summary>
    /// Provides access to the I2C port
    /// </summary>
    public class I2C
    {
        [DllImport("libwiringPi.so", EntryPoint = "WiringPiI2CSetup")]
        public static extern int WiringPiI2CSetup(int devId);

        [DllImport("libwiringPi.so", EntryPoint = "WiringPiI2CRead")]
        public static extern int WiringPiI2CRead(int fd);

        [DllImport("libwiringPi.so", EntryPoint = "WiringPiI2CWrite")]
        public static extern int WiringPiI2CWrite(int fd, int data);

        [DllImport("libwiringPi.so", EntryPoint = "WiringPiI2CWriteReg8")]
        public static extern int WiringPiI2CWriteReg8(int fd, int reg, int data);

        [DllImport("libwiringPi.so", EntryPoint = "WiringPiI2CWriteReg16")]
        public static extern int WiringPiI2CWriteReg16(int fd, int reg, int data);

        [DllImport("libwiringPi.so", EntryPoint = "WiringPiI2CReadReg8")]
        public static extern int WiringPiI2CReadReg8(int fd, int reg);

        [DllImport("libwiringPi.so", EntryPoint = "WiringPiI2CReadReg16")]
        public static extern int WiringPiI2CReadReg16(int fd, int reg);
    }
}