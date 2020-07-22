using System;

namespace TicTacToe
{
    interface IComputerLogic<T> where T : ITicTacToeItem
    {
        bool TryMakeComputerChoice(T[,] board, TicTacToeState nextMover, out T selection);
    }

    public class RandomLogic<T> : IComputerLogic<T> where T : ITicTacToeItem
    {
        private Random _rand = new Random();
        public bool TryMakeComputerChoice(T[,] board, TicTacToeState nextMover, out T selection)
        {
            foreach(var item in board)
            {
                if(item.State == TicTacToeState.None)
                {
                    selection = item;
                    return true;
                }
            }
            selection = default;
            return false;
        }
    }
}
