using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class ChestObject : Interactable
    {
        public Inventory Stash { get; set; }
        bool KeyRequired;        

        public ChestObject(string _name, string _description, bool _key, Inventory _stash)
        {
            Name = _name;
            Description = _description;
            Icon = SpecialChars.Chest;
            FColor = ConsoleColor.DarkYellow;
            KeyRequired = _key;
            Stash = _stash;
        }

        public override void Interact()
        {
            LootChest();
        }
        public void LootChest()
        {
            string message = "";
            //int number = 0;
            if (Stash.Items.Count == 0) message += ("This chest is empty.");
            for (int i = 0; i < Stash.Items.Count; i++)
            {
                message += ($"You got a {Stash.Items[i].Name}! ");
                if(Stash.Items[i].GetType() == typeof(Weapon))
                {
                    Program.PlayerArmory.Items.Add(Stash.Items[i]);
                }
                else
                {
                    Program.PlayerInventory.Items.Add(Stash.Items[i]);
                }
                                
            }
            Stash.Items.Clear();
            Program.WriteToDialogBox(message);
        }
    }
}
