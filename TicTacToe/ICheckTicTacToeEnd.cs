using Utilities;

namespace TicTacToe
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
            var height = board.GetUpperBound(0);

            var winner = CheckColumns(board, height);
            if (winner != TicTacToeState.None)
                return (true, winner);

            winner = CheckRows(board, height);
            if (winner != TicTacToeState.None)
                return (true, winner);

            winner = CheckBackslash(board, height);
            if (winner != TicTacToeState.None)
                return (true, winner);

            winner = CheckForwardslash(board, height);
            if (winner != TicTacToeState.None)
                return (true, winner);

            return (board.None(b => b.State == TicTacToeState.None), TicTacToeState.None);
        }

        private TicTacToeState CheckForwardslash(T[,] board, int height)
        {
            var slashState = board[0, height].State;
            if (slashState != TicTacToeState.None)
            {
                for (int i = 1; i <= height; i++)
                {
                    if (board[i, height - i].State != slashState)
                        break;
                    else if (i == height)
                        return slashState;
                }
            }

            return TicTacToeState.None;
        }

        private TicTacToeState CheckBackslash(T[,] board, int height)
        {
            var slashState = board[0, 0].State;
            if (slashState != TicTacToeState.None)
            {
                for (int i = 1; i <= height; i++)
                {
                    if (board[i, i].State != slashState)
                        break;
                    else if (i == height)
                        return slashState;
                }
            }
            return TicTacToeState.None;
        }

        private TicTacToeState CheckRows(T[,] board, int height)
        {
            for (int j = 0; j <= height; j++)
            {
                var rowState = board[0, j].State;
                if (rowState == TicTacToeState.None)
                    continue;

                for (int i = 1; i <= height; i++)
                {
                    if (rowState != board[i, j].State)
                        break;
                    else if (i == height)
                        return rowState;
                }
            }

            return TicTacToeState.None;
        }

        private TicTacToeState CheckColumns(T[,] board, int height)
        {
            for (int i = 0; i <= height; i++)
            {
                var columnState = board[i, 0].State;
                if (columnState == TicTacToeState.None)
                    continue;

                for (int j = 1; j <= height; j++)
                {
                    if (columnState != board[i, j].State)
                        break;
                    else if (j == height)
                        return columnState;
                }
            }

            return TicTacToeState.None;
        }
    }
}
