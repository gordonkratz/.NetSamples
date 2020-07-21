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
                'O', 'X', 'O',
                'O', 'X', ' ',
                'O', ' ', ' '},
                3, true, TicTacToeState.O);

            TestCase(new[] {
                ' ', 'X', 'O',
                ' ', 'X', 'O',
                ' ', ' ', 'O'},
                3, true, TicTacToeState.O);

            TestCase(new[] {
                'X', 'O', ' ',
                'X', 'O', ' ',
                ' ', 'O', ' '},
                3, true, TicTacToeState.O);

            TestCase(new[] {
                'x', 'o', ' ',
                'x', 'o', ' ',
                'x', ' ', ' '},
                3, true, TicTacToeState.X);

            TestCase(new[] {
                ' ', 'o', 'x',
                ' ', 'o', 'x',
                ' ', ' ', 'x'},
                3, true, TicTacToeState.X);

            TestCase(new[] {
                'o', 'x', ' ',
                'o', 'x', ' ',
                ' ', 'x', ' '},
                3, true, TicTacToeState.X);
        }

        [Test]
        public void ShouldSeeHorizontals()
        {
            TestCase(new[]
            {
                'o', 'o', 'o',
                'x', 'x', ' ',
                ' ', ' ', ' '
            }, 3, true, TicTacToeState.O);

            TestCase(new[]
            {
                'x', 'x', ' ',
                'o', 'o', 'o',
                ' ', ' ', ' '
            }, 3, true, TicTacToeState.O);


            TestCase(new[]
            {
                'x', 'x', ' ',
                ' ', ' ', ' ',
                'o', 'o', 'o',
            }, 3, true, TicTacToeState.O);

            TestCase(new[]
            {
                'o', 'o', ' ',
                'x', 'x', 'x',
                ' ', ' ', ' '
            }, 3, true, TicTacToeState.X);

            TestCase(new[]
            {
                'x', 'x', 'x',
                'o', 'o', ' ',
                ' ', ' ', ' '
            }, 3, true, TicTacToeState.X);

            TestCase(new[]
            {
                'o', 'o', ' ',
                ' ', ' ', ' ',
                'x', 'x', 'x',
            }, 3, true, TicTacToeState.X);
        }

        [Test]
        public void ShouldSeeSlashes()
        {
            TestCase(new[]
            {
                'o', ' ', ' ',
                ' ', 'o', ' ',
                'x', 'x', 'o',
            }, 3, true, TicTacToeState.O);

            TestCase(new[]
            {
                'x', ' ', ' ',
                ' ', 'x', ' ',
                'o', 'o', 'x',
            }, 3, true, TicTacToeState.X);

            TestCase(new[]
            {
                'x', ' ', 'o',
                ' ', 'o', ' ',
                'o', ' ', 'x',
            }, 3, true, TicTacToeState.O);

            TestCase(new[]
            {
                'o', ' ', 'X',
                ' ', 'x', ' ',
                'x', 'o', ' ',
            }, 3, true, TicTacToeState.X);
        }

        [Test]
        public void ShouldSeeUnfinishedGames()
        {

            TestCase(new[]
            {
                'o', ' ', ' ',
                ' ', 'x', ' ',
                'x', 'o', ' ',
            }, 3, false, TicTacToeState.None);

            TestCase(new[]
            {
                'o', ' ', ' ',
                ' ', 'x', ' ',
                'x', ' ', 'o',
            }, 3, false, TicTacToeState.None);

            TestCase(new[]
            {
                'o', ' ', ' ',
                ' ', 'x', ' ',
                'x', ' ', ' ',
            }, 3, false, TicTacToeState.None);

            TestCase(new[]
            {
                'o', ' ', ' ',
                ' ', ' ', ' ',
                'x', ' ', ' ',
            }, 3, false, TicTacToeState.None);
        }

        [Test]
        public void ShouldSeeStalemate()
        {
            TestCase(new[]
            {
                'o', 'x', 'x',
                'x', 'o', 'o',
                'x', 'o', 'x',
            }, 3, true, TicTacToeState.None);
        }

        [Test]
        public void HigherOrderBoards()
        {
            TestCase(new[]
            {
                'o', 'x', 'x', ' ',
                'o', 'o', ' ', ' ',
                ' ', ' ', 'o', ' ',
                ' ', ' ', 'x', ' ',
            }, 4, false, TicTacToeState.None);
        }

        private void TestCase(char[] states, int dimension, bool endExpected, TicTacToeState resultExpected)
        {
            var board = new Cell[dimension, dimension];
            var count = 0;
            for(int i = 0; i < dimension; i++)
            {
                for(int j = 0; j < dimension; j++)
                {
                    board[j, i] = new Cell(ParseState(states[count++]));
                }
            }

            (var isWinner, var winner) = _checker.IsGameOver(board);
            Assert.AreEqual(endExpected, isWinner);
            Assert.AreEqual(resultExpected, winner);
        }

        private TicTacToeState ParseState(char s)
        {
            switch (s)
            {
                case 'o':
                case 'O':
                    return TicTacToeState.O;
                case 'x':
                case 'X':
                    return TicTacToeState.X;
                default:
                    return TicTacToeState.None;
            }
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