using System.Linq;
using System;
using Shared;

namespace TSPSolver
{
    class TspSolver
    {
        public static int MAX_ITERATIONS = 50000;
        public static Random _random = new Random();
        private string[][] _citiesInfo;

        public TspSolver(string[][] citiesInformation)
        {
            _citiesInfo = citiesInformation;
        }

        private int[] GenerateRandomSolution()
        {
            var randomList = Enumerable.Range(0, 15).ToList();

            return randomList.Shuffle(_random).ToArray<int>();
        }

        private int GetDistance(int city1, int city2)
        {
            return int.Parse(_citiesInfo[city1][city2]);
        }

        public (int, int[]) Solve()
        {
            var sumResult = 1000;
            var solutionList = new int[_citiesInfo.Length];
            int[] solution = new int[_citiesInfo.Length];
            int i = 0;

            while (i < MAX_ITERATIONS)
            {
                solution = GenerateRandomSolution();

                var resultOfSolution = 0;

                for (int j = 0; j < solution.Length; j++)
                {
                    if (j != solution.Length - 1)
                        resultOfSolution += GetDistance(solution[j], solution[j + 1]);
                    else
                        resultOfSolution += GetDistance(solution[j], solution[0]);
                }

                if (resultOfSolution < sumResult)
                {
                    sumResult = resultOfSolution;
                    solutionList = solution;
                }
                else
                    i++;
            }

            return (sumResult, solutionList);
        }
    }
}
