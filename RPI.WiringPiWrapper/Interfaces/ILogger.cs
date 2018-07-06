namespace RPI.WiringPiWrapper.Interfaces
{
    public interface ILogger
    {
        void WriteMessage(string message);

        void WriteMessage(object objectToWrite);
    }
}