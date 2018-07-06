using System;
using System.Diagnostics;
using RPI.WiringPiWrapper.Tools;
using RPI.WiringPiWrapper.WiringPi;

namespace RPI.WiringPiWrapper.Devices.SonicSensor
{
    public class SonicSensorDriver
    {
        private readonly int _echoPin = 7;
        private readonly int _triggerPin = 0;

        private readonly HighPrecisionTimer2 _highPrecisionTimer;

        public SonicSensorDriver()
        {
            Console.WriteLine($"Sonic sensor driver configured. Echo: {_echoPin}, trigger: {_triggerPin}");
            _highPrecisionTimer = new HighPrecisionTimer2();
        }

        public SonicSensorDriver(int echoPin, int triggerPin)
        {
            _echoPin = echoPin;
            _triggerPin = triggerPin;

            Console.WriteLine($"Sonic sensor driver configured. Echo: {_echoPin}, trigger: {_triggerPin}");
        }

        public void Configure()
        {
            Console.WriteLine("Starting sonic sensor configuration...");

            GPIO.PinMode(_triggerPin, (int)GPIO.GPIOpinmode.Output);
            GPIO.PinMode(_echoPin, (int)GPIO.GPIOpinmode.Input);

            GPIO.DigitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);

            Console.WriteLine("Done.");
        }

        public double GetDistance()
        {
            var stopwatch = new Stopwatch();

            Console.WriteLine("Triggering device...");
            // Clears the trigPin
            GPIO.DigitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);
            _highPrecisionTimer.SleepMicroseconds(5);

            // Sets the trigPin on HIGH state for 10 micro seconds
            GPIO.DigitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.High);
            _highPrecisionTimer.SleepMicroseconds(10);
            GPIO.DigitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);

            while (GPIO.DigitalRead(_echoPin) != (int)GPIO.GPIOpinvalue.High) { };
            stopwatch.Start();

            // Reads the echoPin, returns the sound wave travel time in microseconds
            while (GPIO.DigitalRead(_echoPin) == (int)GPIO.GPIOpinvalue.High) { }

            stopwatch.Stop();
            var echSleptTime = stopwatch.Elapsed.TotalMilliseconds;
            var distance = echSleptTime * 343 / 2;

            return distance;
        }
    }
}