using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure.GameObjects
{
    class AppleItem : GameItem
    {
        public AppleItem() : base("Apple", "A fresh apple. Healthy!", 0, true)
        {
        }

        public override void UseItem()
        {
            Console.WriteLine($"You eat the {Name}. You feel better!");
            Program.p1.Health += 5;
        }
    }
}
