using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class DoorKey
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }

        public DoorKey(string _name, string _desc, int _id)
        {
            Name = _name;
            Description = _desc;
            ID = _id;
        }
    }
}
