using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FrontendFramework;

namespace BindingSamples
{
    public class Plugin : IPlugin<BindingSamplesControl>
    {
        public string Header => "Binding Samples";
    }

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterPlugin<Plugin, BindingSamplesControl>();
            container.Register(Component.For<BindingSamplesViewModel>());
        }
    }
}
