using System;
using System.Diagnostics;
using RPI.WiringPiWrapper.Helpers;
using RPI.WiringPiWrapper.WiringPi;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;

namespace RPI.WiringPiWrapper.Devices.SonicSensor
{
    public class SonicSensorDriver
    {
        private readonly int _echoPin;
        private readonly int _triggerPin;
        private readonly IWrapGPIO _gpioWrapper;

        private readonly HighPrecisionTimer2 _highPrecisionTimer;

        public SonicSensorDriver(IWrapGPIO gpioWrapper):this(echoPin:7, triggerPin:0, gpioWrapper:gpioWrapper)
        {
        }

        public SonicSensorDriver(int echoPin, int triggerPin, IWrapGPIO gpioWrapper)
        {
            _gpioWrapper = gpioWrapper ?? throw new ArgumentNullException(nameof(gpioWrapper));
            _echoPin = echoPin;
            _triggerPin = triggerPin;

            Console.WriteLine($"Sonic sensor driver configured. Echo: {_echoPin}, trigger: {_triggerPin}");
        }

        public void Configure()
        {
            Console.WriteLine("Starting sonic sensor configuration...");

            _gpioWrapper.PinMode(_triggerPin, (int)GPIO.GPIOpinmode.Output);
            _gpioWrapper.PinMode(_echoPin, (int)GPIO.GPIOpinmode.Input);

            _gpioWrapper.DigitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);

            Console.WriteLine("Done.");
        }

        public double GetDistance()
        {
            var stopwatch = new Stopwatch();

            Console.WriteLine("Triggering device...");
            // Clears the trigPin
            _gpioWrapper.DigitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);
            _highPrecisionTimer.SleepMicroseconds(5);

            // Sets the trigPin on HIGH state for 10 micro seconds
            _gpioWrapper.DigitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.High);
            _highPrecisionTimer.SleepMicroseconds(10);
            _gpioWrapper.DigitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);

            while (_gpioWrapper.DigitalRead(_echoPin) != (int)GPIO.GPIOpinvalue.High) { };
            stopwatch.Start();

            // Reads the echoPin, returns the sound wave travel time in microseconds
            while (_gpioWrapper.DigitalRead(_echoPin) == (int)GPIO.GPIOpinvalue.High) { }

            stopwatch.Stop();
            var echSleptTime = stopwatch.Elapsed.TotalMilliseconds;
            var distance = echSleptTime * 343 / 2;

            return distance;
        }
    }
}