namespace RankBasedSelection
{
    public class RankSelection
    {
        public int Fitness { get; set; }
        public int Rank { get; set; }
        public double Probability { get; set; }
        public int Solution { get; set; }

        public RankSelection(int fitness, int rank, double probability, int solution)
        {
            Fitness = fitness;
            Rank = rank;
            Probability = probability;
            Solution = solution;
        }

        public override string ToString()
        {
            return $"Fitness: {Fitness}, Rank: {Rank}, Probability: {Probability}, Solution: {Solution}";
        }
    }
}