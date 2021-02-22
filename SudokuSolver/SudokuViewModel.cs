using FrontendFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reactive.Linq;
using Ui.Utilities;
using System.Reactive.Concurrency;
using Utilities.Threading;

namespace SudokuSolver
{
    public class SudokuViewModel : ViewModelBase
    {
        private readonly SudokuViewModelItem[] _backingArray;
        private readonly IWpfThread _thread;
        private readonly ISudokuSolver _solver;
        private bool _isSolving;

        public SudokuViewModel(IWpfThread thread, ISudokuSolver solver)
        {
            SolveAnimatedCommand = new RelayCommand(StartSolvingAnimated);
            SolveCommand = new RelayCommand(Solve);
            ResetCommand = new RelayCommand(Reset);
            _backingArray = new SudokuViewModelItem[81];
            for(int i = 0; i < _backingArray.Length; i++)
            {
                Cells.Add(_backingArray[i] = new SudokuViewModelItem());
            }

            _thread = thread;
            _solver = solver;
        }

        private void Solve()
        {
            var solution = _solver.Solve(_backingArray.Select(item => item.Value).ToArray());
            for(int i = 0; i < solution.Length; i++)
            {
                _backingArray[i].Value = solution[i];
            }
        }

        private void Reset()
        {
            foreach(var item in _backingArray)
            {
                item.Value = 0;
            }
            IsSolving = false;
        }

        public ICommand SolveAnimatedCommand { get; } 
        public ICommand SolveCommand { get; }
        public ICommand ResetCommand { get; }

        public bool IsSolving 
        { 
            get => _isSolving;
            set => SetProperty(ref _isSolving, value);
        }

        public void StartSolvingAnimated()
        {
            IsSolving = true;
            var observable = _solver.GenerateSolvingSteps(_backingArray.Select(item => item.Value).ToArray())
                .ToObservable(Scheduler.Default);
            observable.Pace(TimeSpan.FromMilliseconds(1))
                .Subscribe(_thread.Wrap<SolveStep>(ApplyIteration), 
                    _thread.Wrap<Exception>(e => Completed()),
                    _thread.Wrap(Completed));
        }

        private void Completed()
        {
            IsSolving = false;
        }

        private void ApplyIteration(SolveStep step)
        {
            _backingArray[step.Index].Value = step.Value;
        }

        public ObservableCollection<SudokuViewModelItem> Cells { get; } = new ObservableCollection<SudokuViewModelItem>();
        
    }

    public class SudokuViewModelItem : ViewModelBase
    {
        private int _value;

        public SudokuViewModelItem()
        {
            Click = new RelayCommand(Increment);
        }

        private void Increment()
        {
            Value = Value % 10 + 1;
        }

        public ICommand Click { get; }

        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
