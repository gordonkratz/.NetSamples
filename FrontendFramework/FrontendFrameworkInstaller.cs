using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Utilities.Castle;
using Utilities.Configuration;

namespace FrontendFramework
{
    [InstallerDependsOn(typeof(ConfigurationInstaller))]
    internal class FrontendFrameworkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<MainWindow>(),
                Component.For<IWpfThread>().ImplementedBy<WpfDispatcher>()
                );
            container.AddFacility<StartableFacility>();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
        }
    }
}
