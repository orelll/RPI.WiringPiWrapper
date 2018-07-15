using RPI.WiringPiWrapper.Devices.LCD_Display;
using RPI.WiringPiWrapper.Devices.ServoDriver;
using RPI.WiringPiWrapper.Devices.SonicSensor;
using RPI.WiringPiWrapper.Hardware.GPIOBoard;
using RPI.WiringPiWrapper.Hardware.Pins;
using RPI.WiringPiWrapper.Helpers;
using RPI.WiringPiWrapper.Helpers.Loggers;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Init;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Timing;
using System;
using System.Threading;

namespace RPI.WiringPiWrapper.Runner
{
    internal class Program
    {
        private static GPIOBoard _gpioClass;

        private static void Main(string[] args)
        {
            Console.WriteLine("Initializing GPIO...");

            var logger = new ConsoleLogger();
            var initWrapper = new InitWrapper();

            _gpioClass = new GPIOBoard(logger, initWrapper);

            RideTheHood();

            Console.WriteLine("All job is done");
            Console.ReadKey();
        }

        private static void RideTheHood()
        {
            var lFNumber = 1;
            var lBNumber = 1;
            var rFNumber = 1;
            var rBNumber = 1;

            IPin lF = new DOPin(lFNumber);
            IPin lB = new DOPin(lBNumber);
            IPin rF = new DOPin(rFNumber);
            IPin rB = new DOPin(rBNumber);

            var gpio = new GPIOWrapper();

            var rider = new Rider(gpio, lF, rF, lB, rB);

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

        private static LcdDisplay GetLCDDisplay()
        {
            var timer = new TimerClass();
            var logger = new ConsoleLogger();

            var display = new LcdDisplay(timer, logger);

            return display;
        }

        private static void DoSonicMeasuring()
        {
            var gpioWrapper = new GPIOWrapper();
            var timingWrapper = new TimingWrapper();

            var sonicSensorDriver = new SonicSensorDriver(gpioWrapper, timingWrapper);
            sonicSensorDriver.Configure();

            do
            {
                sonicSensorDriver.GetDistance();

                Thread.Sleep(500);
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

        private static void DoPWM()
        {
            var driverHandler = new ServoDriver();
            driverHandler.Configure();
            driverHandler.Reset();
            Console.WriteLine($"Obtained hanlder: {driverHandler}");
            Console.ReadKey();

            string readedValue;
            while (true)
            {
                Console.WriteLine("Give me value:");
                readedValue = Console.ReadLine();

                if (string.IsNullOrEmpty(readedValue)) break;

                driverHandler.SetPWM(int.Parse(readedValue));
            }
        }

        private void DoDisco()
        {
            //var pinmode = GPIOpinmode.Output;
            //GPIO.pinMode(_testingPin, (int)pinmode);

            //Console.WriteLine("Staring blinker");
            //MakeSimpleBlinkingLoop(150);
        }

        private static void MakeSimpleBlinkingLoop(int iterarions)
        {
            //for (int i = 0; i < iterarions; i++)
            //{
            //    var delay = GetDelayTime(i);

            //    Console.WriteLine($"Setting pin {_testingPin} to state: {GPIOpinvalue.High}");
            //    GPIO.digitalWrite(_testingPin, (int)GPIOpinvalue.High);
            //    Thread.Sleep(delay);

            //    Console.WriteLine($"Setting pin {_testingPin} to state: {GPIOpinvalue.Low}");
            //    GPIO.digitalWrite(_testingPin, (int)GPIOpinvalue.Low);
            //    Thread.Sleep(delay);
            //}
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