using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class GoblinEnemy : Enemy
    {
        public GoblinEnemy(string name, int hp, Map map) : base(name,4, 10, "Goblin", 'g', ConsoleColor.Yellow, hp, map)
        {
        }
    }
}
 