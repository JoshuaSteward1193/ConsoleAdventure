using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class NPC : Character
    {
        public List<Interactable> MyStuff = new List<Interactable>();        
        private Coordinate position;
        override public Coordinate Position
        {
            get
            {
                return position;
            }
            set
            {
                if(value != null && Position != null)
                {
                    if (value.XVal == Position.XVal - 1 || value.YVal == Position.YVal + 1)
                    {
                        Icon = SpecialChars.HatManLeft;
                    }
                    else if (value.XVal == Position.XVal + 1 || value.YVal == Position.YVal - 1)
                    {
                        Icon = SpecialChars.HatManRight;
                    }
                }                
                position = value;
            }
        }


        public NPC(string name, char ico, ConsoleColor col, int hp, Map map) : base(name, ico, col, hp, map)
        {
        }
        private void LeftOrRight()
        {

        }
    }
}
