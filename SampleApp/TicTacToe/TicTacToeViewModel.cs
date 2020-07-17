using SampleApp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SampleApp.TicTacToe
{
    public class TicTacToeViewModel : ViewModelBase
    {
        public ICommand MakeMove { get; }
        public static readonly int Dimension = 3;

        public TicTacToeViewModel()
        {
            var rand = new Random();
            var array = Enumerable.Range(0, Dimension * Dimension)
                    .Select(_ => new TicTacToeViewModelItem(TicTacToeState.None))
                    .ToArray();
            Cells = new ObservableCollection<TicTacToeViewModelItem>(array);
            MakeMove = new RelayCommand<TicTacToeViewModelItem>(MakeMoveInternal);
        }

        private void MakeMoveInternal(TicTacToeViewModelItem item)
        {
            item.State = item.State == TicTacToeState.O ? TicTacToeState.X : TicTacToeState.O;
        }

        public ObservableCollection<TicTacToeViewModelItem> Cells { get; } 
    }

    public enum TicTacToeState
    {
        None = 0,
        X = 1, 
        O = 2,
    }
}
