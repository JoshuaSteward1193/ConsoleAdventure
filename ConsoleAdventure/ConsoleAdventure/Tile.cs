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
                case 'b':
                    Icon = SpecialChars.Bush;
                    Terrain = "Bush";
                    FColor = ConsoleColor.DarkGreen;
                    Passable = false;
                    break;
                case '@':
                    Terrain = "Map Border";
                    FColor = ConsoleColor.Magenta;                    
                    Passable = false;
                    break;
                case ',':
                    Icon = SpecialChars.MaybeGrass1;
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
                case 'F':
                    int i = Program.rand.Next(0, 3);
                    Icon = SpecialChars.SmallTree1;
                    if (i == 2) Icon = SpecialChars.BigTree1;
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
