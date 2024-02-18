using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class Scorecard
    {
        public Dictionary<string, int> Scores { get; private set; }
        private Dictionary<string, int> yahtzeeCombinations;

        public Scorecard()
        {
            Scores = new Dictionary<string, int>();

            yahtzeeCombinations = new Dictionary<string, int>
        {
            {"1'ere", 5},
            {"2'ere", 10},
            {"3'ere", 15},
            {"4'ere", 20},
            {"5'ere", 25},
            {"6'ere", 30},
            // "SUM" is a calculated field, so it's not included as a static value
            {"Bonus", 50},
            {"1 par", 12},
            {"2 par", 22},
            {"3 ens", 18},
            {"4 ens", 24},
            {"Lille straight", 15},
            {"Stor straight", 20},
            {"Hus", 28},
            {"Chance", 30},
            {"YATZY", 50}
        };

            // Initialize all score categories with a default value, for example, -1 or 0.
            foreach (var combination in yahtzeeCombinations)
            {
                Scores.Add(combination.Key, -1);
            }
        }

        public void SetScore(string category, int score)
        {
            if (Scores.ContainsKey(category))
            {
                Scores[category] = score;
            }
            else
            {
                throw new KeyNotFoundException($"The category {category} does not exist in the scorecard.");
            }
        }

        public int CalculateTotalScore()
        {
            return Scores.Values.Where(score => score != -1).Sum();
        }
    }
}
