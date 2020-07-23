using FrontendFramework;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IEnumerable<IPluginWrapper> plugins)
        {
            InitializeComponent();
            foreach(var plugin in plugins)
            {
                var tabItem = new TabItem
                {
                    Content = plugin.Control,
                    Header = plugin.Plugin.Header
                };

                _tabControl.Items.Add(tabItem);
            }
        }
    }
}
