using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class MushroomColonyObject : Interactable
    {
        public MushroomColonyObject()
        {
            Name = "Mushroom Ring";
            Description = "A ring-shaped colony of mushrooms are growing here.";
            Icon = SpecialChars.MushroomRing;
            FColor = ConsoleColor.Red;
            BColor = ConsoleColor.Black;
            Charges = 1;
            InteractionID = 1;
            MaintainanceID = 0;
        }

        public override void Interact()
        {
            //Adds between 1 and 4 RedMushrooms to the inventory
            DecreaseCharges();
            int i = Program.rand.Next(1, 5);
            Console.WriteLine($"You collect {i} mushrooms from the {Name}.");
            while (i > 0)
            {
                Program.PlayerInventory.Items.Add(new GameItem("Red Mushroom", "A nice red mushroom. Probably edible.", 1, true));
                i--;
            }
        }
    }
}
