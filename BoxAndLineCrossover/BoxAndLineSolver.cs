using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace BoxAndLineCrossover
{
    public class BoxAndLineSolver
    {
        private static int MAX_ITERATIONS = 200;
        private static int MAX_PARENTS = 7;
        private static Random _random = new Random();
        private string[][] _citiesInfo;
        private double _alpha = 0;

        public BoxAndLineSolver(string[][] citiesInformation, double alpha)
        {
            _citiesInfo = citiesInformation;
            _alpha = alpha;
        }

        public SolutionRepresentation Solve()
        {
            var bestSolution = new SolutionRepresentation();
            var listOfSolutions = GenerateParents();
            int i = 0;

            while (i < MAX_ITERATIONS)
            {
                for (int j = 0; j < MAX_PARENTS; j++)
                {
                    var currentParents = listOfSolutions.Shuffle(_random).Take(2).ToList();
                    var newIndividual = Crossover(currentParents.First(), currentParents.Last());

                    listOfSolutions.Add(newIndividual);

                    if (newIndividual.Sum < bestSolution.Sum)
                    {
                        bestSolution.Cities = newIndividual.Cities;
                        bestSolution.Sum = newIndividual.Sum;
                    }
                }

                listOfSolutions = listOfSolutions.Shuffle(_random).ToList();
                i++;
            }

            return bestSolution;
        }

        private List<SolutionRepresentation> GenerateParents()
        {
            var parents = new List<SolutionRepresentation>();

            for (int i = 0; i < MAX_PARENTS; i++)
            {
                var parent = new SolutionRepresentation();

                var cities = Enumerable.Range(0, 15).ToArray();

                cities = cities.Shuffle(_random).ToArray<int>();

                parent.Cities = cities;
                parent.Sum = GetTotalDistance(cities);

                parents.Add(parent);
            }

            return parents;
        }

        private int GetTotalDistance(int[] cities)
        {
            var sum = 0;

            //Sum distances between two adjacent cities form 0 to n-1
            for (int i = 0; i < cities.Length - 1; i++)
            {
                sum += GetDistance(cities[i], cities[i + 1]);
            }

            // Add to that sum distance of last item and the  first one
            // From end c# 8
            sum += GetDistance(cities[^1], cities[0]);

            return sum;
        }

        private SolutionRepresentation Crossover(SolutionRepresentation firstParent,
            SolutionRepresentation secondParent)
        {
            bool isValidSolution = false;
            var cities = new List<int>();

            while (!isValidSolution)
            {
                cities = new List<int>();
                double u = _random.GenerateDouble(0, 1 + 2 * _alpha);

                for (int i = 0; i < firstParent.Cities.Count(); i++)
                {
                    double solution = (firstParent.Cities[i] - _alpha) +
                                      u * (secondParent.Cities[i] - firstParent.Cities[i]);

                    if (solution < 0)
                    {
                        cities.Add(0);
                    }
                    else if (solution > 14)
                    {
                        cities.Add(14);
                    }
                    else
                    {
                        cities.Add((int) solution);
                    }
                }

                isValidSolution = cities.Distinct().Count() == firstParent.Cities.Count();
            }

            var response = new SolutionRepresentation
            {
                Cities = cities.ToArray(),
                Sum = EvaluateSolution(cities)
            };

            return response;
        }

        private int EvaluateSolution(List<int> currentSolution)
        {
            int evaluation = 0;

            for (int i = 0; i < currentSolution.Count; i++)
            {
                if (i != currentSolution.Count - 1)
                {
                    evaluation += GetDistance(currentSolution[i], currentSolution[i + 1]);
                }
                else
                {
                    evaluation += GetDistance(currentSolution[i], currentSolution[0]);
                }
            }

            return evaluation;
        }

        private int GetDistance(int city1, int city2)
        {
            return int.Parse(_citiesInfo[city1][city2]);
        }
    }
}