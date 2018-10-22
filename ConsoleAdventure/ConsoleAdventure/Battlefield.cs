using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Battlefield : Map
    {
        public Battlefield(string name, Tile[,] terrain, Coordinate spawn) : base(name, terrain, spawn)
        {
            
        }
    }
}
