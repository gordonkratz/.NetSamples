using Castle.MicroKernel.Registration;
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
            container.Install(new TicTacToe.Installer());
        }
    }
}
