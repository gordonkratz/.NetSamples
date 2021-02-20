using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Ui.Utilities;

namespace SudokuSolver
{
    public class SudokuViewModel : ViewModelBase
    {
        private readonly SudokuViewModelItem[] _backingArray;

        public SudokuViewModel()
        {
            _backingArray = new SudokuViewModelItem[81];
            for(int i = 0; i < _backingArray.Length; i++)
            {
                Cells.Add(_backingArray[i] = new SudokuViewModelItem());
            }
        }

        public ObservableCollection<SudokuViewModelItem> Cells { get; } = new ObservableCollection<SudokuViewModelItem>();
        
    }

    public class SudokuViewModelItem : ViewModelBase
    {
        private int _value;

        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
