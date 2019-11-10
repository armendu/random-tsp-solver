using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BoxAndLineCrossover
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("./input/p01_d.txt").ToList();

            var citiesInformation = new string[lines.Count][];
            // Initialize arrays
            for (int i = 0; i < citiesInformation.Length; i++)
            {
                citiesInformation[i] = new string[lines.Count];
            }

            for (int i = 0; i < lines.Count; i++)
            {
                List<string> strippedLines = lines[i].Split(' ').Where(l => l != "").ToList();
                for (int j = 0; j < strippedLines.Count; j++)
                {
                    citiesInformation[i][j] = strippedLines[j];
                }
            }

            Console.Write("Enter alpha (0, 0.1, or 0.25): ");
            string input = Console.ReadLine();

            bool parseResult = double.TryParse(input, out double alpha);

            if (!parseResult)
                Environment.Exit(0);

            var solver = new BoxAndLineSolver(citiesInformation, alpha);
            var result = solver.Solve();

            Console.WriteLine($"The sum of the result is {result.Sum}");
            Console.WriteLine("The array of cities is:");
            foreach (var item in result.Cities)
            {
                Console.Write($"{item},");
            }
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
