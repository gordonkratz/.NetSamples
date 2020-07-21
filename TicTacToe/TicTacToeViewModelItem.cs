using TicTacToe;
using Ui.Utilities;

namespace TicTacToe
{
    public class TicTacToeViewModelItem : ViewModelBase, ITicTacToeItem
    {
        private TicTacToeState _state;

        public TicTacToeViewModelItem(int column, int row)
        {
            _state = TicTacToeState.None;
            Column = column;
            Row = row;
        }

        public TicTacToeState State 
        { 
            get => _state;
            set => OnPropertyChanged(ref _state, value);
        }

        public int Column { get; }

        public int Row { get; }
    }
}
