using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Tile
    {
        public Coordinate Coord { get; set; }
        public char Icon { get; set; }
        public ConsoleColor FColor { get; set; }
        public ConsoleColor BColor { get; set; }
        public bool Passable { get; set; }
        public string Type { get; set; }

        public Tile(char ic, int yv, int xv)
        {
            Icon = ic;
            Coord = new Coordinate(yv, xv);
            BColor = ConsoleColor.Black;
            switch (ic)
            {
                case '@':
                    Type = "Map Border";
                    FColor = ConsoleColor.Magenta;                    
                    Passable = false;
                    break;
                case ',':
                    Type = "Grassy Plains";
                    FColor = ConsoleColor.Green;                    
                    Passable = true;                    
                    break;                
                case 'X':
                    Type = "Stone Wall";
                    FColor = ConsoleColor.Gray;
                    Passable = false;
                    break;
                case '=':
                    Type = "Wooden Structure";
                    FColor = ConsoleColor.DarkYellow;
                    Passable = true;
                    break;
                case '#':
                    Type = "Forest";
                    FColor = ConsoleColor.DarkGreen;
                    Passable = true;
                    break;  
                    
            }
        }
    }
}
