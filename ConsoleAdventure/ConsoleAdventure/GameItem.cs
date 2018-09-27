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

        public GameItem(string nam, string desc, int id)
        {
            Name = nam;
            Description = desc;
            UseID = id;
        }

        public void UseItem()
        {
            switch (UseID)
            {
                case 1:
                    Console.WriteLine($"You eat the {Name}. You don't feel any different.");
                    break;
            }
        }        
    }
}
