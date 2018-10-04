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
        public int YPos { get; set; }
        public int XPos { get; set; }
        
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
        public Interactable()
        {

        }

        public virtual void Maintainance()
        {
            switch (MaintainanceID)
            {

            }
        }
        public virtual void Interact()
        {
            switch (InteractionID)
            {
                case 0:
                    Console.WriteLine($"You kick the {Name}. Nothing happens.");
                    break;
                case 1:
                    Console.WriteLine("The door looks like it will fall down at any minute. You throw all your weight " +
                        "aginst the door. It falls outward under the blow, and you are free.");
                    DecreaseCharges();
                    break;
            }

        }
        public virtual void SetPos(int y, int x)
        {
            YPos = y;
            XPos = x;
        }
        public virtual void DecreaseCharges()
        {
            Charges--;
            if(Charges <= 0)
            {
                Program.currentMap.TerrainData[YPos, XPos].DestroyThing();
            }
            
        }
    }
}
