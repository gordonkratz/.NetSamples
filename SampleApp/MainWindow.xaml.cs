using System.Windows;
using System.Windows.Controls;
using TicTacToe;

namespace SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(TicTacToeControl control)
        {
            InitializeComponent();
            var tabItem = new TabItem
            {
                Content = control
            };

            _tabControl.Items.Add(tabItem);
        }
    }
}
