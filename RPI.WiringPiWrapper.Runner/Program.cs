using RPI.WiringPiWrapper.ConsoleRunner.IoC;
using RPI.WiringPiWrapper.Devices.LCD_Display;
using RPI.WiringPiWrapper.Devices.ServoDriver;
using RPI.WiringPiWrapper.Devices.SonicSensor;
using RPI.WiringPiWrapper.Hardware.GPIOBoard;
using RPI.WiringPiWrapper.Hardware.Pins;
using RPI.WiringPiWrapper.Helpers;
using RPI.WiringPiWrapper.Helpers.Loggers;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.Runner;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Init;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Timing;
using System;
using System.Threading;
using NLog.Config;
using RPI.WiringPiWrapper.WiringPi.Wrappers.I2C;

namespace RPI.WiringPiWrapper.ConsoleRunner
{
    internal class Program
    {
        private static GPIOBoard _gpioClass;
        private static TypesResolver _typesResolver;

        private static void Main(string[] args)
        {
            _typesResolver = new TypesResolver();

            var logger = _typesResolver.ResolveType<ILogger>();

            logger.WriteMessage("Initializing GPIO...");
            

            try
            {
                var initWrapper = _typesResolver.ResolveType<IWrapInit>();
                _gpioClass = new GPIOBoard(logger, initWrapper);

                var gpioWrapper = _typesResolver.ResolveType<IWrapGPIO>();
                RideTheHood(logger, gpioWrapper);
            }
            catch (Exception a)
            {
                logger.WriteMessage(a);
            }

            Console.WriteLine("All job is done");
            Console.ReadKey();
        }

        private static void RideTheHood(ILogger logger, IWrapGPIO gpioWrapper)
        {
            var lFNumber = 1;
            var lBNumber = 1;
            var rFNumber = 1;
            var rBNumber = 1;

            IPin lF = new DOPin(lFNumber);
            IPin lB = new DOPin(lBNumber);
            IPin rF = new DOPin(rFNumber);
            IPin rB = new DOPin(rBNumber);


            var rider = new Rider(gpioWrapper, lF, rF, lB, rB, logger);

            rider.MoveAhead();
            Thread.Sleep(2000);
            rider.MoveBack();
            Thread.Sleep(2000);
            rider.TurnLeft();
            Thread.Sleep(2000);
            rider.TurnRight();
            Thread.Sleep(2000);
            rider.TurnLeftInPlace();
            Thread.Sleep(2000);
            rider.TurnRightInPlace();
            Thread.Sleep(2000);

            rider.Stop();
        }

        private static LcdDisplay GetLCDDisplay(ITimer timer, ILogger logger) =>  new LcdDisplay(timer, logger);

        private static void DoSonicMeasuring(IWrapGPIO gpioWrapper, IWrapTiming timingWrapper, int delay)
        {
            var sonicSensorDriver = new SonicSensorDriver(gpioWrapper, timingWrapper);
            sonicSensorDriver.Configure();

            do
            {
                sonicSensorDriver.GetDistance();
                Thread.Sleep(delay);
            }
            while (true);
        }

        private static void DoMessaging(LcdDisplay lcdDisplay)
        {
            var readedLine = string.Empty;

            do
            {
                lcdDisplay.Clear();
                lcdDisplay.DisplayString(readedLine);

                Console.WriteLine("Type any text:");
                readedLine = Console.ReadLine();
            }
            while (!string.IsNullOrEmpty(readedLine));
        }

        private static void DoPWM(IWrapI2C i2cWrapper, ILogger logger, ITimer timer, int address)
        {
            var driverHandler = new ServoDriver(i2cWrapper, address, logger, timer);
            driverHandler.Configure();
            driverHandler.Reset();
            Console.WriteLine($"Obtained hanlder: {driverHandler}");
            Console.ReadKey();

            string readedValue;
            while (true)
            {
                Console.WriteLine("Give me value:");
                readedValue = Console.ReadLine();

                if (string.IsNullOrEmpty(readedValue))
                {
                    break;
                }

                driverHandler.SetPWM(1, int.Parse(readedValue));
            }
        }

        private void DoDisco()
        {
          
        }

        private static int GetDelayTime(int iteration)
        {
            var sin = Math.Sin(iteration);
            sin = sin + 1;

            var delay = Convert.ToInt32(50 * sin);

            return delay;
        }
    }
}