using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuSolver
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SudokuControl : UserControl
    {
        public SudokuControl(SudokuViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = ((sender as Button)?.DataContext as SudokuViewModelItem);
            if (vm == null)
                return;

            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                vm.Value = 0;
            }
            else
            {
                vm.Value = (vm.Value + 1) % 10;
            }
            vm.Fixed = vm.Value != 0;

        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var vm = ((sender as Button)?.DataContext as SudokuViewModelItem);
            if (vm == null)
                return;
            vm.Value = vm.Value == 0 ? 9 : (vm.Value - 1);
            vm.Fixed = vm.Value != 0;
        }
    }

    public class ZeroToEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int i)
            {
                return i == 0 ? (object) "" : i;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}