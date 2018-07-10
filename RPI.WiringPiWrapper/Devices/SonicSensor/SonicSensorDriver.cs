using RPI.WiringPiWrapper.Hardware.Pins;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Timing;
using System;
using System.Diagnostics;

namespace RPI.WiringPiWrapper.Devices.SonicSensor
{
    public class SonicSensorDriver
    {
        private readonly IPin _echoPin;
        private readonly IPin _triggerPin;
        private readonly IWrapGPIO _gpio;
        private readonly IWrapTiming _timing;

        public SonicSensorDriver(IWrapGPIO gpioWrapper, IWrapTiming timingWrapper) : this(echoPin: new DIPin(7), triggerPin: new DOPin(0)
                                                                                        , gpioWrapper: gpioWrapper, timingWrapper: timingWrapper)
        {

        }

        public SonicSensorDriver(IPin echoPin, IPin triggerPin, IWrapGPIO gpioWrapper, IWrapTiming timingWrapper)
        {
            _gpio = gpioWrapper ?? throw new ArgumentNullException(nameof(gpioWrapper));
            _timing = timingWrapper ?? throw new ArgumentNullException(nameof(timingWrapper));

            PinValidator.Using(echoPin).AgainstNull().ValidateMode(GPIO.GPIOpinmode.Input);
            PinValidator.Using(triggerPin).AgainstNull().ValidateMode(GPIO.GPIOpinmode.Output);

            _echoPin = echoPin;
            _triggerPin = triggerPin;

            Console.WriteLine($"Sonic sensor driver configured. Echo: {_echoPin}, trigger: {_triggerPin}");
        }

        public void Configure()
        {
            Console.WriteLine("Starting sonic sensor configuration...");

            _gpio.PinMode(_triggerPin);
            _gpio.PinMode(_echoPin);

            _gpio.DigitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);

            Console.WriteLine("Done.");
        }

        public double GetDistance()
        {
            var stopwatch = new Stopwatch();

            Console.WriteLine("Triggering device...");
            // Clears the trigPin
            _gpio.DigitalWrite(_triggerPin, (int)GPIO.GPIOpinvalue.Low);
            _timing.DelayMicroseconds(5);

            // Sets the trigPin on HIGH state for 10 micro seconds
            _gpio.DigitalWrite(_triggerPin, GPIO.GPIOpinvalue.High);
            _timing.DelayMicroseconds(10);
            _gpio.DigitalWrite(_triggerPin, GPIO.GPIOpinvalue.Low);

            while (_gpio.DigitalRead(_echoPin) != GPIO.GPIOpinvalue.High) { };
            stopwatch.Start();

            // Reads the echoPin, returns the sound wave travel time in microseconds
            while (_gpio.DigitalRead(_echoPin) == GPIO.GPIOpinvalue.High) { }

            stopwatch.Stop();
            var echSleptTime = stopwatch.Elapsed.TotalMilliseconds;
            var distance = echSleptTime * 343 / 2;

            return distance;
        }
    }
}