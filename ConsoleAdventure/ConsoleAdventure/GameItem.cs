using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class GameItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UseID { get; set; }
        public bool Usable { get; set; }

        public GameItem(string _name, string _desc, int _useId, bool _usable)
        {
            Name = _name;
            Description = _desc;
            UseID = _useId;
            Usable = _usable;
        }

        public virtual void UseItem()
        {
            switch (UseID)
            {
                case 0:
                    Console.WriteLine("You think you hear a familiar voice inside your head: 'Now is not the time to use that!'");
                    Console.WriteLine("You must be going crazy.");
                    break;
                case 1:
                    Console.WriteLine($"You eat the {Name}. You don't feel any different.");
                    break;
            }
        }        
    }
}
