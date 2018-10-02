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

            foreach(DoorKey key in Program.p1.KeyRing)
            {
                if(key.ID == lockID)
                {
                    locked = false;
                }
            }
            if (!locked)
            {
                Console.WriteLine($"You unlock the {Name}.");
            }
            else
            {
                Console.WriteLine("You need a key of somekind to open this door.");
            }
        }
    }
}
