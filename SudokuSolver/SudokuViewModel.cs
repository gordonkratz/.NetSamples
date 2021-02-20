using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Ui.Utilities;

namespace SudokuSolver
{
    public class SudokuViewModel : ViewModelBase
    {
        private readonly SudokuViewModelItem[] _backingArray;

        public SudokuViewModel()
        {
            SolveCommand = new RelayCommand(SolvePuzzle);
            _backingArray = new SudokuViewModelItem[81];
            for(int i = 0; i < _backingArray.Length; i++)
            {
                Cells.Add(_backingArray[i] = new SudokuViewModelItem());
            }
        }

        public ICommand SolveCommand { get; } 

        public  void SolvePuzzle()
        {
            var given = _backingArray.Select(item => item.Value != 0).ToArray();

            var index = 0;
            while (index < _backingArray.Length) 
            {
                if (given[index])
                    index++;
                else
                {
                    _backingArray[index].Value++;
                    while (!CheckValidation(index))
                        _backingArray[index].Value++;

                    if (_backingArray[index].Value > 9)
                    {
                        _backingArray[index].Value = 0;
                        index--;
                        while (given[index])
                            index--;
                    }
                    else
                        index++;
                }
            }
        }

        private bool CheckValidation(int index)
        {
            var currentNumber = _backingArray[index].Value;

            //check row
            var start = index / 9 * 9;
            for (int r = 0; r < 9; r++) {
                var i = start + r;
                if (i == index)
                    continue;
                if (_backingArray[i].Value == currentNumber)
                    return false;
            }

            // check column
            start = index % 9;
            for (int r = 0; r < 9; r++)
            {
                var i = 9 * r + start;
                if (i == index)
                    continue;
                if (_backingArray[i].Value == currentNumber)
                    return false; 
            }

            // check box
            var leftMost = index / 3 * 3;
            var topCorner = (leftMost % 9) + (leftMost / 27 * 27);
            for(int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    var i = 9 * a + b + topCorner;
                    if (i == index)
                        continue;
                    if (_backingArray[i].Value == currentNumber)
                        return false;
                }
            }
            return true;
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
