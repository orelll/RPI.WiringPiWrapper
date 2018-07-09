namespace RPI.WiringPiWrapper.WiringPi.Wrappers.Init
{
    public interface IWrapInit
    {
        int WiringPiSetup();

        int WiringPiSetupGpio();

        int WiringPiSetupSys();

        int WiringPiSetupPhys();
    }
}