using WiringPiInit = RPI.WiringPiWrapper.WiringPi;

namespace RPI.WiringPiWrapper.WiringPi.Wrappers.Init
{
    public class InitWrapper : IWrapInit
    {
        public int WiringPiSetup()
        {
            return WiringPiInit.Init.WiringPiSetup();
        }

        public int WiringPiSetupGpio()
        {
            return WiringPiInit.Init.WiringPiSetupGpio();
        }

        public int WiringPiSetupPhys()
        {
            return WiringPiInit.Init.WiringPiSetupPhys();
        }

        public int WiringPiSetupSys()
        {
            return WiringPiInit.Init.WiringPiSetupSys();
        }
    }
}