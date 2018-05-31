using RPI.WiringPiWrapper;
using RPI.WiringPiWrapper.Devices.GPIO;
using RPI.WiringPiWrapper.Devices.LCD_Display;
using RPI.WiringPiWrapper.Helpers;
using RPI.WiringPiWrapper.Helpers.Interfaces;
using RPI.WiringPiWrapper.Helpers.Tools;
using RPI.WiringPiWrapper.Helpers.Tools.Loggers;
using RPI.WiringPiWrapper.ServoDriver;
using RPI.WiringPiWrapper.SonicSensor;
using System;
using System.Threading;
using static RPI.WiringPiWrapper.Helpers.GPIO;

namespace rpi.wiringpiwrapper
{
    internal class Program
    {
        private static GPIOClass _gpioClass;

        private static void Main(string[] args)
        {
            Console.WriteLine("Initializing GPIO...");

            var timer = new TimerClass();
            var logger = new ConsoleLogger();
            _gpioClass = new GPIOClass(logger);

            var lcdDisplay = GetLCDDisplay();
            _gpioClass.AddDevice(lcdDisplay);

            DoMessaging(lcdDisplay);

            Console.WriteLine("All job is done");
            Console.ReadKey();
            GPIO.digitalWrite(1, (int)GPIOpinvalue.Low);
        }

        private static LCD_Display GetLCDDisplay()
        {
            var timer = new TimerClass();
            var logger = new ConsoleLogger();

            var display = new LCD_Display(timer, logger);

            return display;
        }

        private static void DoSonicMeasuring()
        {
            var readedLine = string.Empty;
            var sonicSensorDriver = new SonicSensorDriver();
                sonicSensorDriver.Configure();

            do
            {
                sonicSensorDriver.GetDistance();

                Thread.Sleep(500);
            }
            while (true);
        }

        private static void DoMessaging(LCD_Display lcdDisplay)
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
            while(true)
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

        private static int InitGPIO()
        {
            var gpioResponse = Init.WiringPiSetup();

            return gpioResponse;
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