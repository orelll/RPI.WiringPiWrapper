using rpi.wiringpiwrapper;
using RPI.WiringPiWrapper.Helpers;
using System;
using System.Threading;

namespace RPI.WiringPiWrapper
{
    public class I2CDevice
    {
        private int _addr;
        public int _deviceHandler;

        public I2CDevice(int addr)
        {
            _addr = addr;
            _deviceHandler = I2C.wiringPiI2CSetup(_addr);
            Console.WriteLine($"Obrained device handler: {_deviceHandler}");
        }

        //# Write a single command
        public void write_cmd(int cmd)
        {
            I2C.wiringPiI2CWrite(_deviceHandler, cmd);
            Thread.Sleep(1);
        }

        ////# Write a command and argument
        //public void write_cmd_arg(int cmd, int data)
        //{
        //    I2C.wiringPiI2CWriteReg16(_deviceHandler, cmd, data);
        //    Thread.Sleep(100);
        //}

        ////# Write a block of data
        //public void write_block_data(int cmd, int data)
        //{
        //    I2C.wiringPiI2CWriteReg16(_deviceHandler, cmd, data);
        //    Thread.Sleep(100);
        //}

        //# Read a single byte
        public int read()
        {
            return I2C.wiringPiI2CRead(_deviceHandler);
        }

        ////# Read
        //public int read_data(int cmd)
        //{
        //    return I2C.wiringPiI2CReadReg16(_deviceHandler, cmd);
        //}

        ////# Read a block of data
        //public int read_block_data(int cmd)
        //{
        //    return I2C.wiringPiI2CReadReg16(_deviceHandler, cmd);
        //}
    }
}