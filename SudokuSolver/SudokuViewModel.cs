using FrontendFramework;
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
        private readonly IWpfThread _thread;
        private bool _isSolving;

        public SudokuViewModel(IWpfThread thread)
        {
            SolveCommand = new RelayCommand(StartSolving);
            ResetCommand = new RelayCommand(Reset);
            _backingArray = new SudokuViewModelItem[81];
            for(int i = 0; i < _backingArray.Length; i++)
            {
                Cells.Add(_backingArray[i] = new SudokuViewModelItem());
            }

            _thread = thread;
        }

        private void Reset()
        {
            for(int i =0; i < _backingArray.Length; i++)
            {
                _backingArray[i].Value = 0;
            }
        }

        public ICommand SolveCommand { get; } 
        public ICommand ResetCommand { get; }
        public bool IsSolving 
        { 
            get => _isSolving;
            set => SetProperty(ref _isSolving, value);
        }

        bool[] given;
        int firstBlank, index;

        public void StartSolving()
        {
            IsSolving = true;
            given = _backingArray.Select(item => item.Value != 0).ToArray();
            firstBlank = Array.IndexOf(given, false) - 1;
            index = 0;
            SolveIteration();
        }


        private void SolveIteration()
        {
            if (index >= _backingArray.Length)
            {
                IsSolving = false;
                return;
            }
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
                    while (given[index] && index > firstBlank)
                        index--;
                    if (index == firstBlank)
                    { 
                        for(int i = 0; i < given.Length; i++)
                        {
                            if (!given[i])
                                _backingArray[i].Value = 0;
                        }
                        IsSolving = false;
                        return;
                    }
                    }
                    else
                        index++;
                
            }
            _thread.Post(SolveIteration, true);
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
