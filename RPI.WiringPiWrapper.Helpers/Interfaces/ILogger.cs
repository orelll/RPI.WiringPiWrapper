namespace RPI.WiringPiWrapper.Helpers.Interfaces
{
    public interface ILogger
    {
        void WriteMessage(string message);

        void WriteMessage(object objectToWrite);
    }
}