namespace RPI.WiringPiWrapper.Interfaces
{
    public interface ITimer
    {
        void SleepMicroseconds(int microsecondsToSleep);
        void SleepMiliseconds(int milisecondsToSleep);
        void SleepSeconds(int secondsToSleep);
    }
}
