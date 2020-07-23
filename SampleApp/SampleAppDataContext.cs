using SampleApp.BindingSamples;
using TicTacToe;

namespace SampleApp
{
    public class SampleAppDataContext
    {
        public BindingSamplesViewModel BindingSampleViewModel { get; } = new BindingSamplesViewModel();

        public TicTacToeViewModel TicTacToeViewModel { get; } = new TicTacToeViewModel(new NaiveChecker<TicTacToeViewModelItem>(), new RandomLogic<TicTacToeViewModelItem>());
    }
}
