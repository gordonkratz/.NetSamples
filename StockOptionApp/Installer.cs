using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FrontendFramework;

namespace StockOptionApp
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterPlugin<Plugin, Control>();
            container.Register(Component.For<ViewModel>());
        }
    }
}
