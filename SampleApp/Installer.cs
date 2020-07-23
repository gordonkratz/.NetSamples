using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SampleApp
{
    class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<SampleAppDataContext>(), 
                Component.For<App>(),
                Component.For<MainWindow>());
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Install(new TicTacToe.Installer());
        }
    }
}
