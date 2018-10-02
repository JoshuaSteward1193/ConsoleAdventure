using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Goblin : Enemy
    {
        public Goblin(string name, int hp, Map map) : base(name, "goblin", 'g', ConsoleColor.Red, hp, map)
        {
            
        }
    }
}
