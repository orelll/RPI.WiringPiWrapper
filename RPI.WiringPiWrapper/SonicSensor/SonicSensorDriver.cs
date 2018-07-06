using RPI.WiringPiWrapper.Helpers;
using RPI.WiringPiWrapper.Helpers.Tools;
using System;
using System.Diagnostics;

namespace RPI.WiringPiWrapper.SonicSensor
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

            GPIO.pinMode(_triggerPin, (int)GPIO.GPIOpinmode.Output);
            GPIO.pinMode(_echoPin, (int)GPIO.GPIOpinmode.Input);

            GPIO.digitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);

            Console.WriteLine("Done.");
        }

        public double GetDistance()
        {
            var stopwatch = new Stopwatch();

            Console.WriteLine("Triggering device...");
            // Clears the trigPin
            GPIO.digitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);
            _highPrecisionTimer.SleepMicroseconds(5);

            // Sets the trigPin on HIGH state for 10 micro seconds
            GPIO.digitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.High);
            _highPrecisionTimer.SleepMicroseconds(10);
            GPIO.digitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);

            while (GPIO.digitalRead(_echoPin) != (int)GPIO.GPIOpinvalue.High) { };
            stopwatch.Start();

            // Reads the echoPin, returns the sound wave travel time in microseconds
            while (GPIO.digitalRead(_echoPin) == (int)GPIO.GPIOpinvalue.High) { }

            stopwatch.Stop();
            var echSleptTime = stopwatch.Elapsed.TotalMilliseconds;
            var distance = echSleptTime * 343 / 2;

            return distance;
        }
    }
}