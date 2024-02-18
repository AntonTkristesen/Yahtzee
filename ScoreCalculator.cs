using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class ScoreCalculator
    {
        private Dictionary<string, Func<List<int>, int>> scoreFunctions;

        public ScoreCalculator()
        {
            scoreFunctions = new Dictionary<string, Func<List<int>, int>>
        {
            { "1'ere", dice => dice.Sum(d => d == 1 ? 1 : 0) },
            { "2'ere", dice => dice.Sum(d => d == 2 ? 2 : 0) },
            { "3'ere", dice => dice.Sum(d => d == 3 ? 3 : 0) },
            { "4'ere", dice => dice.Sum(d => d == 4 ? 4 : 0) },
            { "5'ere", dice => dice.Sum(d => d == 5 ? 5 : 0) },
            { "6'ere", dice => dice.Sum(d => d == 6 ? 6 : 0) },
            { "1 par", dice => CalculatePair(dice) },
            { "2 par", dice => CalculateTwoPairs(dice) },
            { "3 ens", dice => CalculateOfAKind(dice, 3) },
            { "4 ens", dice => CalculateOfAKind(dice, 4) },
            { "Lille straight", dice => CalculateSmallStraight(dice) },
            { "Stor straight", dice => CalculateLargeStraight(dice) },
            { "Hus", dice => CalculateFullHouse(dice) },
            { "Chance", dice => dice.Sum() },
            { "YATZY", dice => CalculateYahtzee(dice) }
        };
        }

        public int CalculateScore(string category, List<int> dice)
        {
            if (scoreFunctions.ContainsKey(category))
            {
                return scoreFunctions[category](dice);
            }
            return 0;
        }

        private int CalculatePair(List<int> dice)
        {
            var groups = dice.GroupBy(x => x).Where(g => g.Count() >= 2);
            return groups.Any() ? groups.Max(g => g.Key) * 2 : 0;
        }

        private int CalculateTwoPairs(List<int> dice)
        {
            var pairs = dice.GroupBy(x => x).Where(g => g.Count() >= 2).Select(g => g.Key).ToList();
            return pairs.Count >= 2 ? pairs.OrderByDescending(x => x).Take(2).Sum(x => x * 2) : 0;
        }

        private int CalculateOfAKind(List<int> dice, int count)
        {
            var group = dice.GroupBy(x => x).FirstOrDefault(g => g.Count() >= count);
            return group != null ? group.Key * count : 0;
        }

        private int CalculateSmallStraight(List<int> dice)
        {
            var smallStraight = new HashSet<int> { 1, 2, 3, 4, 5 };
            return smallStraight.IsSubsetOf(new HashSet<int>(dice)) ? 15 : 0;
        }

        private int CalculateLargeStraight(List<int> dice)
        {
            var largeStraight = new HashSet<int> { 2, 3, 4, 5, 6 };
            return largeStraight.IsSubsetOf(new HashSet<int>(dice)) ? 20 : 0;
        }

        private int CalculateFullHouse(List<int> dice)
        {
            bool isTwo = dice.GroupBy(x => x).Any(g => g.Count() == 2);
            bool isThree = dice.GroupBy(x => x).Any(g => g.Count() == 3);
            return (isTwo && isThree) ? dice.Sum() : 0;
        }

        private int CalculateYahtzee(List<int> dice)
        {
            return dice.All(d => d == dice[0]) ? 50 : 0;
        }
    }
}

