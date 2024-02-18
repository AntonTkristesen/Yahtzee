using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class Die
    {
        public int Value { get; private set; }

        public void Roll(Random random)
        {
            Value = random.Next(1, 7);
        }
    }
}
