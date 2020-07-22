using Ui.Utilities;

namespace TicTacToe
{
    public class TicTacToeViewModelItem : ViewModelBase, ITicTacToeItem
    {
        private TicTacToeState _state;

        public TicTacToeViewModelItem()
        {
            _state = TicTacToeState.None;
        }

        public TicTacToeState State 
        { 
            get => _state;
            set => OnPropertyChanged(ref _state, value);
        }
    }
}
