using RPI.WiringPiWrapper.Interfaces;

namespace RPI.WiringPiWrapper.Helpers.Loggers
{
    public class DummyLogger : ILogger
    {
        public void WriteMessage(string message)
        {
        }

        public void WriteMessage(object objectToSave)
        {
        }
    }
}