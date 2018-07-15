using RPI.WiringPiWrapper.Interfaces;
using System;

namespace RPI.WiringPiWrapper.Helpers.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteMessage(object objectToWrite)
        {
            Console.WriteLine(objectToWrite.ToString());
        }
    }
}