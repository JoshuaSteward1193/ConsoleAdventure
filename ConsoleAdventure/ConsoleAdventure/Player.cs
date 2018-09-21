using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Player : Character
    {
        public Player(string name, char ico, ConsoleColor col, int hp) : base(name, ico, col, hp)
        {
        }
    }
}
