namespace P09PathsInLabyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        private static char[,] lab;
        private static readonly Stack<char> Movements = new Stack<char>();

        public static void Main()
        {
            lab = ReadLabyrinth();
            FindPaths(0, 0, 'S');
        }

        private static void FindPaths(int row, int col, char direction)
        {
            if (!IsValidMove(row, col))
            {
                return;
            }

            Movements.Push(direction);
            if (lab[row, col] == 'e')
            {
                PrintPath();
            }
            else if (lab[row, col] == '-')
            {
                Mark(row, col);
                FindPaths(row, col + 1, 'R');
                FindPaths(row + 1, col, 'D');
                FindPaths(row, col - 1, 'L');
                FindPaths(row - 1, col, 'U');
                Unmark(row, col);
            }

            Movements.Pop();
        }

        private static void Unmark(int row, int col)
        {
            lab[row, col] = '-';
        }

        private static void Mark(int row, int col)
        {
            lab[row, col] = 'v';
        }

        private static void PrintPath()
        {
            Console.WriteLine(string.Join(string.Empty, string.Join(string.Empty, Movements).Reverse()).Remove(0, 1));
        }

        private static bool IsValidMove(int row, int col)
        {
            return row > -1 &&
                   row < lab.GetLength(0) &&
                   col > -1 &&
                   col < lab.GetLength(1) &&
                   lab[row, col] != '*';
        }

        private static char[,] ReadLabyrinth()
        {
            // int rows = int.Parse(Console.ReadLine());
            // int cols = int.Parse(Console.ReadLine());
            var matrix = new[,]
            {
                { '-', '*', '*', '-', 'e' },
                { '-', '-', '-', '-', '-' },
                { '*', '*', '*', '*', '*' }
            };
            // var matrix = new char[rows, cols];
            // for (int i = 0; i < rows; i++)
            // {
            //     var charactersInRow = Console.ReadLine().ToCharArray();
            //     for (int j = 0; j < cols; j++)
            //     {
            //         matrix[i, j] = charactersInRow[j];
            //     }
            // }

            return matrix;
        }
    }
}