using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SampleApp.TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToeControl.xaml
    /// </summary>
    public partial class TicTacToeControl : UserControl
    {
        public TicTacToeControl()
        {
            InitializeComponent();
            //Enumerable.Range(0, TicTacToeViewModel.Dimension)
            //    .Select(_ => new ColumnDefinition())
            //    .ToList()
            //    .ForEach(TicTacToeGrid.ColumnDefinitions.Add);


            //Enumerable.Range(0, TicTacToeViewModel.Dimension)
            //    .Select(_ => new RowDefinition())
            //    .ToList()
            //    .ForEach(TicTacToeGrid.RowDefinitions.Add);

            //for (int i = 0; i < TicTacToeViewModel.Dimension; i++)
            //{
            //    for (int j = 0; j < TicTacToeViewModel.Dimension; j++)
            //    {
            //        var button = new Button();
            //        Grid.SetColumn(button, i);
            //        Grid.SetRow(button, j);
            //        var dataBinding = new Binding($"{nameof(TicTacToeViewModel.Board)}[{i + j}]");
            //        button.SetBinding(DataContextProperty, dataBinding);
            //        TicTacToeGrid.Children.Add(button);
            //    }
            //}
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
