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
        private char icon;
        public char Icon
        {
            get
            {
                if (Resident == null)
                {
                    return icon;
                }
                else
                {
                    return Resident.Icon;
                }
            }
            set
            {
                icon = value;
            }
        }
        private ConsoleColor fColor;
        public ConsoleColor FColor
        {
            get
            {
                if(Resident == null)
                {
                    return fColor;
                }
                else
                {
                    return Resident.Color;
                }
            }
            set
            {
                fColor = value;
            }
        }
        public ConsoleColor BColor { get; set; }
        public bool Passable { get; set; }
        public string Terrain { get; set; }

        private Character resident;
        public Character Resident
        {
            get
            {
                return resident;
            }
            set
            {
                resident = value;
            }
        }

        public Tile(char ic, int yv, int xv)
        {
            Icon = ic;
            Coord = new Coordinate(yv, xv);
            BColor = ConsoleColor.Black;
            switch (ic)
            {
                case '@':
                    Terrain = "Map Border";
                    FColor = ConsoleColor.Magenta;                    
                    Passable = false;
                    break;
                case ',':
                    Terrain = "Grassy Plains";
                    FColor = ConsoleColor.Green;                    
                    Passable = true;                    
                    break;                
                case 'X':
                    Terrain = "Stone Wall";
                    FColor = ConsoleColor.Gray;
                    Passable = false;
                    break;
                case '=':
                    Terrain = "Wooden Structure";
                    FColor = ConsoleColor.DarkYellow;
                    Passable = true;
                    break;
                case '#':
                    Terrain = "Forest";
                    FColor = ConsoleColor.DarkGreen;
                    Passable = true;
                    break;  
                    
            }
        }

        public void MoveCharacterTo(Tile oldPos, Character character)
        {
            Resident = character;
            oldPos.Resident = null;
            character.Position = Coord;
        }
        public void SpawnCharacter(Character character)
        {
            Resident = character;
            character.Position = Coord;
        }
    }
}
