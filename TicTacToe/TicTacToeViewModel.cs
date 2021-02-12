using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Ui.Utilities;

namespace TicTacToe
{
    public class TicTacToeViewModel : ViewModelBase
    {
        public ICommand MakeMove { get; }
        public ICommand ResetCommand { get; }

        private int _boardSize = 3;
        private TicTacToeState _nextMover = TicTacToeState.X;
        private TicTacToeMode _mode = TicTacToeMode.TwoPlayer;
        private event Action<TicTacToeState> _gameOver;
        private TicTacToeViewModelItem[,] _itemsArray;
        private readonly ICheckTicTacToeEnd<TicTacToeViewModelItem> _checker;
        private readonly IComputerLogic<TicTacToeViewModelItem> _computerLogic;

        public TicTacToeViewModel(ICheckTicTacToeEnd<TicTacToeViewModelItem> checker, 
            IComputerLogic<TicTacToeViewModelItem> logic)
        {
            _checker = checker;
            _computerLogic = logic;

            CellCollection = new ObservableCollection<TicTacToeViewModelItem>();
            MakeMove = new RelayCommand<TicTacToeViewModelItem>(MakeMoveCommandInternal);
            ResetCommand = new RelayCommand(Reset);
            UpdateBoardSize();
        }

        public event Action<TicTacToeState> GameOver
        {
            add => _gameOver += value;
            remove => _gameOver -= value;
        }

        private void MakeMoveCommandInternal(TicTacToeViewModelItem item)
        {
            
            if (!UpdateBoard(item) && Mode == TicTacToeMode.VsComputer)
            {
                if(_computerLogic.TryMakeComputerChoice(_itemsArray, NextMover, out var selected))
                {
                    UpdateBoard(selected);
                }
                else
                {
                    Reset();
                }
            }
        }

        private bool UpdateBoard(TicTacToeViewModelItem item)
        {
            item.State = NextMover;
            NextMover = NextMover == TicTacToeState.O ? TicTacToeState.X : TicTacToeState.O;
            (var isEndCondition, var winner) = _checker.IsGameOver(_itemsArray);
            if (isEndCondition)
            {
                _gameOver?.Invoke(winner);
            }
            return isEndCondition;
        }

        public void Reset()
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
            set => SetProperty(ref _nextMover, value);
        }

        public int BoardSize
        {
            get => _boardSize;
            set
            {
                if (SetProperty(ref _boardSize, value))
                {
                    UpdateBoardSize();
                }
            }
        }

        public TicTacToeMode Mode
        {
            get => _mode;
            set
            {
                if (SetProperty(ref _mode, value))
                {
                    Reset();
                }
            }
        }

        private void UpdateBoardSize()
        {
            CellCollection.Clear();
            _itemsArray = new TicTacToeViewModelItem[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    CellCollection.Add(_itemsArray[i, j] = new TicTacToeViewModelItem());

                }
            }
        }
    }

    public enum TicTacToeState
    {
        None = 0,
        X = 1,
        O = 2,
    }

    public enum TicTacToeMode
    {
        TwoPlayer,
        VsComputer
    }
}
