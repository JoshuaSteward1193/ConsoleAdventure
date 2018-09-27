using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    class InventoryController
    {
        private static List<GameItem> displayItems = new List<GameItem>();
        private static bool[] selectedIndex;

        public static void ShowInventory(Inventory inv)
        {
            int i = 0;
            foreach(GameItem x in inv.Items)
            {
                i++;
                displayItems.Add(x);
            }
            selectedIndex = new bool[displayItems.Count];
            selectedIndex[0] = true;
            for(int j = 1; j < selectedIndex.Length; j++)
            {
                selectedIndex[j] = false;
            }
        }

        public static void PrintLine(string name, bool selected, string desc)
        {
            if (selected)
            {
                Console.WriteLine($" > {name} | {desc}");                
            }
            else
            {
                Console.WriteLine($"   {name} | {desc}");                
            }            
        }

        public static void PrintInv()
        {
            GameItem item;
            for(int i = 0; i < displayItems.Count; i++)
            {
                item = displayItems[i];
                PrintLine(item.Name, selectedIndex[i], item.Description);
            }
        }
    }
}
