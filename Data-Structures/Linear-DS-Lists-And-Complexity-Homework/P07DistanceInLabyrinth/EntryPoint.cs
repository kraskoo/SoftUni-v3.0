namespace P07DistanceInLabyrinth
{
    using System;
    using System.Collections.Generic;

    public class EntryPoint
    {
        public static void Main()
        {
            int fieldSize = int.Parse(Console.ReadLine());
            string[,] field = new string[fieldSize, fieldSize];
            int startX = -1;
            int startY = -1;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = line[j].ToString();
                    if (line[j] == '*')
                    {
                        startX = i;
                        startY = j;
                    }
                }
            }

            Console.WriteLine();
            FieldCell startField = new FieldCell(startX, startY);
            Queue<FieldCell> fieldCells = new Queue<FieldCell>();
            fieldCells.Enqueue(startField);
            while (fieldCells.Count > 0)
            {
                var currentCell = fieldCells.Dequeue();
                if (CheckNextMoveIndex(field, currentCell.X, currentCell.Y - 1))
                {
                    field[currentCell.X, currentCell.Y - 1] = (currentCell.Value + 1).ToString();
                    fieldCells.Enqueue(new FieldCell(currentCell.X, currentCell.Y - 1, currentCell.Value + 1));
                }

                if (CheckNextMoveIndex(field, currentCell.X + 1, currentCell.Y))
                {
                    field[currentCell.X + 1, currentCell.Y] = (currentCell.Value + 1).ToString();
                    fieldCells.Enqueue(new FieldCell(currentCell.X + 1, currentCell.Y, currentCell.Value + 1));
                }

                if (CheckNextMoveIndex(field, currentCell.X, currentCell.Y + 1))
                {
                    field[currentCell.X, currentCell.Y + 1] = (currentCell.Value + 1).ToString();
                    fieldCells.Enqueue(new FieldCell(currentCell.X, currentCell.Y + 1, currentCell.Value + 1));
                }

                if (CheckNextMoveIndex(field, currentCell.X - 1, currentCell.Y))
                {
                    field[currentCell.X - 1, currentCell.Y] = (currentCell.Value + 1).ToString();
                    fieldCells.Enqueue(new FieldCell(currentCell.X - 1, currentCell.Y, currentCell.Value + 1));
                }
            }

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == "0")
                    {
                        field[i, j] = "u";
                    }

                    Console.Write(field[i, j]);
                }

                Console.WriteLine();
            }
        }

        private static bool CheckNextMoveIndex(string[,] field, int x, int y)
        {
            return (x >= 0 &&
                x < field.GetLength(0) &&
                y >= 0 &&
                y < field.GetLength(1) &&
                field[x, y] == "0");
        }
    }

    public class FieldCell
    {
        public FieldCell(int x, int y, int value = 0)
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Value { get; set; }
    }
}