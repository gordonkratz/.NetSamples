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
        public int[] BoardSizeOptions { get; } = Enumerable.Range(3, 10).ToArray();

        TicTacToeViewModel _vm;

        public TicTacToeControl(TicTacToeViewModel vm)
        {
            InitializeComponent();
            DataContext = _vm = vm;
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
