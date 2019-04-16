using Autofac;
using RPI.WiringPiWrapper.Devices.ServoDriver;
using RPI.WiringPiWrapper.Hardware.Pins;
using RPI.WiringPiWrapper.Helpers;
using RPI.WiringPiWrapper.Helpers.Loggers;
using RPI.WiringPiWrapper.Interfaces;
using RPI.WiringPiWrapper.WiringPi.Wrappers.GPIO;
using RPI.WiringPiWrapper.WiringPi.Wrappers.I2C;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Init;
using RPI.WiringPiWrapper.WiringPi.Wrappers.Timing;

namespace RPI.WiringPiWrapper.ConsoleRunner.IoC
{
    public static class ContainerBootstrapper
    {
        public static void DoRegistrations(ContainerBuilder builder)
        {
            builder.RegisterType<DIPin>().Named<IPin>("DI");
            builder.RegisterType<DOPin>().Named<IPin>("DO");
           

            builder.RegisterType<TimerClass>().As<ITimer>();
            builder.RegisterType<NLogger>().As<ILogger>();
            builder.RegisterType<I2CWrapper>().As<IWrapI2C>();
            builder.RegisterType<GPIOWrapper>().As<IWrapGPIO>();
            builder.RegisterType<TimingWrapper>().As<IWrapTiming>();
            builder.RegisterType<InitWrapper>().As<IWrapInit>();

            builder.RegisterType<ServoDriver>().Named<II2CDevice>("Servo");
        }
    }
}