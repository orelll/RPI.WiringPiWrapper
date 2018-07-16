using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using System.Runtime.CompilerServices;

namespace RPI.WiringPiWrapper.Runner
{
    public class Rider
    {
        private ILogger log;

        private IWrapGPIO _gpio;
        private IPin _leftForward;
        private IPin _leftBackward;
        private IPin _rightForward;
        private IPin _rightBackward;

        public Rider(GPIOWrapper gpio, IPin leftForward, IPin rightForward, ILogger logger)
        {
            _gpio = gpio;
            _leftForward = leftForward;
            _leftBackward = null;

            _rightForward = rightForward;
            _rightBackward = null;

            log = logger;
        }

        public Rider(GPIOWrapper gpio, IPin leftForward, IPin rightForward, IPin leftBackward, IPin rightBackward, ILogger logger)
        {
            _gpio = gpio;
            _leftForward = leftForward;
            _leftBackward = leftBackward;

            _rightForward = rightForward;
            _rightBackward = rightBackward;

            log = logger;
        }

        public void Stop()
        {
            AddExecuteLog();
            _gpio.DigitalWrite(_leftForward, WiringPi.GPIO.GPIOpinvalue.Low);
            _gpio.DigitalWrite(_leftBackward, WiringPi.GPIO.GPIOpinvalue.Low);
            _gpio.DigitalWrite(_rightForward, WiringPi.GPIO.GPIOpinvalue.Low);
            _gpio.DigitalWrite(_rightBackward, WiringPi.GPIO.GPIOpinvalue.Low);
            AddDoneLog();
        }

        public void MoveAhead()
        {
            Stop();

            AddExecuteLog();
            _gpio.DigitalWrite(_leftForward, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_rightForward, WiringPi.GPIO.GPIOpinvalue.High);
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
            _gpio.DigitalWrite(_rightForward, WiringPi.GPIO.GPIOpinvalue.High);
            AddDoneLog();
        }

        public void TurnRight()
        {
            Stop();

            AddExecuteLog();
            _gpio.DigitalWrite(_leftForward, WiringPi.GPIO.GPIOpinvalue.High);
            AddDoneLog();
        }

        public void TurnLeftInPlace()
        {
            Stop();

            AddExecuteLog();
            _gpio.DigitalWrite(_leftBackward, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_rightForward, WiringPi.GPIO.GPIOpinvalue.High);
            AddDoneLog();
        }

        public void TurnRightInPlace()
        {
            Stop();

            AddExecuteLog();
            _gpio.DigitalWrite(_rightBackward, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_leftForward, WiringPi.GPIO.GPIOpinvalue.High);
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