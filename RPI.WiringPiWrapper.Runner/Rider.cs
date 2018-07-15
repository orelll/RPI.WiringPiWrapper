using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;

namespace RPI.WiringPiWrapper.Runner
{
    public class Rider
    {
        private IWrapGPIO _gpio;
        private IPin _leftForward;
        private IPin _leftBackward;
        private IPin _rightForward;
        private IPin _rightBackward;

        public Rider(GPIOWrapper gpio, IPin leftForward, IPin rightForward)
        {
            _gpio = gpio;
            _leftForward = leftForward;
            _leftBackward = null;

            _rightForward = rightForward;
            _rightBackward = null;
        }

        public Rider(GPIOWrapper gpio, IPin leftForward, IPin rightForward, IPin leftBackward, IPin rightBackward)
        {
            _gpio = gpio;
            _leftForward = leftForward;
            _leftBackward = leftBackward;

            _rightForward = rightForward;
            _rightBackward = rightBackward;
        }

        public void Stop()
        {
            _gpio.DigitalWrite(_leftForward, WiringPi.GPIO.GPIOpinvalue.Low);
            _gpio.DigitalWrite(_leftBackward, WiringPi.GPIO.GPIOpinvalue.Low);
            _gpio.DigitalWrite(_rightForward, WiringPi.GPIO.GPIOpinvalue.Low);
            _gpio.DigitalWrite(_rightBackward, WiringPi.GPIO.GPIOpinvalue.Low);
        }

        public void MoveAhead()
        {
            Stop();

            _gpio.DigitalWrite(_leftForward, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_rightForward, WiringPi.GPIO.GPIOpinvalue.High);
        }

        public void MoveBack()
        {
            Stop();

            _gpio.DigitalWrite(_leftBackward, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_rightBackward, WiringPi.GPIO.GPIOpinvalue.High);
        }

        public void TurnLeft()
        {
            Stop();

            _gpio.DigitalWrite(_rightForward, WiringPi.GPIO.GPIOpinvalue.High);
        }

        public void TurnRight()
        {
            Stop();

            _gpio.DigitalWrite(_leftForward, WiringPi.GPIO.GPIOpinvalue.High);
        }

        public void TurnLeftInPlace()
        {
            Stop();

            _gpio.DigitalWrite(_leftBackward, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_rightForward, WiringPi.GPIO.GPIOpinvalue.High);
        }

        public void TurnRightInPlace()
        {
            Stop();

            _gpio.DigitalWrite(_rightBackward, WiringPi.GPIO.GPIOpinvalue.High);
            _gpio.DigitalWrite(_leftForward, WiringPi.GPIO.GPIOpinvalue.High);
        }
    }
}