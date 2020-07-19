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

        private TicTacToeState _nextMover = TicTacToeState.X;
        private readonly TicTacToeViewModelItem[,] _itemsArray;

        public TicTacToeViewModel()
        {
            var rand = new Random();
            _itemsArray = new TicTacToeViewModelItem[Dimension, Dimension];
            for(int i = 0; i < Dimension; i++)
            {
                for(int j = 0; j < Dimension; j++)
                {
                    _itemsArray[i, j] = new TicTacToeViewModelItem(i, j);
                }
            }

            CellCollection = new ObservableCollection<TicTacToeViewModelItem>(
                _itemsArray.Cast<TicTacToeViewModelItem>().OrderBy(i => i.Column).ThenBy(i => i.Row));
            MakeMove = new RelayCommand<TicTacToeViewModelItem>(MakeMoveInternal);
        }

        private void MakeMoveInternal(TicTacToeViewModelItem item)
        {
            item.State = _nextMover;
            _nextMover = _nextMover == TicTacToeState.O ? TicTacToeState.X : TicTacToeState.O;

        }

        public ObservableCollection<TicTacToeViewModelItem> CellCollection { get; } 
    }

    public enum TicTacToeState
    {
        None = 0,
        X = 1, 
        O = 2,
    }
}
