using RPI.WiringPiWrapper.Helpers.Interfaces;
using System;

namespace RPI.WiringPiWrapper.Helpers.Tools.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteMessage(object objectToWrite)
        {
            var objectAsString = objectToWrite.ToString();
            WriteMessage(objectAsString);
        }
    }
}