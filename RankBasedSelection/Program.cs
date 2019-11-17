using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace RankBasedSelection
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            int[,] distances =
            {
                {0, 5, 7, 4, 15},
                {5, 0, 3, 4, 10},
                {7, 3, 0, 2, 7},
                {4, 4, 2, 0, 9},
                {15, 10, 7, 9, 0}
            };

            List<int> list = new List<int> {0, 1, 2, 3, 4};

            double rankSum = 0;

            Console.WriteLine("Alpha parameter: ");
            string input = Console.ReadLine();

            bool parseResult = double.TryParse(input, out double alpha);

            if (!parseResult)
            {
                Console.WriteLine("Please enter a valid value for alpha");
                Environment.Exit(0);
            }

            Console.WriteLine("Input popsize: ");
            input = Console.ReadLine();

            parseResult = int.TryParse(input, out int popSize);

            if (!parseResult)
            {
                Console.WriteLine("Please enter a valid value for popsize");
                Environment.Exit(0);
            }

            for (int i = 1; i <= popSize; i++)
            {
                rankSum += Math.Pow(i, alpha);
            }

            Console.WriteLine("Rank sum: " + rankSum);

            var solutions = new List<List<int>>();
            for (int i = 0; i < popSize; i++)
            {
                solutions.Add(list.Shuffle(rnd).ToList());
            }

            SolutionMatrix(solutions);

            var fitness = new List<RankSelection>();

            for (int i = 0; i < solutions.Count; i++)
            {
                var solutionDistances = Enumerable.Range(0, 4).Select(x =>
                    GetDistance(distances, solutions[i][x], solutions[i][x + 1]));

                fitness.Add(new RankSelection(solutionDistances.Sum(), i, 0.0, i));
            }

            fitness = fitness.OrderBy(obj => obj.Fitness).ToList();

            for (int i = 0; i < fitness.Count; i++)
            {
                fitness[i].Rank = popSize - i;
                fitness[i].Probability = Math.Pow(fitness[i].Rank, alpha) / rankSum;
                Console.WriteLine(fitness[i].ToString());
            }

            Console.ReadLine();
        }

        static void SolutionMatrix(List<List<int>> arr)
        {
            var rowCount = arr.Count;
            var colCount = arr.Max(l => l.Count);

            Console.WriteLine("Solutions");
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                    Console.Write($"{arr[row][col]}\t");
                Console.WriteLine();
            }
        }

        private static int GetDistance(int[,] distances, int firstElem, int secondElem)
        {
            return distances[firstElem, secondElem];
        }
    }

    
}