using NUnit.Framework;
using NUnit.Framework.Constraints;
using SampleApp.TicTacToe;
using System;

namespace TicTacToeTests
{
    public class Tests
    {
        ICheckTicTacToeEnd<Cell> _checker;

        [SetUp]
        public void Setup()
        {
            _checker = new NaiveChecker<Cell>();
        }

        [Test]
        public void ShouldSetUp()
        {

        }

        [Test]
        public void ShouldSeeVerticalRows()
        {
            TestCase(new[] {
                TicTacToeState.O, TicTacToeState.X, TicTacToeState.O,
                TicTacToeState.O, TicTacToeState.X, TicTacToeState.None,
                TicTacToeState.O, TicTacToeState.None, TicTacToeState.None},
                3, true, TicTacToeState.O);

            TestCase(new[] {
                TicTacToeState.None, TicTacToeState.X, TicTacToeState.O,
                TicTacToeState.None, TicTacToeState.X, TicTacToeState.O,
                TicTacToeState.None, TicTacToeState.None, TicTacToeState.O},
                3, true, TicTacToeState.O);

            TestCase(new[] {
                TicTacToeState.X, TicTacToeState.O, TicTacToeState.None,
                TicTacToeState.X, TicTacToeState.O, TicTacToeState.None,
                TicTacToeState.None, TicTacToeState.O, TicTacToeState.None},
                3, true, TicTacToeState.O);
        }

        private void TestCase(TicTacToeState[] states, int rank, bool endExpected, TicTacToeState resultExpected)
        {
            var board = new Cell[rank, rank];
            var count = 0;
            for(int i = 0; i < rank; i++)
            {
                for(int j = 0; j < rank; j++)
                {
                    board[i, j] = new Cell(states[count]);
                }
            }

            (var isWinner, var winner) = _checker.IsGameOver(board);
            Assert.AreEqual(endExpected, isWinner);
            Assert.AreEqual(TicTacToeState.O, winner);
        }
    }

    public class Cell : ITicTacToeItem
    {
        public Cell(TicTacToeState state)
        {
            State = state;
        }

        public TicTacToeState State { get; }
    }
}