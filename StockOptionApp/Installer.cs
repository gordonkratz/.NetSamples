using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FrontendFramework;
using StockOptionApp.FIleDownload;
using System.IO;
using System.Net.Http;

namespace StockOptionApp
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterPlugin<Plugin, Control>();
            container.Register(Component.For<ViewModel>(),
                Component.For<IProvide<FlexOptionData>>().ImplementedBy<FlexOptionDataProvider>(),
                Component.For<ICsvParser<FlexOptionData>>().ImplementedBy<FlexOptionParser>(),
                Component.For<IDownloadFile<FlexOptionData>>().ImplementedBy<FlecxOptionFileDownloader>(),
                Component.For<HttpClient>()
                );
        }
    }
}
