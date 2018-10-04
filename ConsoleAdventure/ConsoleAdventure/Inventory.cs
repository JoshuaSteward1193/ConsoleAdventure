using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class Inventory
    {
        public List<GameItem> Items = new List<GameItem>();

        public Inventory()
        {
        }
        public Inventory(GameItem _item1)
        {
            Items.Add(_item1);
        }
        public Inventory(GameItem _item1, GameItem _item2)
        {
            Items.Add(_item1);
            Items.Add(_item2);
        }
        public Inventory(GameItem _item1, GameItem _item2, GameItem _item3)
        {
            Items.Add(_item1);
            Items.Add(_item2);
            Items.Add(_item3);
        }
        public Inventory(GameItem _item1, GameItem _item2, GameItem _item3, GameItem _item4)
        {
            Items.Add(_item1);
            Items.Add(_item2);
            Items.Add(_item3);
            Items.Add(_item4);
        }
        
    }

}
