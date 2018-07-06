using System.Collections.Generic;

namespace RPI.WiringPiWrapper.Interfaces
{
    public interface IDevice
    {
        IEnumerable<int> ListUsedPins { get; }
    }
}