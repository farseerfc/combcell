using CombCell.DSAlgo;

namespace CombCell
{
    /// <summary>
    /// Used in radio button to choose algorithm
    /// </summary>
    public class AlgoShower
    {
        private PathAlgorithm<Pair<int>> algo;

        public PathAlgorithm<Pair<int>> Algorithm
        {
            get { return algo; }
        }

        public override string ToString()
        {
            if (Algorithm == null) return "";
            return Algorithm.Name;
        }
        public AlgoShower(PathAlgorithm<Pair<int>> algo)
        {
            this.algo = algo;
        }
    }
}
