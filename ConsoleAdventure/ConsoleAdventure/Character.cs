using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Character
    {
        public string Name { get; set; }
        public char Icon { get; set; }
        private int health;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
                if (health < 0)
                {
                    health = 0;
                }
            }
        }
        public Coordinate Position { get; set; }
        public ConsoleColor Color { get; set; }

        public Character(string name, char ico, ConsoleColor col, int hp)
        {
            Name = name;
            Icon = ico;
            Health = hp;            
            Color = col;
        }
        

    }
}
