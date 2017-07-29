namespace P08QueensPuzzle
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EntryPoint
    {
        private const int Size = 8;
        private static HashSet<int> reservedRows;
        private static HashSet<int> reservedCols;
        private static HashSet<int> reservedLeftDiagonal;
        private static HashSet<int> reservedRightDiagonal;
        private static int solutionCounter;
        private static bool[,] table;
        private static StringBuilder outputBuilder;

        public static void Main()
        {
            Initialize();
            PutQueen(0);
            Print();
        }

        private static void Print()
        {
            Console.WriteLine(outputBuilder.ToString().Trim());
            Console.WriteLine($"Found {solutionCounter} solutions");
        }

        private static void Initialize()
        {
            solutionCounter = 0;
            outputBuilder = new StringBuilder();
            reservedRows = new HashSet<int>();
            reservedCols = new HashSet<int>();
            reservedLeftDiagonal = new HashSet<int>();
            reservedRightDiagonal = new HashSet<int>();
            table = new bool[Size, Size];
        }

        private static void PutQueen(int row)
        {
            if (row == Size)
            {
                AttachSolution();
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if (CanPlaceQueen(row, col))
                    {
                        MarkQueenPositions(row, col);
                        PutQueen(row + 1);
                        UnmarkQueenPositions(row, col);
                    }
                }
            }
        }

        private static void UnmarkQueenPositions(int row, int col)
        {
            table[row, col] = false;
            reservedRows.Remove(row);
            reservedCols.Remove(col);
            reservedLeftDiagonal.Remove(row - col);
            reservedRightDiagonal.Remove(col + row);
        }

        private static void MarkQueenPositions(int row, int col)
        {
            table[row, col] = true;
            reservedRows.Add(row);
            reservedCols.Add(col);
            reservedLeftDiagonal.Add(row - col);
            reservedRightDiagonal.Add(col + row);
        }

        private static bool CanPlaceQueen(int row, int col)
        {
            return !reservedRows.Contains(row) &&
                   !reservedCols.Contains(col) &&
                   !reservedLeftDiagonal.Contains(row - col) &&
                   !reservedRightDiagonal.Contains(col + row);
        }

        private static void AttachSolution()
        {
            solutionCounter++;
            for (int row = 0; row < Size; row++)
            {
                char[] rowCells = new char[Size];
                for (int col = 0; col < Size; col++)
                {
                    rowCells[col] = table[row, col] ? 'Q' : '-';
                }

                outputBuilder.AppendLine(string.Join(" ", rowCells));
            }

            outputBuilder.AppendLine();
        }
    }
}