using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Net.Http;

namespace Utilities.FileProvider
{
    public class FileDownloadInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<HttpClient>(),
                Component.For(typeof(IProvide<>)).ImplementedBy(typeof(BasicProvider<>))
                );
        }
    }
}
