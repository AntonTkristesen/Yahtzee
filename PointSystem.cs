using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee3;

namespace Yahtzee3
{
    public class PointSystem
    {
        private const int NUM_DICE = 5;
        private int[] dice = new int[NUM_DICE];

        public int ThreeOfAKind()
        {
            if (dice.GroupBy(x => x).Any(g => g.Count() >= 3))
                return dice.Sum();
            else
                return 0;
        }

        public int FourOfAKind()
        {
            if (dice.GroupBy(x => x).Any(g => g.Count() >= 4))
                return dice.Sum();
            else
                return 0;
        }

        public int FullHouse()
        {
            if (dice.GroupBy(x => x).Any(g => g.Count() == 3) &&
                dice.GroupBy(x => x).Any(g => g.Count() == 2))
                return 25;
            else
                return 0;
        }

        public int SmallStraight()
        {
            if (dice.Distinct().OrderBy(x => x).Select((value, index) => value - index).Distinct().Count() >= 4)
                return 30;
            else
                return 0;
        }

        public int LargeStraight()
        {
            if (dice.Distinct().OrderBy(x => x).Select((value, index) => value - index).Distinct().Count() == 5)
                return 40;
            else
                return 0;
        }

        public int Yahtzee()
        {
            if (dice.GroupBy(x => x).Any(g => g.Count() == 5))
                return 50;
            else
                return 0;
        }

        public int Chance()
        {
            return dice.Sum();
        }


        public int CountDiceValue(int value)
        {
            return value * dice.Count(d => d == value);
        }
    }
}
