using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class DiceCup
    {
        public List<Die> Dice { get; private set; }

        public DiceCup(int numberOfDice)
        {
            Dice = new List<Die>();
            for (int i = 0; i < numberOfDice; i++)
            {
                Dice.Add(new Die());
            }
        }

        public void RollAll(Random random)
        {
            foreach (var die in Dice)
            {
                die.Roll(random);
            }
        }
    }
}