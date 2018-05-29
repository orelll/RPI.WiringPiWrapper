using RPI.WiringPiWrapper;
using RPI.WiringPiWrapper.ServoDriver;
using RPI.WiringPiWrapper.SonicSensor;
using System;
using System.Threading;
using static rpi.wiringpiwrapper.GPIO;

namespace rpi.wiringpiwrapper
{
    internal class Program
    {
        private static readonly int _testingPin = 1;

        private static void Main(string[] args)
        {
            Console.WriteLine("Initializing GPIO...");
            if (InitGPIO() == -1)
            {
                Console.WriteLine("GPIO initialization failed");
                return;
            }

            DoSonicMeasuring();

            Console.WriteLine("All job is done");
            Console.ReadKey();
            GPIO.digitalWrite(1, (int)GPIOpinvalue.Low);
        }

        private static void DoSonicMeasuring()
        {
            var readedLine = string.Empty;
            var sonicSensorDriver = new SonicSensorDriver();

            do
            {
                sonicSensorDriver.Configure();
                sonicSensorDriver.GetDistance();

                Thread.Sleep(500);
            }
            while (true);
        }

        private static void DoMessaging()
        {
            var readedLine = string.Empty;
            var mylcd = new LcdClass();

            do
            {
                mylcd.lcd_clear();
                mylcd.lcd_display_string(readedLine);

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

        //private static void DoPWM()
        //{
        //    const int servoMin = 1;  // Min pulse length out of 4095
        //    const int servoMax = 4000;  // Max pulse length out of 4095
        //    Console.WriteLine("Initializing pwm driver...");
        //    var pwmDriver = new PWMServoDriver2();
        //    pwmDriver.pca9685Setup(120, 0x40, 50);
        //    Console.WriteLine("Done.");
        //    Console.ReadKey();

        //    for (ushort i = servoMin; i < servoMax; i++)
        //    {
        //        Console.WriteLine($"Setting value {i}");
        //        pwmDriver.myPwmWrite(1, 1);
        //        Thread.Sleep(10);
        //    }
        //    for (ushort i = servoMax; i > servoMin; i--)
        //    {
        //        Console.WriteLine($"Setting value {i}");
        //        pwmDriver.myPwmWrite(1, 0);
        //        Thread.Sleep(10);
        //    }
        //}

        private void DoDisco()
        {
            var pinmode = GPIOpinmode.Output;
            GPIO.pinMode(_testingPin, (int)pinmode);

            Console.WriteLine("Staring blinker");
            MakeSimpleBlinkingLoop(150);
        }

        private static int InitGPIO()
        {
            var gpioResponse = rpi.wiringpiwrapper.Init.WiringPiSetup();

            return gpioResponse;
        }

        private static void MakeSimpleBlinkingLoop(int iterarions)
        {
            for (int i = 0; i < iterarions; i++)
            {
                var delay = GetDelayTime(i);

                Console.WriteLine($"Setting pin {_testingPin} to state: {GPIOpinvalue.High}");
                GPIO.digitalWrite(_testingPin, (int)GPIOpinvalue.High);
                Thread.Sleep(delay);

                Console.WriteLine($"Setting pin {_testingPin} to state: {GPIOpinvalue.Low}");
                GPIO.digitalWrite(_testingPin, (int)GPIOpinvalue.Low);
                Thread.Sleep(delay);
            }
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