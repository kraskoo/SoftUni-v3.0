namespace CubicRube
{
    using System;
    using System.Linq;

    public static class Startup
    {
        public static void Main()
        {
            int size = int.Parse(Console.ReadLine());
            bool[,,] cube = new bool[size, size, size];
            Func<int, int, bool> validateIndex = (i, ds) => i > -1 && i < ds;
            Func<bool[,,], int, int, int, bool> canTargetOnCell =
                (c, x, y, z) =>
                    validateIndex(x, size) &&
                    validateIndex(y, size) &&
                    validateIndex(z, size) &&
                    !c[x, y, z];
            long targetSum = 0;
            int successfulHits = 0;
            string hitOnTarget = Console.ReadLine();
            while (!hitOnTarget.Equals("Analyze"))
            {
                int[] targetArguments =
                    hitOnTarget
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                int targetXaxis = targetArguments[0];
                int targetYaxis = targetArguments[1];
                int targetZaxis = targetArguments[2];
                int targetDamage = targetArguments[3];
                if (canTargetOnCell(cube, targetXaxis, targetYaxis, targetZaxis) &&
                    targetDamage > 0)
                {
                    cube[targetXaxis, targetYaxis, targetZaxis] = true;
                    targetSum += targetDamage;
                    successfulHits++;
                }

                hitOnTarget = Console.ReadLine();
            }

            int sumOfNonHittingCells = size * size * size - successfulHits;
            Console.WriteLine(targetSum);
            Console.WriteLine(sumOfNonHittingCells);
        }
    }
}