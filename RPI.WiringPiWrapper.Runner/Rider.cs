using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using System.Runtime.CompilerServices;

namespace RPI.WiringPiWrapper.Runner
{
    public class Rider
    {
        private ILogger log;

        private IWrapGPIO _gpio;
        private IPin _leftPin;
        private IPin _leftBackward;
        private IPin _rightPin;
        private IPin _rightBackward;

        public Rider(GPIOWrapper gpio, IPin leftPin, IPin rightPin, ILogger logger)
        {
            _gpio = gpio;
            _leftPin = leftPin;
            _leftBackward = null;

            _rightPin = rightPin;
            _rightBackward = null;

            log = logger;
        }

        public Rider(IWrapGPIO gpio, IPin leftPin, IPin rightPin, IPin leftBackward, IPin rightBackward, ILogger logger)
        {
            _gpio = gpio;
            _leftPin = leftPin;
            _leftBackward = leftBackward;

            _rightPin = rightPin;
            _rightBackward = rightBackward;

            log = logger;
        }

        public void Stop()
        {
            AddExecuteLog();
            _gpio.DigitalWrite(_leftPin, WiringPi.GPIO.GPIOpinvalue.Low);
            _gpio.DigitalWrite(_leftBackward, WiringPi.GPIO.GPIOpinvalue.Low);
            _gpio.DigitalWrite(_rightPin, WiringPi.GPIO.GPIOpinvalue.Low);
            _gpio.DigitalWrite(_rightBackward, WiringPi.GPIO.GPIOpinvalue.Low);
            AddDoneLog();
        }

        public void MoveAhead()
        {
            Stop();

            AddExecuteLog();
            _gpio.DigitalWrite(_leftPin, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_rightPin, WiringPi.GPIO.GPIOpinvalue.High);
            AddDoneLog();
        }

        public void MoveBack()
        {
            Stop();

            AddExecuteLog();
            _gpio.DigitalWrite(_leftBackward, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_rightBackward, WiringPi.GPIO.GPIOpinvalue.High);
            AddDoneLog();
        }

        public void TurnLeft()
        {
            Stop();

            AddExecuteLog();
            _gpio.DigitalWrite(_rightPin, WiringPi.GPIO.GPIOpinvalue.High);
            AddDoneLog();
        }

        public void TurnRight()
        {
            Stop();

            AddExecuteLog();
            _gpio.DigitalWrite(_leftPin, WiringPi.GPIO.GPIOpinvalue.High);
            AddDoneLog();
        }

        public void TurnLeftInPlace()
        {
            Stop();

            AddExecuteLog();
            _gpio.DigitalWrite(_leftBackward, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_rightPin, WiringPi.GPIO.GPIOpinvalue.High);
            AddDoneLog();
        }

        public void TurnRightInPlace()
        {
            Stop();

            AddExecuteLog();
            _gpio.DigitalWrite(_rightBackward, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_leftPin, WiringPi.GPIO.GPIOpinvalue.High);
            AddDoneLog();
        }

        private void AddExecuteLog([CallerMemberName]string caller = "")
        {
            log.WriteMessage(caller);
        }

        private void AddDoneLog()
        {
            log.WriteMessage("done");
        }
    }
}