using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class DoorKey : GameItem
    {        
        public int ID { get; set; }

        public DoorKey(string _name, string _desc, int _id) : base(_name, _desc, 0, false)
        {
            ID = _id;
        }
    }
}
