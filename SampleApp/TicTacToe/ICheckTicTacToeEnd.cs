using SampleApp.Core;

namespace SampleApp.TicTacToe
{
    public interface ITicTacToeItem
    {
        TicTacToeState State { get; }
    }

    public interface ICheckTicTacToeEnd<T> where T : ITicTacToeItem
    {
        (bool, TicTacToeState) IsGameOver(T[,] board);
    }

    public class NaiveChecker<T> : ICheckTicTacToeEnd<T> where T : ITicTacToeItem
    {
        public (bool, TicTacToeState) IsGameOver(T[,] board)
        {
            var dimension = board.Rank;

            var winner = CheckColumns(board);
            if (winner != TicTacToeState.None)
                return (true, winner);

            winner = CheckRows(board);
            if (winner != TicTacToeState.None)
                return (true, winner);

            winner = CheckBackslash(board);
            if (winner != TicTacToeState.None)
                return (true, winner);

            winner = CheckForwardslash(board);
            if (winner != TicTacToeState.None)
                return (true, winner);

            return (board.None(b => b.State == TicTacToeState.None), TicTacToeState.None);
        }

        private TicTacToeState CheckForwardslash(T[,] board)
        {
            var slashState = board[0, board.Rank - 1].State;
            if (slashState != TicTacToeState.None)
            {
                for (int i = 1; i < board.Rank; i++)
                {
                    if (board[i, board.Rank - 1 - i].State != slashState)
                        break;
                    else if (i == board.Rank - 1)
                        return slashState;
                }
            }

            return TicTacToeState.None;
        }

        private TicTacToeState CheckBackslash(T[,] board)
        {
            var slashState = board[0, 0].State;
            if (slashState != TicTacToeState.None)
            {
                for (int i = 1; i < board.Rank; i++)
                {
                    if (board[i, i].State != slashState)
                        break;
                    else if (i == board.Rank - 1)
                        return slashState;
                }
            }
            return TicTacToeState.None;
        }

        private TicTacToeState CheckRows(T[,] board)
        {
            for (int j = 0; j < board.Rank; j++)
            {
                var rowState = board[0, j].State;
                if (rowState == TicTacToeState.None)
                    continue;

                for (int i = 1; i < board.Rank; i++)
                {
                    if (rowState != board[i, j].State)
                        break;
                    else if (i == board.Rank - 1)
                        return rowState;
                }
            }

            return TicTacToeState.None;
        }

        private TicTacToeState CheckColumns(T[,] board)
        {
            for (int i = 0; i < board.Rank; i++)
            {
                var columnState = board[i, 0].State;
                if (columnState == TicTacToeState.None)
                    continue;

                for (int j = 1; j < board.Rank; j++)
                {
                    if (columnState != board[i, j].State)
                        break;
                    else if (j == board.Rank - 1)
                        return columnState;
                }
            }

            return TicTacToeState.None;
        }
    }
}
