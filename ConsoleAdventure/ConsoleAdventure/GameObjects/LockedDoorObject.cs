using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class LockedDoorObject : Interactable
    {
        private int lockID;
        public LockedDoorObject(string _name, string _desc, int _key)
        {
            Icon = SpecialChars.Door;
            FColor = ConsoleColor.DarkYellow;
            Charges = 1;
            lockID = _key;
            Name = _name;
            Description = _desc;
        }

        public override void Interact()
        {
            bool locked = true;

            for(int i = 0; i < Program.PlayerInventory.Items.Count; i++)
            {
                DoorKey key = Program.PlayerInventory.Items[i] as DoorKey;
                if(key != null)
                {
                    if(key.ID == lockID)
                    {
                        locked = false;
                    }
                }
            }
            if (!locked)
            {
                Program.PrintCenterLine($"You unlock the {Name}.");
                DecreaseCharges();
            }
            else
            {
                Program.PrintCenterLine("You need a key of some kind to open this door.");
            }
        }
    }
}
