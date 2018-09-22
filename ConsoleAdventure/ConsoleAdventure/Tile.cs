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
                if (Thing == null)
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
                else
                {
                    return Thing.Icon;
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
                if(Thing == null)
                {
                    if (Resident == null)
                    {
                        return fColor;
                    }
                    else
                    {
                        return Resident.Color;
                    }
                }
                else
                {
                    return Thing.FColor;
                }
                
            }
            set
            {
                fColor = value;
            }
        }
        public ConsoleColor BColor { get; set; }
        private bool passable;
        public bool Passable
        {
            get
            {
                if(Thing == null)
                {
                    return passable;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                passable = value;
            }
        }
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

        private Interactable thing;
        public Interactable Thing { get; set; }

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
                case ':':
                    Terrain = "Gravel Path";
                    FColor = ConsoleColor.DarkGray;
                    Passable = true;
                    break;
                case '~':
                    Terrain = "Body of Water";
                    FColor = ConsoleColor.DarkCyan;
                    Passable = false;
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
