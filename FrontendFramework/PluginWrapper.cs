using System.Windows.Controls;

namespace FrontendFramework
{
    public interface IPluginWrapper
    {
        public IPlugin Plugin { get; }
        public Control Control { get; }
    }

    public class PluginWrapper<T, K> : IPluginWrapper
        where T : IPlugin<K>
        where K : Control 
    {
        public PluginWrapper(T plugin, K control)
        {
            Plugin = plugin;
            Control = control;
        }

        public IPlugin Plugin { get; }
        public Control Control { get; }
    }
}
