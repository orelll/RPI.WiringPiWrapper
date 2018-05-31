using System;
using System.Collections.Generic;
using System.Text;

namespace RPI.WiringPiWrapper.Helpers.Interfaces
{
    public interface ITimer
    {
        void SleepMicroseconds(int microsecondsToSleep);
        void SleepMiliseconds(int milisecondsToSleep);
        void SleepSeconds(int secondsToSleep);
    }
}
