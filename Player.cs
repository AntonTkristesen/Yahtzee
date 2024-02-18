using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class Player
    {
        public string Name { get; set; }
        public Scorecard Scorecard { get; private set; }

        public Player(string name)
        {
            Name = name;
            Scorecard = new Scorecard();
        }
    }
}
