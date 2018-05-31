using System;
using System.Collections.Generic;
using System.Text;

namespace RPI.WiringPiWrapper.Helpers.Interfaces
{
    public interface IHighPrecisionTimeMeasurement
    {
        void SleepMicroseconds(long microseconds);
        double ConvertTicksToDistance(long ticks);
    }
}
