using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class NPC : Character
    {
        public List<Interactable> MyStuff = new List<Interactable>();


        public NPC(string name, char ico, ConsoleColor col, int hp) : base(name, ico, col, hp)
        {
        }
    }
}
