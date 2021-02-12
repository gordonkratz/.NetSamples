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
            set => SetProperty(ref _state, value);
        }
    }
}
