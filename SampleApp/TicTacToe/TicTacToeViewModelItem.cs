using SampleApp.Core;

namespace SampleApp.TicTacToe
{
    public class TicTacToeViewModelItem : ViewModelBase
    {

        private TicTacToeState _state;

        public TicTacToeViewModelItem(TicTacToeState state)
        {
            _state = state;
        }

        public TicTacToeState State { 
            get => _state;
            set => OnPropertyChanged(ref _state, value);
        }
    }
}
