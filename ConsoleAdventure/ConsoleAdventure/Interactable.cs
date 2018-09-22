using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Interactable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public char Icon { get; set; }
        public ConsoleColor FColor { get; set; }
        public ConsoleColor BColor { get; set; }
        public int InteractionID { get; set; }
        public int MaintainanceID { get; set; }
        public int Charges { get; set; }
        
        public Interactable(string name, string desc, char ico, int charge, int id, int mId, ConsoleColor color, ConsoleColor bColor = ConsoleColor.Black)
        {
            Name = name;
            Description = desc;
            Icon = ico;
            FColor = color;
            InteractionID = id;
            MaintainanceID = mId;
            Charges = charge;
        }

        public void Maintainance()
        {
            switch (MaintainanceID)
            {

            }
        }
        public void Interact()
        {
            switch (InteractionID)
            {
                case 0:
                    Console.WriteLine($"You kick the {Name}. Nothing happens.");
                    break;
            }
        }
    }
}
