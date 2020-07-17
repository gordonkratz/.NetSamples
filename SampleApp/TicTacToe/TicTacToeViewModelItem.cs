using SampleApp.Core;
using System;
using System.Windows.Input;

namespace SampleApp.TicTacToe
{
    public class TicTacToeViewModelItem : ViewModelBase
    {

        private TicTacToeState _state;

        public TicTacToeViewModelItem(TicTacToeState state)
        {
            _state = state;
            CellCommand = new RelayCommand(ClaimCell);
        }

        private void ClaimCell()
        {
            State = (TicTacToeState) (((int)State) % 3) + 1;
        }

        public TicTacToeState State { 
            get => _state;
            set => OnPropertyChanged(ref _state, value);
        }

        public ICommand CellCommand { get; } 


    }
}
