using SampleApp.BindingSamples;
using SampleApp.TicTacToe;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp
{
    public class SampleAppDataContext
    {
        public BindingSamplesViewModel BindingSampleViewModel { get; } = new BindingSamplesViewModel();

        public TicTacToeViewModel TicTacToeViewModel { get; } = new TicTacToeViewModel();
    }
}
