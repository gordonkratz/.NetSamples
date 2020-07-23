using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Windows.Controls;

namespace FrontendFramework
{
    public interface IPlugin
    {
        string Header { get; }
    }

    public interface IPlugin<T> : IPlugin where T : Control
    {
    }

    public static class PluginEx
    {
        public static void RegisterPlugin<T, K>(this IWindsorContainer container) 
            where T : class, IPlugin<K>
            where K : Control
        {
            container.Register(Component.For<T>(),
                Component.For<K>(),
                Component.For<IPluginWrapper>().ImplementedBy<PluginWrapper<T, K>>()
                );
        }
    }
}
