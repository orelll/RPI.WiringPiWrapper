using rpi.wiringpiwrapper;
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

        public SonicSensorDriver()
        {
            Console.WriteLine($"Sonic sensor driver configured. Echo: {_echo_pin}, trigger: {_trigger_pin}");
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
            // Clears the trigPin
            GPIO.digitalWrite(_trigger_pin, (int)GPIO.GPIOpinvalue.Low);
            Sleeper(2);

            // Sets the trigPin on HIGH state for 10 micro seconds
            GPIO.digitalWrite(_trigger_pin, (int)GPIO.GPIOpinvalue.High);
            Sleeper(10);
            GPIO.digitalWrite(_trigger_pin, (int)GPIO.GPIOpinvalue.Low);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Reads the echoPin, returns the sound wave travel time in microseconds
            while (GPIO.digitalRead(_echo_pin) == (int)GPIO.GPIOpinvalue.Low) { }

            stopwatch.Stop();

            var distance = ConvertTicksToDistance(stopwatch.ElapsedTicks);

            // Prints the distance on the Serial Monitor
            Console.WriteLine($"Measured distance: {distance}");

            return distance;
        }

        private double ConvertTicksToDistance(long ticks)
        {
            var multipler = TimeSpan.TicksPerMillisecond;
            var ticksAsMicroseconds = ticks / multipler;
            Console.WriteLine($"ticks multipler: {multipler}");

            // Calculating the distance
            var distance = ticksAsMicroseconds * 0.034 / 2;

            return distance;
        }

        private void Sleeper(int microseconds)
        {
            var multipler = TimeSpan.TicksPerMillisecond;
            var ticksToWait = microseconds * multipler / 1000;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.ElapsedTicks <= ticksToWait) { }

            stopwatch.Stop();
            Console.WriteLine($"Stopwatch sleeper: {ticksToWait} vs {stopwatch.ElapsedTicks}");
        }

    }
}
