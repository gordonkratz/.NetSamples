using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
    public interface ISudokuSolver
    {
        int[] Solve(int[] givenNumbers);
        IEnumerable<SolveStep> GenerateSolvingSteps(int[] givenNumbers);
    }

    public struct SolveStep
    {
        public SolveStep(int index, int value)
        {
            Index = index;
            Value = value;
        }

        public int Index { get; }
        public int Value { get; }
    }

    internal class SudokuSolver : ISudokuSolver
    {
        public int[] Solve(int[] givenNumbers)
        {
            var solution = givenNumbers.ToArray();
            foreach(var step in GenerateSolvingSteps(givenNumbers))
            {
                solution[step.Index] = step.Value;
            }
            return solution;
        }

        public IEnumerable<SolveStep> GenerateSolvingSteps(int[] givenNumbers)
        {
            var index = 0;
            bool[] given = givenNumbers.Select(g => g != 0).ToArray();
            var copy = givenNumbers.Select(g => g).ToArray();
            var firstBlank = Array.IndexOf(given, false) - 1;

            while (index < givenNumbers.Length)
            {
                if (given[index])
                    index++;
                else
                {
                    yield return new SolveStep(index, ++copy[index]);
                    while (!CheckValidation(index, copy))
                        yield return new SolveStep(index, ++copy[index]);

                    if (copy[index] > 9)
                    {
                        yield return new SolveStep(index, copy[index] = 0);
                        index--;
                        while (given[index] && index > firstBlank)
                            index--;
                        if (index == firstBlank)
                        {
                            yield break;
                        }
                    }
                    else
                        index++;
                }
            }
        }
        private bool CheckValidation(int index, int[] currentBoard)
        {
            var currentNumber = currentBoard[index];

            //check row
            var start = index / 9 * 9;
            for (int r = 0; r < 9; r++)
            {
                var i = start + r;
                if (i == index)
                    continue;
                if (currentBoard[i] == currentNumber)
                    return false;
            }

            // check column
            start = index % 9;
            for (int r = 0; r < 9; r++)
            {
                var i = 9 * r + start;
                if (i == index)
                    continue;
                if (currentBoard[i] == currentNumber)
                    return false;
            }

            // check box
            var leftMost = index / 3 * 3;
            var topCorner = (leftMost % 9) + (leftMost / 27 * 27);
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    var i = 9 * a + b + topCorner;
                    if (i == index)
                        continue;
                    if (currentBoard[i] == currentNumber)
                        return false;
                }
            }
            return true;
        }
    }
}
