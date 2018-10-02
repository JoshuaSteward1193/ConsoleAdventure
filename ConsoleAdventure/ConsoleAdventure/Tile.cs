using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Tile
    {
        public int Gcost;
        public int Hcost;
        public Tile Parent = null;
        public Coordinate Coord { get; set; }
        private char icon;
        public char Icon
        {
            get
            {
                if (Resident == null)
                {
                    if (Thing == null)
                    {
                        return icon;
                    }
                    else
                    {
                        return Thing.Icon;
                    }
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
                    if (Thing == null)
                    {
                        return fColor;
                    }
                    else
                    {
                        return Thing.FColor;
                    }
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
        private bool passable;
        public bool Passable
        {
            get
            {
                if(Thing == null)
                {
                    if(Resident == null)
                    {
                        return passable;
                    }
                    else
                    {
                        return false;
                    }                    
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
        public Interactable Thing
        {
            get
            {
                return thing;
            }
            set
            {
                thing = value;
                if(thing != null)
                {
                    thing.SetPos(Coord.YVal, Coord.XVal);
                }                
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
                case '#':
                    Icon = SpecialChars.SolidMaterial;
                    Terrain = "Rock Wall";
                    FColor = ConsoleColor.DarkGray;
                    Passable = false;
                    break;
                case '+':
                    Terrain = "Stone Structure";
                    FColor = ConsoleColor.Gray;
                    Passable = true;
                    break;
                case '.':
                    Icon = SpecialChars.Dirt;
                    Terrain = "Bare dirt";
                    FColor = ConsoleColor.DarkYellow;
                    Passable = true;
                    break;
                case '/':
                    Terrain = "Steep Rocks";
                    FColor = ConsoleColor.DarkGray;
                    Passable = false;
                    break;
                case '\\':
                    Terrain = "Steep Rocks";
                    FColor = ConsoleColor.DarkGray;
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
        public void DestroyThing()
        {
            Thing = null;
        }
        public int GetHcost(Tile goal)
        {
            if(Hcost != 0)
            {
                int x1 = Coord.XVal;
                int x2 = goal.Coord.XVal;
                int y1 = Coord.YVal;
                int y2 = goal.Coord.YVal;
                return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
            }
            else
            {
                return Hcost;
            }
            
        }
        public int GetGcost(Tile start)
        {
            if(Gcost != 0)
            {
                int x1 = Coord.XVal;
                int x2 = start.Coord.XVal;
                int y1 = Coord.YVal;
                int y2 = start.Coord.YVal;
                return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
            }
            else
            {
                return Gcost;
            }
        }
    }
}
