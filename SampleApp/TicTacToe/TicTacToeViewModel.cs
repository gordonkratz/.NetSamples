using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Ui.Utilities;

namespace SampleApp.TicTacToe
{
    public class TicTacToeViewModel : ViewModelBase
    {
        public ICommand MakeMove { get; }

        private int _dimension = 3;
        private TicTacToeState _nextMover = TicTacToeState.X;
        private TicTacToeViewModelItem[,] _itemsArray;
        private readonly ICheckTicTacToeEnd<TicTacToeViewModelItem> _checker;

        public TicTacToeViewModel()
        {
            _checker = new NaiveChecker<TicTacToeViewModelItem>();

            CellCollection = new ObservableCollection<TicTacToeViewModelItem>();
            MakeMove = new RelayCommand<TicTacToeViewModelItem>(MakeMoveInternal);
            UpdateDimension();
        }

        private void MakeMoveInternal(TicTacToeViewModelItem item)
        {
            item.State = NextMover;
            NextMover = NextMover == TicTacToeState.O ? TicTacToeState.X : TicTacToeState.O;
            (var isEndCondition, var winner) = _checker.IsGameOver(_itemsArray);
            if (isEndCondition)
                ClearBoard();
        }

        private void ClearBoard()
        {
            foreach (var item in _itemsArray)
            {
                item.State = TicTacToeState.None;
            }
        }

        public ObservableCollection<TicTacToeViewModelItem> CellCollection { get; }

        public TicTacToeState NextMover
        {
            get => _nextMover;
            set => OnPropertyChanged(ref _nextMover, value);
        }

        public int Dimension
        {
            get => _dimension;
            set
            {
                if (OnPropertyChanged(ref _dimension, value))
                {
                    UpdateDimension();
                }
            }
        }

        private void UpdateDimension()
        {
            _itemsArray = new TicTacToeViewModelItem[Dimension, Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    _itemsArray[i, j] = new TicTacToeViewModelItem(i, j);
                }
            }

            CellCollection.Clear();
            _itemsArray.Cast<TicTacToeViewModelItem>()
                .OrderBy(i => i.Column)
                .ThenBy(i => i.Row)
                .ToList()
                .ForEach(CellCollection.Add);

        }
    }

    public enum TicTacToeState
    {
        None = 0,
        X = 1,
        O = 2,
    }
}
