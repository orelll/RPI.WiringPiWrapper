using rpi.wiringpiwrapper;
using RPI.WiringPiWrapper.Helpers;
using RPI.WiringPiWrapper.Helpers.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace RPI.WiringPiWrapper.SonicSensor
{
    public class SonicSensorDriver
    {
        private int _echo_pin = 7;
        private int _trigger_pin = 0;
        private HighPrecisionTimer2 _highPrecisionTimer;

        public SonicSensorDriver()
        {
            Console.WriteLine($"Sonic sensor driver configured. Echo: {_echo_pin}, trigger: {_trigger_pin}");
            _highPrecisionTimer = new HighPrecisionTimer2();
        }

        public SonicSensorDriver(int echoPin, int triggerPin)
        {
            _echo_pin = echoPin;
            _trigger_pin = triggerPin;

            Console.WriteLine($"Sonic sensor driver configured. Echo: {_echo_pin}, trigger: {_trigger_pin}");
        }


        public void Configure()
        {
            Console.WriteLine("Starting sonic sensor configuration...");
            GPIO.pinMode(_trigger_pin, (int)GPIO.GPIOpinmode.Output);
            GPIO.pinMode(_echo_pin, (int)GPIO.GPIOpinmode.Input);

            GPIO.digitalWrite(_trigger_pin, (int)GPIO.GPIOpinvalue.Low);

            Console.WriteLine("Done.");
        }

        public double GetDistance()
        {
            var stopwatch = new Stopwatch();
            
            Console.WriteLine("Triggering device...");
            // Clears the trigPin
            GPIO.digitalWrite(_trigger_pin, (int)GPIO.GPIOpinvalue.Low);
            _highPrecisionTimer.SleepMicroseconds(5);
            
            // Sets the trigPin on HIGH state for 10 micro seconds
            GPIO.digitalWrite(_trigger_pin, (int)GPIO.GPIOpinvalue.High);
            _highPrecisionTimer.SleepMicroseconds(10);
            GPIO.digitalWrite(_trigger_pin, (int)GPIO.GPIOpinvalue.Low);

            while (GPIO.digitalRead(_echo_pin) != (int)GPIO.GPIOpinvalue.High) { };
            //var elapsedEchoTime = HighPrecisionTimer2.GetTimeUntilNextEdge(_echo_pin, (int)GPIO.GPIOpinvalue.High);
            stopwatch.Start();

            // Reads the echoPin, returns the sound wave travel time in microseconds
            while (GPIO.digitalRead(_echo_pin) == (int)GPIO.GPIOpinvalue.High) { }

            stopwatch.Stop();
            var echSleptTime = stopwatch.Elapsed.TotalMilliseconds;
            var distance = echSleptTime * 343 / 2;
            // Prints the distance on the Serial Monitor
            Console.WriteLine($"Elapsed time{echSleptTime} [ms] timestamp: {DateTime.Now.Second}");
            Console.WriteLine($"Measured distance: {distance}");

            return distance;
        }

       

    }
}
