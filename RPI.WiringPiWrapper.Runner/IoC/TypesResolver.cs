using Autofac;

namespace RPI.WiringPiWrapper.ConsoleRunner.IoC
{
    public class TypesResolver
    {
        private readonly IContainer _container;

        public TypesResolver()
        {
            var containerBuilder = new ContainerBuilder();
            ContainerBootstrapper.DoRegistrations(containerBuilder);

            _container = containerBuilder.Build();
        }

        public T ResolveType<T>(string name)
        {
           return !string.IsNullOrEmpty(name) ? _container.ResolveNamed<T>(name) : ResolveType<T>();
        }

        public T ResolveType<T>()
        {
            return _container.Resolve<T>();
        }
    }
}