using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToeControl.xaml
    /// </summary>
    public partial class TicTacToeControl : UserControl
    {
        public TicTacToeMode[] TicTacToeModes { get; } = Enum.GetValues(typeof(TicTacToeMode)).Cast<TicTacToeMode>().ToArray();

        TicTacToeViewModel _vm;
        public TicTacToeControl()
        {
            InitializeComponent();
            this.DataContextChanged += TicTacToeControl_DataContextChanged;
        }

        private void TicTacToeControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(_vm != null)
                _vm.GameOver -= OnGameFinished;
            _vm = e.NewValue as TicTacToeViewModel;
            if(_vm != null)
                _vm.GameOver += OnGameFinished;
        }

        private void OnGameFinished(TicTacToeState winner)
        {
            MessageBox.Show($"{winner} has won the game! Play again?", "Game over", MessageBoxButton.OK, MessageBoxImage.Question);
            _vm.Reset();
        }
    }

    public class TicTacToeButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumValue = value as TicTacToeState?;
            switch (enumValue)
            {
                case TicTacToeState.O:
                    return "O";
                case TicTacToeState.X:
                    return "X";
                case TicTacToeState.None:
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
