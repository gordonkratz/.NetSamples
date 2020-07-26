using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FrontendFramework;
using StockOptionApp.FIleDownload;
using Utilities.Castle;
using Utilities.FileProvider;

namespace StockOptionApp
{
    [InstallerDependsOn(typeof(FileDownloadInstaller))]
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterPlugin<Plugin, Control>();
            container.Register(Component.For<ViewModel>(),
                Component.For<IProvide<FlexOptionData>>().ImplementedBy<FlexOptionDataProvider>(),
                Component.For<IParse<FlexOptionData>>().ImplementedBy<FlexOptionParser>(),
                Component.For<IDownloadFile<FlexOptionData>>().ImplementedBy<FlexOptionFileDownloader>()
                );
        }
    }
}
