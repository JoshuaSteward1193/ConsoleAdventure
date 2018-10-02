using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Enemy : Character
    {
        public string Type { get; set; }
        public Enemy(string name, string type, char ico, ConsoleColor col, int hp, Map map) : base(name, ico, col, hp, map)
        {
            Type = type;
        }
    }
}
