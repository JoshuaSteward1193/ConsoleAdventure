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
                    //Adds between 1 and 4 RedMushrooms to the inventory
                    
                    DecreaseCharges();
                    int i = Program.rand.Next(1, 5);
                    Console.WriteLine($"You collect {i} mushrooms from the {Name}.");
                    while (i > 0)
                    {
                        Program.PlayerInventory.Items.Add(new GameItem("Red Mushroom", "A nice red mushroom. Probably edible.", 1));
                        i--;
                    }
                    
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
